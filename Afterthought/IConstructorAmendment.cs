using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Afterthought
{
	public interface IConstructorAmendment
	{
		string Name { get; }

		ConstructorInfo ConstructorInfo { get; }

		MethodInfo Implementation { get; }

		MethodInfo Before { get; }

		MethodInfo After { get; }
	}
}
