using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Afterthought
{
	#region Amendment.InterfaceMember

	/// <summary>
	/// Abstract base class for concrete <see cref="Amendment<TType, TAmended>"/> which supports amending code into 
	/// a specific <see cref="Type"/> during compilation.
	/// </summary>
	public abstract partial class Amendment
	{
		/// <summary>
		/// Marker base class for properties, methods, and events indicating that
		/// they are members that may implement interfaces.
		/// </summary>
		public abstract class InterfaceMember : Member
		{
			internal InterfaceMember(string name)
				: base(name)
			{ }
		}
	}

	#endregion
}
