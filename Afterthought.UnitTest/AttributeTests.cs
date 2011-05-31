using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Afterthought.UnitTest.Target;
using System.ComponentModel;

namespace Afterthought.UnitTest
{
	[TestClass]
	public class AttributeTests
	{
		/// <summary>
		/// Tests adding attributes to a type
		/// </summary>
		[TestMethod]
		public void AddAttributesToType()
		{
			Type type = typeof(Calculator);

			var attributes = type.GetCustomAttributes(true).OfType<TestAttribute>();

			// Check for creation of an attribute with an int ctor
			Assert.IsTrue(attributes.Where((t) => { return t.IntValue == 5; }).Any(), "Failed to create int attribute");

			// Check for creation of an attribute with an empty ctor
			Assert.IsTrue(attributes.Where((t) => { return t.IntValue == -1; }).Any(), "Failed to create basic attribute");

			// Check for creation of an attribute with an Type ctor
			Assert.IsTrue(attributes.Where((t) => { return t.TypeValue == typeof(string); }).Any(), "Failed to create Type attribute");

			// Check for creation of an attribute with an array ctor
			Assert.IsTrue(attributes.Where((t) => { return t.StringArValue != null; }).Any(), "Failed to create array attribute");
		}

		/// <summary>
		/// Tests adding attributes to a field
		/// </summary>
		[TestMethod]
		public void AddAttributesToField()
		{
			Type type = typeof(Calculator);

			var attributes = type.GetField("holding1").GetCustomAttributes(true).OfType<TestAttribute>();

			// Check for creation of an attribute with an int ctor
			Assert.IsTrue(attributes.Where((t) => { return t.IntValue == 5; }).Any(), "Failed to create int attribute");

			// Check for creation of an attribute with an empty ctor
			Assert.IsTrue(attributes.Where((t) => { return t.IntValue == -1; }).Any(), "Failed to create basic attribute");

			// Check for creation of an attribute with an Type ctor
			Assert.IsTrue(attributes.Where((t) => { return t.TypeValue == typeof(string); }).Any(), "Failed to create Type attribute");

			// Check for creation of an attribute with an array ctor
			Assert.IsTrue(attributes.Where((t) => { return t.StringArValue != null; }).Any(), "Failed to create array attribute");
		}

		/// <summary>
		/// Tests adding attributes to a property
		/// </summary>
		[TestMethod]
		public void AddAttributesToProperty()
		{
			Type type = typeof(Calculator);

			var attributes = type.GetProperty("Random1").GetCustomAttributes(true).OfType<TestAttribute>();

			// Check for creation of an attribute with an int ctor
			Assert.IsTrue(attributes.Where((t) => { return t.IntValue == 5; }).Any(), "Failed to create int attribute");

			// Check for creation of an attribute with an empty ctor
			Assert.IsTrue(attributes.Where((t) => { return t.IntValue == -1; }).Any(), "Failed to create basic attribute");

			// Check for creation of an attribute with an Type ctor
			Assert.IsTrue(attributes.Where((t) => { return t.TypeValue == typeof(string); }).Any(), "Failed to create Type attribute");

			// Check for creation of an attribute with an array ctor
			Assert.IsTrue(attributes.Where((t) => { return t.StringArValue != null; }).Any(), "Failed to create array attribute");
		}

		/// <summary>
		/// Tests adding attributes to a method
		/// </summary>
		[TestMethod]
		public void AddAttributesToMethod()
		{
			Type type = typeof(Calculator);

			var attributes = type.GetMethod("Multiply").GetCustomAttributes(true).OfType<TestAttribute>();

			// Check for creation of an attribute with an int ctor
			Assert.IsTrue(attributes.Where((t) => { return t.IntValue == 5; }).Any(), "Failed to create int attribute");

			// Check for creation of an attribute with an empty ctor
			Assert.IsTrue(attributes.Where((t) => { return t.IntValue == -1; }).Any(), "Failed to create basic attribute");

			// Check for creation of an attribute with an Type ctor
			Assert.IsTrue(attributes.Where((t) => { return t.TypeValue == typeof(string); }).Any(), "Failed to create Type attribute");

			// Check for creation of an attribute with an array ctor
			Assert.IsTrue(attributes.Where((t) => { return t.StringArValue != null; }).Any(), "Failed to create array attribute");
		}
		
		/// <summary>
		/// Tests adding attributes to a constructor
		/// </summary>
		[TestMethod]
		public void AddAttributesToConstructor()
		{
			Type type = typeof(Calculator);

			var attributes = type.GetConstructor(new Type[] { }).GetCustomAttributes(true).OfType<TestAttribute>();

			// Check for creation of an attribute with an int ctor
			Assert.IsTrue(attributes.Where((t) => { return t.IntValue == 5; }).Any(), "Failed to create int attribute");

			// Check for creation of an attribute with an empty ctor
			Assert.IsTrue(attributes.Where((t) => { return t.IntValue == -1; }).Any(), "Failed to create basic attribute");

			// Check for creation of an attribute with an Type ctor
			Assert.IsTrue(attributes.Where((t) => { return t.TypeValue == typeof(string); }).Any(), "Failed to create Type attribute");

			// Check for creation of an attribute with an array ctor
			Assert.IsTrue(attributes.Where((t) => { return t.StringArValue != null; }).Any(), "Failed to create array attribute");
		}
	}
}
