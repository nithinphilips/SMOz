using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibSmoz.ProgramsMenu;
using LibSmoz.Transformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SMOz.Tests
{
    [TestClass]
    public class EqualityTests
    {
        [TestMethod]
        public void CategoryItemEquality()
        {
            CategoryItem a = new CategoryItem("Hello", CategoryItemType.String);
            CategoryItem b = new CategoryItem("Hello", CategoryItemType.String);

            Assert.IsTrue(a == b, "Equality operator failed");
            Assert.AreEqual(true, a.Equals(b), "Explicit call to IEquatable<T> method failed");
            Assert.AreEqual(a, b, "Generic Assert.AreEqual call failed");
        }

        [TestMethod]
        public void CategoryItemSortComparison()
        {
            var l = new List<CategoryItem>
                                   {
                                       new CategoryItem("A", CategoryItemType.Regex),
                                       new CategoryItem("^Hello$", CategoryItemType.Regex),
                                       new CategoryItem("Hello", CategoryItemType.String),
                                       new CategoryItem("Hello", CategoryItemType.WildCard),
                                       new CategoryItem("Z", CategoryItemType.Regex)
                                   };

            var lSorted = new List<CategoryItem>
                                   {
                                       new CategoryItem("Hello", CategoryItemType.WildCard),
                                       new CategoryItem("^Hello$", CategoryItemType.Regex),
                                       new CategoryItem("Hello", CategoryItemType.String),
                                       new CategoryItem("A", CategoryItemType.Regex),
                                       new CategoryItem("Z", CategoryItemType.Regex)
                                   };

            Assert.IsFalse(l.SequenceEqual(lSorted));
            l.Sort();
            Assert.IsTrue(l.SequenceEqual(lSorted));
        }
        
        [TestMethod]
        public void CategoryEquality()
        {
            Category a = new Category("Hello");
            Category b = new Category("Hello");

            Assert.IsTrue(a == b, "Equality operator failed");
            Assert.AreEqual(true, a.Equals(b), "Explicit call to IEquatable<T> method failed");
            Assert.AreEqual(a, b, "Generic Assert.AreEqual call failed");
        }

        [TestMethod]
        public void CategorySortComparison()
        {
            var l = new List<Category>
                                   {
                                       new Category("Hello"),
                                       new Category("Hello", "GoodBye"),
                                       new Category("Hello", "A"),
                                       new Category("Hello", "B"),
                                       new Category("A"),
                                       new Category("Z")
                                   };

            var lSorted = new List<Category>
                                   {
                                       new Category("A"),
                                       new Category("Hello"),
                                       new Category("Hello", "A"),
                                       new Category("Hello", "B"),
                                       new Category("Hello", "GoodBye"),
                                       new Category("Z")
                                   };

            Assert.IsFalse(l.SequenceEqual(lSorted));
            l.Sort();
            Assert.IsTrue(l.SequenceEqual(lSorted));
        }

        [TestMethod]
        public void ProgramItemEquality()
        {
            Directory.CreateDirectory("Empty");
            StartMenu s = new StartMenu("Empty");
            ProgramCategory cat = new ProgramCategory(s);
            ProgramItem a = new ProgramItem(cat, "Hello.lnk");
            ProgramItem b = new ProgramItem(cat, "Hello.lnk");

            Assert.IsTrue(a == b, "Equality operator failed");
            Assert.AreEqual(true, a.Equals(b), "Explicit call to IEquatable<T> method failed");
            Assert.AreEqual(a, b, "Generic Assert.AreEqual call failed");
        }

        [TestMethod]
        public void ProgramItemSortComparison()
        {
            Directory.CreateDirectory("Empty");
            StartMenu s = new StartMenu("Empty");
            ProgramCategory cat = new ProgramCategory(s);
            var l = new List<ProgramItem>
                                   {
                                       new ProgramItem(cat, "Z.lnk"),
                                       new ProgramItem(cat, "2.lnk"),
                                       new ProgramItem(cat, "B.lnk"),
                                       new ProgramItem(cat, "1.lnk"),
                                       new ProgramItem(cat, "A.lnk"),
                                   };

            var lSorted = new List<ProgramItem>
                                   {
                                       new ProgramItem(cat, "1.lnk"),
                                       new ProgramItem(cat, "2.lnk"),
                                       new ProgramItem(cat, "A.lnk"),
                                       new ProgramItem(cat, "B.lnk"),
                                       new ProgramItem(cat, "Z.lnk"),
                                   };

            Assert.IsFalse(l.SequenceEqual(lSorted));
            l.Sort();
            Assert.IsTrue(l.SequenceEqual(lSorted));
        }
    }
}
