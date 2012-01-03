using LibSmoz.Transformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SMOz.Tests
{
    /// <summary>
    ///This is a test class for CategoryTest and is intended
    ///to contain all CategoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CategoryTest
    {

        /// <summary>
        ///A test for Category Constructor
        ///</summary>
        [TestMethod()]
        public void CategoryConstructorTest()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            string restrictedPath = string.Empty; // TODO: Initialize to an appropriate value
            Category target = new Category(name, restrictedPath);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Category Constructor
        ///</summary>
        [TestMethod()]
        public void CategoryConstructorTest1()
        {
            string name = string.Empty; // TODO: Initialize to an appropriate value
            Category target = new Category(name);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for Category Constructor
        ///</summary>
        [TestMethod()]
        public void CategoryConstructorTest2()
        {
            Category target = new Category();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CompareTo
        ///</summary>
        [TestMethod()]
        public void CompareToTest()
        {
            Category target = new Category(); // TODO: Initialize to an appropriate value
            Category other = null; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.CompareTo(other);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
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
        ///A test for Merge
        ///</summary>
        [TestMethod()]
        public void MergeTest()
        {
            Category target = new Category(); // TODO: Initialize to an appropriate value
            Category category = null; // TODO: Initialize to an appropriate value
            target.Merge(category);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
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
