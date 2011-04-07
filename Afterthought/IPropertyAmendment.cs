using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Afterthought
{
	public interface IPropertyAmendment
	{
		string Name { get; }

		Type Type { get; }

		PropertyInfo PropertyInfo { get; }

		PropertyInfo Implements { get; }

		MethodInfo LazyInitializer { get; }

		MethodInfo Initializer { get; }

		MethodInfo Getter { get; }

		MethodInfo Setter { get; }

		MethodInfo BeforeGet { get; }

		MethodInfo AfterGet { get; }

		MethodInfo BeforeSet { get; }

		MethodInfo AfterSet { get; }

	}
}
