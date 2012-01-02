using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibSmoz.Transformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SMOz.Tests
{
    [TestClass]
    public class TemplateMergeTest
    {
        [TestMethod]
        public void TestMerge()
        {
            string templA = @"
[Category1]
Item1A
Item1B
@Item1C
*Item1D

[Category2]
Item2A
Item2B
            ";

            string templB = @"
[Category1]
Item1A
Item1B
@Item1C
*Item1D
Item1E

[Category3]
Item3A
Item3B
            ";

            Template t1 = TemplateParser.Parse(TemplateParserTest.ReadAllLines(templA));
            Template t2 = TemplateParser.Parse(TemplateParserTest.ReadAllLines(templB));

            t1.Merge(t2);

            Assert.AreEqual(3, t1.Count);
            Assert.AreEqual(5, t1.First(i => i.Name == "Category1").Count);
            Assert.AreEqual(2, t1.First(i => i.Name == "Category2").Count);
            Assert.AreEqual(2, t1.First(i => i.Name == "Category3").Count);
        }
        
        [TestMethod]
        public void TestMergeComplex()
        {
            string templA = @"
[Category1]
@^Item1A$
@^Item1B$
@Item1C
*Item1D

[Category2]
Item2A
Item2B
            ";

            string templB = @"
[Category1]
Item1A
Item1B
Item1C
@.*Item1D.*
Item1E

[Category3]
Item3A
Item3B
            ";

            // All selectors untimately become regex. Thus:
            //      Item1A  == @^Item1A$   
            //      *Item1D == @.*Item1D.* 

            Template t1 = TemplateParser.Parse(TemplateParserTest.ReadAllLines(templA));
            Template t2 = TemplateParser.Parse(TemplateParserTest.ReadAllLines(templB));

            t1.Merge(t2);

            Assert.AreEqual(3, t1.Count);
            Assert.AreEqual(6, t1.First(i => i.Name == "Category1").Count);
            Assert.AreEqual(2, t1.First(i => i.Name == "Category2").Count);
            Assert.AreEqual(2, t1.First(i => i.Name == "Category3").Count);
        }
    }
}
