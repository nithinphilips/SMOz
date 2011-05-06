using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace NotifyPropertyChanged.UnitTest.Target
{
	/// <summary>
	/// Example of a type that already implements <see cref="INotifyPropertyChanged"/>.
	/// </summary>
	[NotifyPropertyChanged]
	public class Soda : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public int Cans { get; set; }

		string type;
		public string Type
		{
			get
			{
				return type;
			}
			set
			{
				if (type != value)
				{
					type = value;
					PropertyChanged(this, new PropertyChangedEventArgs("Type"));
				}
			}
		}
	}
}
