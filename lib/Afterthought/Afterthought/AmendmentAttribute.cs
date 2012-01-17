﻿//-----------------------------------------------------------------------------
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
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Afterthought
{
	/// <summary>
	/// Identifies which types to amend in a target assembly and which <see cref="ITypeAmendment"/>
	/// implementation to create to describe the ammendments.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class AmendmentAttribute : System.Attribute, IAmendmentAttribute
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
		public static IEnumerable<ITypeAmendment> GetAmendments(params Assembly[] assemblies)
		{
			// Start by finding all assembly amenders
			var assemblyAmenders = assemblies.SelectMany(a => a.GetCustomAttributes(true)).OfType<IAmendmentAttribute>().ToArray();

			foreach (var assembly in assemblies)
			{
				foreach (var type in assembly.GetTypes())
				{
					// Process all type and assembly-level amendments
					foreach (var amendment in 
						type.GetCustomAttributes(true).OfType<IAmendmentAttribute>().SelectMany(attr => attr.GetAmendments(type))
						.Concat(assemblyAmenders.SelectMany(a => a.GetAmendments(type))))
					{
						yield return amendment;
					}
				}
			}
		}
	}
}
