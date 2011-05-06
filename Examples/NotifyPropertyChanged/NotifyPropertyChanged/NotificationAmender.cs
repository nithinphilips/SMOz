using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace NotifyPropertyChanged
{
	/// <summary>
	/// Static class containing a method that will be called after each property set.
	/// This method in outside of the Amendment<,> subclass to ensure that the amended type
	/// will not have a runtime dependency on Afterthought.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static class NotificationAmender<T>
	{
		public static void OnPropertyChanged<P>(INotifyPropertyChangedAmendment instance, string property, P oldValue, P value, P newValue)
		{
			// Only raise property changed if the value of the property actually changed
			if ((oldValue == null ^ newValue == null) || (oldValue != null && !oldValue.Equals(newValue)))
				instance.OnPropertyChanged(new PropertyChangedEventArgs(property));
		}
	}

	/// <summary>
	/// Amendment class that instructs Afterthought to implement INotifyPropertyChanged for the target types.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class NotificationAmendment<T> : Afterthought.Amendment<T, INotifyPropertyChangedAmendment>
	{
		public override void Amend()
		{
			// Create the PropertyChanged event
			var propertyChanged = new Event<PropertyChangedEventHandler>("PropertyChanged");

			// Implement INotifyPropertyChanged, specifying the PropertyChanged event
			ImplementInterface<INotifyPropertyChanged>(propertyChanged);

			// Implement INotifyPropertyChangedAmendment, specifying a method that raises the PropertyChanged event
			ImplementInterface<INotifyPropertyChangedAmendment>(
				propertyChanged.RaisedBy("OnPropertyChanged")
			);
		}

		public override void Amend<TProperty>(Property<TProperty> property)
		{
			// Raise property change notifications
			if (property.PropertyInfo.CanRead && property.PropertyInfo.CanWrite && property.PropertyInfo.GetSetMethod().IsPublic)
				property.AfterSet = NotificationAmender<T>.OnPropertyChanged<TProperty>;
		}
	}
}
