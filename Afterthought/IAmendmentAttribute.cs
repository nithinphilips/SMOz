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
	/// Interface implemented by attributes that produce <see cref="ITypeAmendment"/> instances
	/// for types in an assembly.
	/// </summary>
	public interface IAmendmentAttribute
	{
		/// <summary>
		/// Identifies the set of <see cref="ITypeAmendment"/> instances to apply based on the type
		/// the attribute was applied to.
		/// </summary>
		IEnumerable<ITypeAmendment> GetAmendments(Type target);
	}
}
