using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Afterthought
{
	public interface IAttributeAmendment
	{
		// Type of the attribute
		Type Type { get; }

		// Constructor for the attribute
		ConstructorInfo Constructor { get; }

		// Optional arguments to pass to the constructor
		object[] Arguments { get; }
	}
}
