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
	public interface IMethodAmendment : IMemberAmendment
	{
		Type ReturnType { get; }

		MethodInfo MethodInfo { get; }

		MethodInfo Implements { get; }

		MethodInfo Overrides { get; }

		MethodInfo Implementation { get; }

		MethodInfo Before { get; }

		MethodInfo After { get; }

		MethodInfo Catch { get; }

		MethodInfo Finally { get; }

		IEventAmendment Raises { get; }
	}
}
