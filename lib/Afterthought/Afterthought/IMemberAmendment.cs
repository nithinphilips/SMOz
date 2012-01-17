using System;
using System.Collections.Generic;
namespace Afterthought
{
	public interface IMemberAmendment
	{
		string Name { get; }

		IEnumerable<IAttributeAmendment> Attributes { get; }
	}
}
