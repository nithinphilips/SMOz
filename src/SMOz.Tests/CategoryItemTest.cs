using LibSmoz.Transformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

namespace SMOz.Tests
{
    /// <summary>
    ///This is a test class for CategoryItemTest and is intended
    ///to contain all CategoryItemTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CategoryItemTest
    {
      
        [TestMethod()]
        public void CategoryItemConstructorTest()
        {
            string value = "Hello";
            CategoryItemType type = CategoryItemType.WildCard; 
            CategoryItem target = new CategoryItem(value, type);

            Assert.AreEqual("Hello", target.Value);
            Assert.AreEqual(".*Hello.*", target.Pattern);
            Assert.AreEqual(CategoryItemType.WildCard, target.Type);

            target.Value = "GoodBye";

            Assert.AreEqual("GoodBye", target.Value);
            Assert.AreEqual(".*GoodBye.*", target.Pattern);
            Assert.AreEqual(CategoryItemType.WildCard, target.Type);

            target.Type = CategoryItemType.String;
            
            Assert.AreEqual("GoodBye", target.Value);
            Assert.AreEqual("^GoodBye$", target.Pattern);
            Assert.AreEqual(CategoryItemType.String, target.Type);
        }

        [TestMethod()]
        public void CategoryItemConstructorTest1()
        {
            CategoryItem target = new CategoryItem();
            
            Assert.AreEqual(string.Empty, target.Value);
            Assert.AreEqual(string.Empty, target.Pattern);
            Assert.AreEqual(CategoryItemType.Regex, target.Type);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            CategoryItem target = new CategoryItem("Hello", CategoryItemType.String);
            CategoryItem other = new CategoryItem("GoodBye", CategoryItemType.String);
            int expected = "^Hello^".CompareTo("^GoodBye$");
            int actual;
            actual = target.CompareTo(other);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            CategoryItem target = new CategoryItem("Hello", CategoryItemType.String);
            object obj = null;
            bool actual = target.Equals(obj);
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void EqualsTest1()
        {
            CategoryItem target = new CategoryItem("Hello", CategoryItemType.String);
            object obj = new CategoryItem("Hello", CategoryItemType.String);
            bool expected = true; 
            bool actual;
            actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            CategoryItem target = new CategoryItem("Hello", CategoryItemType.String);
            int expected = "^Hello$".GetHashCode();
            int actual = target.GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void op_EqualityTest()
        {
            CategoryItem a = new CategoryItem("Hello", CategoryItemType.String);
            CategoryItem b = new CategoryItem("^Hello$", CategoryItemType.Regex);
            bool actual = (a == b);
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void op_EqualityTest1()
        {
            CategoryItem a = new CategoryItem("Hello", CategoryItemType.String);
            CategoryItem b = null; 
            bool actual = (a == b);
            Assert.AreEqual(true, actual);
       }

        [TestMethod()]
        public void op_ImplicitTest()
        {
            CategoryItem x = new CategoryItem("Hello", CategoryItemType.String);
            string expected = "Hello";
            string actual = x; 
            Assert.AreEqual(expected, actual);

            CategoryItem y = "@^Hello$";
            expected = "Hello";
            actual = y.Value; 
            Assert.AreEqual(expected, actual);
            
            CategoryItem z = "^Hello$";
            expected = "Hello";
            actual = y.Value; 
            Assert.AreEqual(expected, actual);
            
            CategoryItem a = "@^Hello$";
            expected = "Hello";
            actual = y.Value; 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void op_InequalityTest()
        {
            CategoryItem a = new CategoryItem("Hello", CategoryItemType.String);
            CategoryItem b = new CategoryItem("Hello", CategoryItemType.Regex); 
            bool expected = true;
            bool actual;
            actual = (a != b);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void RegexObjectTest()
        {
            CategoryItem target = new CategoryItem("Hello.lnk", CategoryItemType.String);
            
            // Hello (String) = ^Hello$ (Regex)
            Assert.IsTrue(target.RegexObject.IsMatch("Hello.lnk"));
            Assert.IsFalse(target.RegexObject.IsMatch("Hello"));
            Assert.IsFalse(target.RegexObject.IsMatch("AHello.lnk"));
            Assert.IsFalse(target.RegexObject.IsMatch("Hello.lnkA"));
            Assert.IsFalse(target.RegexObject.IsMatch("HelloA.lnk"));

            target = new CategoryItem("Hello", CategoryItemType.WildCard);
            
            // Hello (WildCard) = .*Hello.* (Regex)
            Assert.IsTrue(target.RegexObject.IsMatch("Hello.lnk"));
            Assert.IsTrue(target.RegexObject.IsMatch("Hello"));
            Assert.IsTrue(target.RegexObject.IsMatch("AHello.lnk"));
            Assert.IsTrue(target.RegexObject.IsMatch("Hello.lnkA"));
            Assert.IsTrue(target.RegexObject.IsMatch("HelloA.lnk"));
            Assert.IsFalse(target.RegexObject.IsMatch("HAAello.lnk"));
            
            target = new CategoryItem("Hello", CategoryItemType.Regex);
            
            Assert.IsTrue(target.RegexObject.IsMatch("Hello.lnk"));
            Assert.IsTrue(target.RegexObject.IsMatch("Hello"));
            Assert.IsTrue(target.RegexObject.IsMatch("AHello.lnk"));
            Assert.IsTrue(target.RegexObject.IsMatch("Hello.lnkA"));
            Assert.IsTrue(target.RegexObject.IsMatch("HelloA.lnk"));
            Assert.IsFalse(target.RegexObject.IsMatch("HAAello.lnk"));
            
            target = new CategoryItem("H[A]?ello", CategoryItemType.Regex);
            
            Assert.IsTrue(target.RegexObject.IsMatch("Hello.lnk"));
            Assert.IsTrue(target.RegexObject.IsMatch("Hello"));
            Assert.IsTrue(target.RegexObject.IsMatch("AHello.lnk"));
            Assert.IsTrue(target.RegexObject.IsMatch("Hello.lnkA"));
            Assert.IsTrue(target.RegexObject.IsMatch("HelloA.lnk"));
            Assert.IsTrue(target.RegexObject.IsMatch("HAello.lnk"));
            Assert.IsFalse(target.RegexObject.IsMatch("HAAello.lnk"));
        }

        // CategoryItem.Value should always be identical to the value we assigned to it
        [TestMethod()]
        public void StringValueTest()
        {
            CategoryItem target = new CategoryItem("Hello", CategoryItemType.String);
            string expected = "Hello";
            string actual;
            target.Value = expected;
            actual = target.Value;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void WildCardValueTest()
        {
            CategoryItem target = new CategoryItem("Hello", CategoryItemType.WildCard);
            string expected = "Hello";
            string actual;
            target.Value = expected;
            actual = target.Value;

            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod()]
        public void RegexValueTest()
        {
            CategoryItem target = new CategoryItem("Hell[aeiou]", CategoryItemType.String);
            string expected = "Hell[aeiou]";
            string actual;
            target.Value = expected;
            actual = target.Value;

            Assert.AreEqual(expected, actual);
        }
    }
}
