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
	public interface ITypeAmendment
	{
		Type Type { get; }

		IEnumerable<Type> Interfaces { get; }

		IEnumerable<IConstructorAmendment> Constructors { get; }

		IEnumerable<IFieldAmendment> Fields { get; }

		IEnumerable<IPropertyAmendment> Properties { get; }

		IEnumerable<IMethodAmendment> Methods { get; }
	}
}
