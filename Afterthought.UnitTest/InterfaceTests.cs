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
	public class InterfaceTests
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
		public void ExplicitPropertyImplementation()
		{
			// Assert that the Pi amended property equals 3.14159
			Assert.AreEqual<decimal>(3.14159m, ((IMath)Calculator).Pi);
		}

		/// <summary>
		/// Tests implicitly implementing an interface property through the
		/// existence of a public property on the original type with the correct signature.
		/// </summary>
		[TestMethod]
		public void ImplicitPropertyImplementation()
		{
			// Assert that the Calculator now implicitly implements IMath.SqRt2
			Assert.AreEqual<decimal>(1.61803398874m, ((IMath)Calculator).SqRt2);
		}

		/// <summary>
		/// Tests automatically implementing an interface property by allowing 
		/// a new default property to be added.
		/// </summary>
		[TestMethod]
		public void AutomaticPropertyImplementation()
		{
			decimal base2 = 2m;

			// Set the IMath.Base property
			((IMath)Calculator).Base = base2;

			// Assert that the IMath.Base property was set to base 2
			Assert.AreEqual<decimal>(base2, ((IMath)Calculator).Base);
		}

		/// <summary>
		/// Tests the modifying an existing property that privately implements an interface.
		/// </summary>
		[TestMethod]
		public void UpdatePropertyImplementation()
		{
			// Assert that the ILog.e amended property equals 2.71828
			Assert.AreEqual(2.71828m, ((ILog)Calculator).e);
		}
	}
}
