using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotifyPropertyChanged
{
	/// <summary>
	/// Marker attribute indicating that a type should implement 
	/// <see cref="INotifyPropertyChanged"/> for all public properties.
	/// </summary>
	public class NotifyPropertyChangedAttribute : Attribute
	{ }
}
