using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;

namespace Afterthought
{
	public partial class Amendment
	{
		#region Attribute

		public abstract class Attribute : IAttributeAmendment
		{
			internal Attribute()
			{ }

			public abstract Type Type { get; }

			object[] IAttributeAmendment.Arguments { get { return Arguments; } }

			ConstructorInfo IAttributeAmendment.Constructor { get { return Constructor; } }

			internal object[] Arguments { get; set; }

			internal ConstructorInfo Constructor { get; set; }
		}

		#endregion

		#region Attribute<TAttribute>

		public class Attribute<TAttribute> : Attribute
			where TAttribute : System.Attribute
		{
			internal Attribute()
				: base()
			{ }

			public override Type Type
			{
				get
				{
					return typeof(TAttribute);
				}
			}
		}

		#endregion
	}
}
