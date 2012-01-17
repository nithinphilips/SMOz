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
	#region Amendment.Method

	/// <summary>
	/// Abstract base class for concrete <see cref="Amendment<TType, TAmended>"/> which supports amending code into 
	/// a specific <see cref="Type"/> during compilation.
	/// </summary>
	public abstract partial class Amendment
	{
		public abstract class Method : InterfaceMember, IMethodAmendment
		{
			internal Method(string name)
				: base(name)
			{ }

			internal Method(MethodInfo method)
				: base(method.Name)
			{
				this.MethodInfo = method;
				this.ReturnType = method.ReturnType;
			}

			internal Method(Method method)
				: base(method.Name)
			{
				this.MethodInfo = method.MethodInfo;
				this.ReturnType = method.ReturnType;
			}

			public Type ReturnType { get; private set; }

			public MethodInfo MethodInfo { get; private set; }

			public override bool IsAmended
			{
				get
				{
					return base.IsAmended || ImplementationMethod != null || 
						BeforeMethod != null || AfterMethod != null || CatchMethod != null || FinallyMethod != null || 
						RaisesEvent != null;
				}
			}

			MethodInfo IMethodAmendment.Implements { get { return Implements; } }

			MethodInfo IMethodAmendment.Overrides { get { return OverrideMethod; } }

			MethodInfo IMethodAmendment.Implementation { get { return ImplementationMethod; } }

			MethodInfo IMethodAmendment.Before { get { return BeforeMethod; } }

			MethodInfo IMethodAmendment.After { get { return AfterMethod; } }

			MethodInfo IMethodAmendment.Catch { get { return CatchMethod; } }

			MethodInfo IMethodAmendment.Finally { get { return FinallyMethod; } }

			IEventAmendment IMethodAmendment.Raises { get { return RaisesEvent; } }

			MethodInfo implements;
			internal MethodInfo Implements
			{
				get
				{
					return implements;
				}
				set
				{
					if (implements != null)
						throw new InvalidOperationException("The method implementation may only be set once.");
					implements = value;
					if (implements != null)
						Name = implements.DeclaringType.FullName + "." + implements.Name;
				}
			}

			MethodInfo overrideMethod;
			internal MethodInfo OverrideMethod
			{
				get
				{
					return overrideMethod;
				}
				set
				{
					if (overrideMethod != null)
						throw new InvalidOperationException("The override method may only be set once.");
					overrideMethod = value;
				}
			}

			internal MethodInfo ImplementationMethod { get; set; }

			internal MethodInfo BeforeMethod { get; set; }

			internal MethodInfo AfterMethod { get; set; }

			internal MethodInfo CatchMethod { get; set; }

			internal MethodInfo FinallyMethod { get; set; }

			internal Event RaisesEvent { get; set; }
		}
	}

	#endregion
}