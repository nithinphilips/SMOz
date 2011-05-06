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

using System.Reflection;
using System;

namespace Afterthought
{
	public interface IEventAmendment : IMemberAmendment
	{
		Type Type { get; }

		EventInfo EventInfo { get; }

		EventInfo Implements { get; }

		MethodInfo Adder { get; }

		MethodInfo Remover { get; }
		
		MethodInfo BeforeAdd { get; }
		
		MethodInfo BeforeRemove { get; }

		MethodInfo AfterAdd { get; }

		MethodInfo AfterRemove { get; }
	}
}
