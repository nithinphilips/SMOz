using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotifyPropertyChanged.UnitTest.Target
{
	/// <summary>
	/// A Milk class ;)
	/// </summary>
	[NotifyPropertyChanged]
	public class Milk
	{
		public decimal Percent { get; set; }

		public string Brand { get; set; }

		public decimal Gallons { get; set; }
	}
}
