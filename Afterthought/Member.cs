using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Afterthought
{
	#region Amendment.Member

	/// <summary>
	/// Abstract base class for concrete <see cref="Amendment<TType, TAmended>"/> which supports amending code into 
	/// a specific <see cref="Type"/> during compilation.
	/// </summary>
	public abstract partial class Amendment
	{
		public abstract class Member 
		{
			internal Member(string name)
			{
				this.Name = name;
			}

			public string Name { get; protected set; }

			public override string ToString()
			{
				return Name;
			}

			public abstract bool IsAmended { get; }
		}
	}

	#endregion
}
