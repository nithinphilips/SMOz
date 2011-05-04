using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afterthought;

namespace Logging.Amender
{
	/// <summary>
	/// Assembly-level amendment provider attribute indicating to Afterthought that
	/// all types in any amended assembly with the <see cref="LoggingAttribute"/> applied
	/// should be amended/logged.
	/// </summary>
	[AttributeUsage(AttributeTargets.Assembly)]
	public class LoggingAmenderAttribute : Attribute, IAmendmentAttribute
	{
		IEnumerable<ITypeAmendment> IAmendmentAttribute.GetAmendments(Type target)
		{
			if (target.GetCustomAttributes(typeof(LoggingAttribute), true).Length > 0)
				yield return (ITypeAmendment)typeof(LoggingAmender<>).MakeGenericType(target).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
		}
	}
}
