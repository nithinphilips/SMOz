using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Afterthought.UnitTest.Target;

namespace Afterthought.UnitTest
{
	[TestClass]
	public class MethodTests
	{
		Calculator Calculator { get; set; }

		[TestInitialize]
		public void InitializeCalculator()
		{
			Calculator = new Calculator();
		}

		/// <summary>
		/// Tests explicitly implementing an interface property by specifying
		/// a new property to add that implements the interface.
		/// </summary>
		[TestMethod]
		public void AddMethod()
		{
			var expected = 16 - 5;

			Assert.AreEqual(expected, ((IMath)Calculator).Subtract(16, 5));
		}
	}
}
