using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibSmoz.Commands;
using LibSmoz.Commands.IO;
using LibSmoz.Commands.UI;
using LibSmoz.Model;
using LibSmoz.Transformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SMOz.Tests
{
    [TestClass]
    public class TransformationTest
    {

//        void ExtractZip(ref byte[] bytes, string destination)
//        {
//            using (var ms = new MemoryStream())
//            {
//                ms.Write(bytes, 0, bytes.Length);
//                ms.Seek(0, SeekOrigin.Begin);
//
//                using (var zip = Ionic.Zip.ZipFile.Read(ms))
//                {
//                    zip.ExtractAll(destination);
//                }
//            }
//        }

        static void ExtractZip(string file, string destination)
        {
            using (var zip = Ionic.Zip.ZipFile.Read(file))
            {
                zip.ExtractAll(destination);
            }
        }

        [TestMethod]
        [DeploymentItem(@"Resources\Programs.System.zip")]
        [DeploymentItem(@"Resources\Programs.User.zip")]
        [DeploymentItem(@"Resources\TransformationTest.Setup.ini")]
        public void StandardRun()
        {
            var knownCategories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            ExtractZip("Programs.System.zip", "Programs.System");
            ExtractZip("Programs.User.zip", "Programs.User");

            knownCategories.Add("Accessories");
            knownCategories.Add("Administrative Tools");

            var startMenu = new StartMenu(knownCategories, "Programs.System", "Programs.User");

            Template t = TemplateParser.Parse("TransformationTest.Setup.ini");

            foreach (var templateCategory in t)
                knownCategories.Add(templateCategory.Name);

            startMenu.Refresh();

            List<Command> realCommand = new List<Command>();
            List<Command> rollbackCommands = new List<Command>();

            var list = t.TransformStartMenu(startMenu).ToList();
            foreach (var command in list)
            {
                command.Execute();
                realCommand.Add((command as MoveProgramItemCommand).GetRealCommand());
            }

            foreach (var command in realCommand)
            {
                command.Execute();
                rollbackCommands.Add((command as MoveFileCommand).GetReverseCommand());
                Console.WriteLine(command);
            }

            var obj =
            new CommandGroup()
            {
                Name = string.Format("Actions performed on {0} {1}", DateTime.UtcNow.ToLongDateString(), DateTime.UtcNow.ToLongTimeString()),
                Commands = rollbackCommands
            };

            var cleanupCommands = t.CleanupStartMenu(startMenu).ToList();

            foreach (var cleanupCommand in cleanupCommands)
            {
                Console.WriteLine(cleanupCommand);
                cleanupCommand.Execute();

                obj.Commands.Add(((MoveFileCommand)cleanupCommand).GetReverseCommand());
            }

            File.WriteAllText("rollback.json", fastJSON.JSON.Instance.ToJSON(obj, true, true));
        }

        public static void Serialize<T>(T instance, string fileName) where T : class
        {
            File.WriteAllText(fileName, fastJSON.JSON.Instance.ToJSON(instance));
        }

        public static T DeSerialize<T>(string fileName) where T : class
        {
            object up;
            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                up = xs.Deserialize(fs);
            }
            return up as T;
        }

    }
}
