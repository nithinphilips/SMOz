using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CommandLine.OptParse;
using LibSmoz.Transformation;

namespace Smoz.Cli
{
    class Program
    {
        static Options options = new Options();

        static void Main(string[] args)
        {
#if RELEASE
            try
            {
#endif
                Console.CancelKeyPress += Console_CancelKeyPress;
                PrintBanner();

                // ****************************************************************
                // Parse command-line arguments
                // ****************************************************************
                PropertyFieldParserHelper parseHelper = new PropertyFieldParserHelper(options);
                Parser parser = ParserFactory.BuildParser(parseHelper);
                parser.CaseSensitive = false;
                parser.DupOptHandleType = DupOptHandleType.Error;
                parser.SearchEnvironment = true;
                parser.UnixShortOption = UnixShortOption.CollapseShort;
                parser.UnknownOptHandleType = UnknownOptHandleType.Warning;
                parser.OptionWarning += parser_OptionWarning;

                try
                {
                    options.ProcessArguments(parser.Parse());
                }
                catch (ParseException peX)
                {
                    Console.Error.WriteLine("There was an error parsing the command-line arguments");
                    Console.Error.WriteLine("    {0}", peX.Message);
                    if (peX.InnerException != null)
                    {
                        Console.Error.WriteLine("    {0}", peX.InnerException.Message);
                    }
                    Console.Error.WriteLine("See --help for proper use.");
                    Environment.Exit(2);
                }

                // ****************************************************************
                // Print help or version
                // ****************************************************************
                if (options.Help)
                {
                    // Print help and exit
                    PrintHelp(parseHelper);
                    Environment.Exit(0);
                }

                if (options.Version)
                {
                    // Print help and exit
                    PrintVersion("SMOz", 2012, "Nithin Philips");
                    Environment.Exit(0);
                }

                if (options.StartMenuFolders.Count == 0)
                {
                    if (Helpers.IsRunAsAdmin())
                    {
                        // We get the current user's start menu path and replace the user name 
                        // with all available user names. This method is fairly naive. 
                        // If the user's name is not unique in the path string, this method will
                        // fail to find the correct start menu location.
                        Console.WriteLine("Running as Administrator");
                        string prototypeStartMenu = Win32.GetFolderPath(Win32.CSIDL.CSIDL_PROGRAMS);
                        prototypeStartMenu = prototypeStartMenu.Replace(Environment.UserName, "{0}");

                        options.StartMenuFolders.AddRange(GetAllStartMenus(prototypeStartMenu));

                        options.StartMenuFolders.Add(Win32.GetFolderPath(Win32.CSIDL.CSIDL_COMMON_PROGRAMS));
                    }
                    else
                    {
                        options.StartMenuFolders.Add(Win32.GetFolderPath(Win32.CSIDL.CSIDL_PROGRAMS));
                    }
                }

                if (!options.Quiet) foreach (var starMenuFolder in options.StartMenuFolders) Console.WriteLine(starMenuFolder);

            if(options.TemplateFiles.Count == 0)
            {
                // Look in cwd.
                string cwdTemplate = Path.Combine(Environment.CurrentDirectory, "Template.ini");
                Console.WriteLine(cwdTemplate);
                if(File.Exists(cwdTemplate))
                {
                    options.TemplateFiles.Add(cwdTemplate);
                }else
                {
                    string exeDirTemplate = Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Template.ini");
                    Console.WriteLine(exeDirTemplate);
                    if(File.Exists(exeDirTemplate))
                    {
                        options.TemplateFiles.Add(exeDirTemplate);
                    }else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("Error: Could not find any template files and none specified.");
                        Console.ResetColor();
                    }
                }
            }

            Template t;
            foreach (var templateFile in options.TemplateFiles)
            {
                t = TemplateParser.Parse(options.TemplateFiles);
            }

#if RELEASE
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("An exeption of type {0} has occured.", ex.GetType());
                Console.Error.WriteLine("   {0}", ex.Message);
                if (!options.Quiet) Console.Error.WriteLine("Details:\n{0}", ex.StackTrace);
            }
#endif

        }

        static IEnumerable<string> GetAllStartMenus(string prototype)
        {
            PrincipalContext ctx = new PrincipalContext(ContextType.Machine, Environment.MachineName);
            UserPrincipal user = new UserPrincipal(ctx) { Name = "*" };
            PrincipalSearcher ps = new PrincipalSearcher { QueryFilter = user };
            PrincipalSearchResult<Principal> result = ps.FindAll();
            foreach (Principal p in result)
            {
                using (UserPrincipal up = (UserPrincipal)p)
                {

                    string programsDir = string.Format(prototype, up.Name);
                    if (Directory.Exists(programsDir))
                        yield return programsDir;
                }
            }
        }


        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Environment.Exit(-1);
        }

        static void parser_OptionWarning(Parser sender, OptionWarningEventArgs e)
        {
            Console.Error.WriteLine("WARNING: " + e.WarningMessage + ". Try --help for a list of possible arguments.");
        }

        private static void PrintBanner()
        {
            Version vrs = Assembly.GetExecutingAssembly().GetName().Version;
            Console.Error.WriteLine("SMOz v{0}.", vrs.ToString());
            Console.Error.WriteLine();
        }

        private static void PrintVersion(string productName, int year, string author)
        {
            Console.WriteLine("{0}.  Copyright (C) {1}  {2}.", productName, year, author);
            Console.WriteLine("This program comes with ABSOLUTELY NO WARRANTY;");
            Console.WriteLine("This is free software, and you are welcome to redistribute it");
            Console.WriteLine("under certain conditions; visit 'http://www.gnu.org/licenses/gpl.html' for details.");
        }

        public static void PrintHelp(IOptionContainer options)
        {
            UsageBuilder usage = new UsageBuilder();

            usage.BeginSection("Name");
            usage.AddParagraph("Start Menu Organizer");
            usage.EndSection();

            usage.BeginSection("Synopsis");
            usage.AddParagraph("smoz.exe [arguments]");
            usage.EndSection();

            usage.BeginSection("Description");
            usage.AddParagraph(@"A tool of organize the Windows Start Menu, based on predefined rules.");
            usage.EndSection();

            usage.BeginSection("Arguments");
            usage.AddOptions(options);
            usage.EndSection();

            try
            {
                usage.ToText(Console.Out, OptStyle.Unix, true);
            }
            catch
            {
                usage.ToText(Console.Out, OptStyle.Unix, true, 90);
            }
        }
    }
}
