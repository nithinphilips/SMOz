using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Afterthought
{
	public interface IMethodAmendment
	{
		string Name { get; }

		Type ReturnType { get; }

		MethodInfo MethodInfo { get; }

		MethodInfo Implements { get; }

		MethodInfo Implementation { get; }

		MethodInfo Before { get; }

		MethodInfo After { get; }
	}
}
