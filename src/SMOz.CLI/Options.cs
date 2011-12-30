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
            StarMenuFolders = new List<string>();
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
        [LongOptionName("start-folders")]
        [UseNameAsLongOption(false)]
        [Description("Sets the start menu folders to manipulate. If not specified, SMOz will try to use all the Start Menu locations it can access")]
        public List<string> StarMenuFolders { get; set; }


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
