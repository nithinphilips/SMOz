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

namespace Afterthought
{
	public interface IEventAmendment : IMemberAmendment
	{
		MethodInfo Adder { get; }

		MethodInfo AfterAdd { get; }
		
		MethodInfo AfterRemove { get; }
		
		MethodInfo BeforeAdd { get; }
		
		MethodInfo BeforeRemove { get; }
		
		EventInfo EventInfo { get; }
		
		EventInfo Implements { get; }
		
		MethodInfo Remover { get; }
	}
}
