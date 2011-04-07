using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Afterthought
{
	public interface ITypeAmendment
	{
		Type Type { get; }

		IEnumerable<Type> Interfaces { get; }

		IEnumerable<IConstructorAmendment> Constructors { get; }

		IEnumerable<IFieldAmendment> Fields { get; }

		IEnumerable<IPropertyAmendment> Properties { get; }

		IEnumerable<IMethodAmendment> Methods { get; }
	}
}
