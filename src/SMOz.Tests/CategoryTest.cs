using System.Collections.Generic;
using LibSmoz.Transformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace SMOz.Tests
{
    /// <summary>
    ///This is a test class for CategoryTest and is intended
    ///to contain all CategoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CategoryTest
    {

        [TestMethod()]
        public void CategoryConstructorTest()
        {
            string name = "Hello";
            string restrictedPath = "Good\\Bye";
            Category target = new Category(name, restrictedPath);
            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(restrictedPath, target.RestrictedPath);
            Assert.IsTrue(target.IsRestricted);
        }

        [TestMethod()]
        public void CategoryConstructorTest1()
        {
            string name = "Hello";
            Category target = new Category(name);
            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(string.Empty, target.RestrictedPath);
            Assert.IsFalse(target.IsRestricted);
        }

        [TestMethod()]
        public void CategoryConstructorTest2()
        {
            Category target = new Category();
            Assert.AreEqual(string.Empty, target.Name);
            Assert.AreEqual(string.Empty, target.RestrictedPath);
            Assert.IsFalse(target.IsRestricted);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            Category a = new Category("Hello", "Good\\Bye");
            Category c = new Category("Zzzzz", "AAAA");
            Category d = new Category("Hello", "AAAA");

            var source = new List<Category> {a, c, d};
            var expected = new List<Category> {d, a, c};

            source.Sort();

            Assert.IsTrue(source.SequenceEqual(expected));

            Assert.AreEqual(-1, a.CompareTo(null));

        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest()
        {
            Category target = new Category(); // TODO: Initialize to an appropriate value
            Category other = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(other);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void EqualsTest1()
        {
            Category target = new Category(); // TODO: Initialize to an appropriate value
            object obj = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetHashCode
        ///</summary>
        [TestMethod()]
        public void GetHashCodeTest()
        {
            Category target = new Category(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Category target = new Category(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Equality
        ///</summary>
        [TestMethod()]
        public void op_EqualityTest()
        {
            Category a = null; // TODO: Initialize to an appropriate value
            Category b = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (a == b);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for op_Inequality
        ///</summary>
        [TestMethod()]
        public void op_InequalityTest()
        {
            Category a = null; // TODO: Initialize to an appropriate value
            Category b = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = (a != b);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsRestricted
        ///</summary>
        [TestMethod()]
        public void IsRestrictedTest()
        {
            Category target = new Category(); // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsRestricted;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            Category target = new Category(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RestrictedPath
        ///</summary>
        [TestMethod()]
        public void RestrictedPathTest()
        {
            Category target = new Category(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.RestrictedPath = expected;
            actual = target.RestrictedPath;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
