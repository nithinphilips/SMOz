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

namespace Afterthought
{
	/// <summary>
	/// Identifies which types to amend in a target assembly and which <see cref="ITypeAmendment"/>
	/// implementation to create to describe the ammendments.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class AmendmentAttribute : Attribute, IAmendmentAttribute
	{
		Type amendmentType;

		public AmendmentAttribute(Type amendmentType)
		{ 
			this.amendmentType = amendmentType;
		}

		/// <summary>
		/// Default implementation that assumes that the <see cref="AmendmentAttribute"/> will be applied to the
		/// type being amended, and that the amendment type will take the specified type as a generic type parameter.
		/// </summary>
		public virtual IEnumerable<ITypeAmendment> GetAmendments(Type target)
		{
			yield return (ITypeAmendment)amendmentType.MakeGenericType(target).GetConstructor(Type.EmptyTypes).Invoke(null);
		}

		/// <summary>
		/// Gets the amendments defined in the specified <see cref="Assembly"/>.
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public static IEnumerable<ITypeAmendment> GetAmendments(System.Reflection.Assembly assembly)
		{
			foreach (var type in assembly.GetTypes())
			{
				foreach (var amendment in type.GetCustomAttributes(true).OfType<IAmendmentAttribute>().SelectMany(attr => attr.GetAmendments(type)))
				{
					if (amendment is Amendment)
						((Amendment)amendment).Initialize();
					yield return amendment;
				}
			}
		}
	}
}
