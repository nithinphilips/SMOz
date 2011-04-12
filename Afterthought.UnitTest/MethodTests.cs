//-----------------------------------------------------------------------------
//
// Copyright (c) VC3, Inc. All rights reserved.
// This code is licensed under the Microsoft Public License.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//-----------------------------------------------------------------------------

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
		/// Tests adding a new public method to a type.
		/// </summary>
		[TestMethod]
		public void AddMethod()
		{
			var expected = 16 - 5;

			Assert.AreEqual(expected, ((IMath)Calculator).Subtract(16, 5));
		}

		/// <summary>
		/// Tests modifying an existing method to run code before the original method
		/// implementation without affecting the values of the specified parameters.
		/// </summary>
		[TestMethod]
		public void BeforeMethodWithoutChanges()
		{
			// Verify that the Multiply method also copies the output to the Result property
			Assert.AreEqual(Calculator.Multiply(3, 4), Calculator.Result);
		}

		/// <summary>
		/// Tests modifying an existing method to run code before the original method
		/// implementation without affecting the values of the specified parameters.
		/// </summary>
		[TestMethod]
		public void BeforeMethodWithChanges()
		{
			var expected = 18;

			// Verify that the second parameter is always converted to 1
			Assert.AreEqual(expected, Calculator.Divide(expected, 0));
		}
	}
}
