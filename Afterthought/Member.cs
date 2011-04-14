//-----------------------------------------------------------------------------
//
// Copyright (c) VC3, Inc. All rights reserved.
// This code is licensed under the Microsoft Public License.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//-----------------------------------------------------------------------------

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
		public abstract class Member : IMemberAmendment
		{
			internal Member(string name)
			{
				this.Name = name;
				this.Attributes = new List<IAttributeAmendment>();
			}

			public string Name { get; protected set; }

			public override string ToString()
			{
				return Name;
			}

			public virtual bool IsAmended { get { return Attributes.Any(); } }

			IEnumerable<IAttributeAmendment> IMemberAmendment.Attributes { get { return Attributes; } }

			internal List<IAttributeAmendment> Attributes { get; set; }

			/// <summary>
			/// Add an Attribute to the collection of Attributes
			/// </summary>
			/// <param name="attribute"></param>
			public void AddAttribute(Attribute attribute)
			{
					Attributes.Add(attribute);
			}
		}
	}

	#endregion
}
