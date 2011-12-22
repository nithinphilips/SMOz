using System;
using System.Collections.Generic;
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
        static void Main(string[] args)
        {
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
