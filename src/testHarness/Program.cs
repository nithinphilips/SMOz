using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibSmoz;
using System.Diagnostics;
using LibSmoz.Commands;
using LibSmoz.Commands.UI;
using LibSmoz.Model;
using LibSmoz.Transformation;

namespace testHarness
{
    class Program
    {
        static void ScanDirTree(string directory)
        {
            directory = directory.EndsWith("\\") ? directory : directory + "\\";
            ScanDirTree(directory, directory);
        }
        static void ScanDirTree(string directory, string root)
        {
            var dirs = Directory.GetDirectories(directory);
            var files = Directory.GetFiles(directory, "*.lnk");

            foreach (var dir in dirs)
            {
                Console.WriteLine("Directory.CreateDirectory(@\"StartA\\{0}\");", dir.Replace(root, ""));
                ScanDirTree(dir, root);
            }

            foreach (var file in files)
            {
                Console.WriteLine("File.Create(@\"StartA\\{0}\").Close();", file.Replace(root, ""));
            }
        }

        static void Main(string[] args)
        {

            ScanDirTree(Environment.GetFolderPath(Environment.SpecialFolder.Programs));

            return;
            

            Template t = TemplateParser.Parse("Template.ini");

            List<string> startMenus = new List<string>(2)
                                          {
                                              Environment.GetFolderPath(Environment.SpecialFolder.Programs),
                                              @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\"
                                          };

            HashSet<string> knownCategories = new HashSet<string>
                                                  {
                                                      "Research In Motion",
                                                      "Accessories",
                                                      "Accessories\\System Tools"
                                                  };

            foreach (var templateCategory in t)
                knownCategories.Add(templateCategory.Name);
            

            StartMenu menu = new StartMenu();
            menu.KnownCategories = knownCategories;
            menu.AddLocations(startMenus);

            var commands = new List<Command>(t.TransformStartMenu(menu));

            Console.WriteLine(new string('-', 80));
            Console.WriteLine(menu);

            foreach (var cmd in commands)
            {
                Console.WriteLine(cmd);
                cmd.Execute();
            }


            Console.WriteLine(new string('-', 80));
            Console.WriteLine(menu);

            commands.Reverse();
            foreach (var cmd in commands)
            {
                Console.WriteLine(cmd);
                cmd.UnExecute();
            }

            Console.WriteLine(new string('-', 80));
            Console.WriteLine(menu);

            //Console.ReadLine();
        }
    }
}
