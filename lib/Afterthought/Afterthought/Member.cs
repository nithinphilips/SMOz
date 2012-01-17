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
using System.Collections;

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
				this.Attributes = new AttributeList();
			}

			public string Name { get; protected set; }

			public override string ToString()
			{
				return Name;
			}

			public virtual bool IsAmended { get { return Attributes.Cast<IAttributeAmendment>().Any(); } }

			IEnumerable<IAttributeAmendment> IMemberAmendment.Attributes { get { return Attributes.Cast<IAttributeAmendment>(); } }

			public AttributeList Attributes { get; private set; }
		}

		public abstract class MemberEnumeration<TEnumeration>
			where TEnumeration : MemberEnumeration<TEnumeration>, IEnumerable
		{
			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public TEnumeration AddAttribute<TAttribute>()
				where TAttribute : System.Attribute
			{
				foreach (Member member in (TEnumeration)this)
					member.Attributes.Add<TAttribute>();
				return (TEnumeration)this;
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public TEnumeration AddAttribute<TAttribute, P1>(P1 value1)
				where TAttribute : System.Attribute
			{
				foreach (Member member in (IEnumerable)this)
					member.Attributes.Add<TAttribute, P1>(value1);
				return (TEnumeration)this;
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public TEnumeration AddAttribute<TAttribute, P1, P2>(P1 value1, P2 value2)
				where TAttribute : System.Attribute
			{
				foreach (Member member in (IEnumerable)this)
					member.Attributes.Add<TAttribute, P1, P2>(value1, value2);
				return (TEnumeration)this;
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public TEnumeration AddAttribute<TAttribute, P1, P2, P3>(P1 value1, P2 value2, P3 value3)
				where TAttribute : System.Attribute
			{
				foreach (Member member in (IEnumerable)this)
					member.Attributes.Add<TAttribute, P1, P2, P3>(value1, value2, value3);
				return (TEnumeration)this;
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public TEnumeration AddAttribute<TAttribute, P1, P2, P3, P4>(P1 value1, P2 value2, P3 value3, P4 value4)
				where TAttribute : System.Attribute
			{
				foreach (Member member in (IEnumerable)this)
					member.Attributes.Add<TAttribute, P1, P2, P3, P4>(value1, value2, value3, value4);
				return (TEnumeration)this;
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public TEnumeration AddAttribute<TAttribute, P1, P2, P3, P4, P5>(P1 value1, P2 value2, P3 value3, P4 value4, P5 value5)
				where TAttribute : System.Attribute
			{
				foreach (Member member in (IEnumerable)this)
					member.Attributes.Add<TAttribute, P1, P2, P3, P4, P5>(value1, value2, value3, value4, value5);
				return (TEnumeration)this;
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public TEnumeration AddAttribute<TAttribute, P1, P2, P3, P4, P5, P6>(P1 value1, P2 value2, P3 value3, P4 value4, P5 value5, P6 value6)
				where TAttribute : System.Attribute
			{
				foreach (Member member in (IEnumerable)this)
					member.Attributes.Add<TAttribute, P1, P2, P3, P4, P5, P6>(value1, value2, value3, value4, value5, value6);
				return (TEnumeration)this;
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public TEnumeration AddAttribute<TAttribute, P1, P2, P3, P4, P5, P6, P7>(P1 value1, P2 value2, P3 value3, P4 value4, P5 value5, P6 value6, P7 value7)
				where TAttribute : System.Attribute
			{
				foreach (Member member in (IEnumerable)this)
					member.Attributes.Add<TAttribute, P1, P2, P3, P4, P5, P6, P7>(value1, value2, value3, value4, value5, value6, value7);
				return (TEnumeration)this;
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public TEnumeration AddAttribute<TAttribute, P1, P2, P3, P4, P5, P6, P7, P8>(P1 value1, P2 value2, P3 value3, P4 value4, P5 value5, P6 value6, P7 value7, P8 value8)
				where TAttribute : System.Attribute
			{
				foreach (Member member in (IEnumerable)this)
					member.Attributes.Add<TAttribute, P1, P2, P3, P4, P5, P6, P7, P8>(value1, value2, value3, value4, value5, value6, value7, value8);
				return (TEnumeration)this;
			}
		}
	}

	#endregion
}
