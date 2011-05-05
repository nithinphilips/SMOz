using System;
using System.Collections.Generic;
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
			if (target.GetCustomAttributes(typeof(NotifyPropertyChangedAttribute), true).Length > 0)
				yield return (ITypeAmendment)typeof(NotificationAmendment<>).MakeGenericType(target).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);

		}
	}
}
