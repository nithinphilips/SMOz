using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using LibSmoz.Ini;
using LibSmoz.Transformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SMOz.Tests
{
    [TestClass]
    public class TemplateParserTest
    {
        [TestMethod]
        public void EmptyString()
        {
            const string templateStr = @"";
            Template t = TemplateParser.Parse(ReadAllLines(templateStr));
            Assert.AreEqual(0, t.Count);
        }

        [TestMethod]
        public void WhiteSpace()
        {
            const string templateStr = @"

    

";
            Template t = TemplateParser.Parse(ReadAllLines(templateStr));
            Assert.AreEqual(0, t.Count);
        }


        [TestMethod]
        public void LoadFile()
        {
            Template t = TemplateParser.Parse(@"F:\Work\Projects\smoz\src\SMOz.Tests\bin\Debug\Template.ini");
            Assert.AreEqual(12, t.Count);

            foreach (var category in t)
            {
                switch (category.Name)
                {
                    case "Administrative Tools":
                        Assert.AreEqual(0, category.Count);
                        break;
                    case "Accessories":
                        Assert.AreEqual(9, category.Count);
                        break;
                    case "Games":
                        Assert.AreEqual(97 - 2, category.Count); // 2 Items are duplicates
                        break;
                    case "Disc Creation":
                        Assert.AreEqual(8, category.Count);
                        break;
                    case "Internet":
                        Assert.AreEqual(24, category.Count);
                        break;
                    case "Video":
                        Assert.AreEqual(23, category.Count);
                        break;
                    case "Graphics":
                        Assert.AreEqual(10, category.Count);
                        break;
                    case "Audio":
                        Assert.AreEqual(52, category.Count);
                        break;
                    case "Programming":
                        Assert.AreEqual(14, category.Count);
                        break;
                    case "Office":
                        Assert.AreEqual(4, category.Count);
                        break;
                    case "System":
                        Assert.AreEqual(85 - 1, category.Count); // 1 Duplicate
                        break;
                    case "Games\\Windows":
                        Assert.AreEqual(11, category.Count);
                        Assert.AreEqual(true, category.IsRestricted);
                        Assert.AreEqual("Games", category.RestrictedPath);
                        break;
                        
                }
            }

        }

        [TestMethod]
        public void CommentsOnly()
        {
            const string templateStr = @"#Comment
# Comment
";
            Template t = TemplateParser.Parse(ReadAllLines(templateStr));
            Assert.AreEqual(0, t.Count);
        }

        [TestMethod, ExpectedException(typeof(IniParseException))]
        public void OrphanedValues()
        {
            const string templateStr = @"Something";
            TemplateParser.Parse(ReadAllLines(templateStr));
        }

        [TestMethod, ExpectedException(typeof(IniParseException))]
        public void InvalidCharacters()
        {
            string templateStr = @"[ThisHas/\:*?<>|InvalidChars" + "\"" + "]" + Environment.NewLine;
            templateStr += "*SomeSelector";
            TemplateParser.Parse(ReadAllLines(templateStr));
        }

        [TestMethod]
        public void EmptyCategory()
        {
            const string templateStr = @"[Dummy]";
            Template t = TemplateParser.Parse(ReadAllLines(templateStr));
            Assert.AreEqual(1, t.Count);
            Assert.AreEqual("Dummy", t.First().Name);
            Assert.AreEqual(0, t.First().Count);
        }

        [TestMethod]
        public void WildCardSelector()
        {
            const string templateStr = @"[Dummy]
*WildCard
";
            Template t = TemplateParser.Parse(ReadAllLines(templateStr));
            Assert.AreEqual(1, t.Count);
            Assert.AreEqual("Dummy", t.First().Name);
            Assert.AreEqual(CategoryItemType.WildCard, t.First().First().Type);
            Assert.AreEqual(".*WildCard.*", t.First().First().GetPattern());
        }

        [TestMethod]
        public void WildCardSelectorWithSpecialChars()
        {
            const string templateStr = @"[Dummy]
*WildCard**++
";
            Template t = TemplateParser.Parse(ReadAllLines(templateStr));
            Assert.AreEqual(1, t.Count);
            Assert.AreEqual("Dummy", t.First().Name);
            Assert.AreEqual(CategoryItemType.WildCard, t.First().First().Type);
            Assert.AreEqual(@".*WildCard\*\*\+\+.*", t.First().First().GetPattern());
        }

        [TestMethod]
        public void MixesSectionAndDuplicates()
        {
            const string templateStr = @"[Dummy]
Exacto
Exacto

[Dummy2]
Exacto
Exacto

[Dummy]
Exacto

[Dummy2]
Exacto2

[Dummy->Something]
Exacto
";
            Template t = TemplateParser.Parse(ReadAllLines(templateStr));
            Assert.AreEqual(3, t.Count);
            Assert.AreEqual("Dummy", t.ElementAt(0).Name);
            Assert.AreEqual("Dummy2", t.ElementAt(1).Name);
            Assert.AreEqual("Dummy", t.ElementAt(2).Name);
            Assert.AreEqual("Something", t.ElementAt(2).RestrictedPath);
            
            Assert.AreEqual(CategoryItemType.String, t.First().First().Type);
            
            Assert.AreEqual(1, t.ElementAt(0).Count);
            Assert.AreEqual("^Exacto$", t.ElementAt(0).ElementAt(0).GetPattern());

            Assert.AreEqual(2, t.ElementAt(1).Count);
            Assert.AreEqual("^Exacto$", t.ElementAt(1).ElementAt(0).GetPattern());
            Assert.AreEqual("^Exacto2$", t.ElementAt(1).ElementAt(1).GetPattern());

            Assert.AreEqual(1, t.ElementAt(2).Count);
            Assert.AreEqual("^Exacto$", t.ElementAt(1).ElementAt(0).GetPattern());
        }

        [TestMethod]
        public void ExactSelector()
        {
            const string templateStr = @"[Dummy]
Exacto
";
            Template t = TemplateParser.Parse(ReadAllLines(templateStr));
            Assert.AreEqual(1, t.Count);
            Assert.AreEqual("Dummy", t.First().Name);
            Assert.AreEqual(CategoryItemType.String, t.First().First().Type);
            Assert.AreEqual("^Exacto$", t.First().First().GetPattern());
        }

        [TestMethod]
        public void ExactSelectorWithSpecialChars()
        {
            const string templateStr = @"[Dummy]
Exacto**++
";
            Template t = TemplateParser.Parse(ReadAllLines(templateStr));
            Assert.AreEqual(1, t.Count);
            Assert.AreEqual("Dummy", t.First().Name);
            Assert.AreEqual(CategoryItemType.String, t.First().First().Type);
            Assert.AreEqual(@"^Exacto\*\*\+\+$", t.First().First().GetPattern());
        }

        [TestMethod]
        public void RegexSelector()
        {
            const string templateStr = @"[Dummy]
@Exacto.*
";
            Template t = TemplateParser.Parse(ReadAllLines(templateStr));
            Assert.AreEqual(1, t.Count);
            Assert.AreEqual("Dummy", t.First().Name);
            Assert.AreEqual(CategoryItemType.Regex, t.First().First().Type);
            Assert.AreEqual("Exacto.*", t.First().First().GetPattern());
        }

        [TestMethod]
        public void LeadingAndTrailingWhiteSpace()
        {
            const string templateStr = @"    [Dummy]    
    @Exacto.*    
    Part2
";
            Template t = TemplateParser.Parse(ReadAllLines(templateStr));
            Assert.AreEqual(1, t.Count);
            Assert.AreEqual("Dummy", t.First().Name);
            Assert.AreEqual(CategoryItemType.Regex, t.First().First().Type);
            Assert.AreEqual("Exacto.*", t.First().First().GetPattern());

            Assert.AreEqual(CategoryItemType.String, t.First().ElementAt(1).Type);
            Assert.AreEqual("^Part2$", t.First().ElementAt(1).GetPattern());
        }

        private static IEnumerable<string> ReadAllLines(string str)
        {
            StringReader sr  = new StringReader(str);
            while (sr.Peek() > 0)
            {
                yield return sr.ReadLine();
            }
        }
    }
}
