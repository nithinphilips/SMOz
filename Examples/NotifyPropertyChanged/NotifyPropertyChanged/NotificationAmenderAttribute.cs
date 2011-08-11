using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Afterthought;

namespace NotifyPropertyChanged
{
	/// <summary>
	/// Attribute that enables Afterthought to identify and amend types that need to
	/// implement <see cref="INotifyPropertyChanged"/>.
	/// </summary>
	public class NotificationAmenderAttribute : Attribute, IAmendmentAttribute
	{
		IEnumerable<ITypeAmendment> IAmendmentAttribute.GetAmendments(Type target)
		{
            // Class does not implement INotifyPropertyChanged. Implement it for the user.
			if (target.GetCustomAttributes(typeof(NotifyPropertyChangedAttribute), true).Length > 0  
                && target.GetInterface("System.ComponentModel.INotifyPropertyChanged") == null)
				yield return (ITypeAmendment)typeof(NotificationAmendment<>).MakeGenericType(target).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);

            // Class implements INotifyPropertyChangedAmendment so that user can fire custom notificaitons
            if (target.GetCustomAttributes(typeof(NotifyPropertyChangedAttribute), true).Length > 0
                && target.GetInterface("NotifyPropertyChanged.INotifyPropertyChangedAmendment") != null)
                yield return (ITypeAmendment)typeof(SimpleNotificationAmendment<>).MakeGenericType(target).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);

		}
	}
}
