using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Afterthought
{
	public interface IFieldAmendment
	{
		FieldInfo FieldInfo { get; }

		string Name { get; }

		Type Type { get; }

		MethodInfo Initializer { get; }
	}
}
