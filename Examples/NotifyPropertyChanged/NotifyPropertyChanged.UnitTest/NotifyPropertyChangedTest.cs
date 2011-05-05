using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotifyPropertyChanged.UnitTest.Target;
using System.ComponentModel;

namespace NotifyPropertyChanged.UnitTest
{
	[TestClass]
	public class NotifyPropertyChangedTest
	{
		List<string> changes;

		[TestMethod]
		public void TestChangeNotification()
		{
			// Create a list to track property changes
			changes = new List<string>();

			// Create a new Milk instance
			var milk = new Milk();
			
			// Record property changes
			((INotifyPropertyChanged)milk).PropertyChanged += (o, args) => changes.Add(args.PropertyName);

			// Change three properties
			milk.Brand = "Publix";
			milk.Gallons = 1;
			milk.Percent = 0; // Will not cause a change to be recorded

			// Verify that the changes were recorded correctly
			Assert.AreEqual(2, changes.Count);
			Assert.AreEqual("Brand", changes[0]);
			Assert.AreEqual("Gallons", changes[1]);
		}
	}
}
