using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine.OptParse;
using System.ComponentModel;

namespace Smoz.Cli
{

    class Options
    {

        public Options(){
            StartMenuFolders = new List<string>();
            TemplateFiles = new List<string>();
        }

        [OptDef(OptValType.Flag)]
        [ShortOptionName('?')]
        [ShortOptionName('h')]
        [LongOptionName("help")]
        [UseNameAsLongOption(false)]
        [Description("Prints help and quits.")]
        public bool Help { get; set; }

        [OptDef(OptValType.Flag)]
        [LongOptionName("version")]
        [UseNameAsLongOption(false)]
        [Description("Prints version information and quits.")]
        public bool Version { get; set; }

        [OptDef(OptValType.Flag)]
        [ShortOptionName('q')]
        [LongOptionName("quiet")]
        [UseNameAsLongOption(false)]
        [Description("Prints minimal amount of messages.")]
        public bool Quiet { get; set; }

        [OptDef(OptValType.MultValue, ValueType = typeof(string))]
        [ShortOptionName('f')]
        [LongOptionName("start-folder")]
        [UseNameAsLongOption(false)]
        [Description("Sets the start menu folder(s) to manipulate. If not specified, SMOz will try to use all the Start Menu folders it can access")]
        public List<string> StartMenuFolders { get; set; }

        [OptDef(OptValType.MultValue, ValueType = typeof(string))]
        [ShortOptionName('t')]
        [LongOptionName("template")]
        [UseNameAsLongOption(false)]
        [Description("Sets the template file(s) to use. If more than one are specified, they will be merged. If none are specified, the program will look for a file named \"Template.ini\" in the current directory or the directory where this executable is located.")]
        public List<string> TemplateFiles { get; set; }

        public IEnumerable<string> Directories { get; set; }

        public void ProcessArguments(string[] args)
        {
            List<string> input = args.Where(arg => arg != Environment.GetCommandLineArgs()[0]).ToList();

//            if (input.Count > 0)
//                Directories = input;
//            else
//                Directories = new string[] {Environment.CurrentDirectory};
        }
    }
}
