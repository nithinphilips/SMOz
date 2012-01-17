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
	#region Amendment.Constructor

	/// <summary>
	/// Abstract base class for concrete <see cref="Amendment<TType, TAmended>"/> which supports amending code into 
	/// a specific <see cref="Type"/> during compilation.
	/// </summary>
	public abstract partial class Amendment
	{
		public abstract class Constructor : Member, IConstructorAmendment
		{
			public Constructor(string name)
				: base(name)
			{ }

			internal Constructor(ConstructorInfo constructor)
				: base(constructor.Name)
			{
				this.ConstructorInfo = constructor;
			}

			internal Constructor(Constructor constructor)
				: base(constructor.Name)
			{
				this.ConstructorInfo = constructor.ConstructorInfo;
			}

			public ConstructorInfo ConstructorInfo { get; private set; }

			public override bool IsAmended
			{
				get
				{
					return base.IsAmended || ImplementationMethod != null || BeforeMethod != null || AfterMethod != null || CatchMethod != null || FinallyMethod != null;
				}
			}

			MethodInfo IConstructorAmendment.Implementation { get { return ImplementationMethod; } }

			MethodInfo IConstructorAmendment.Before { get { return BeforeMethod; } }

			MethodInfo IConstructorAmendment.After { get { return AfterMethod; } }

			MethodInfo IConstructorAmendment.Catch { get { return CatchMethod; } }

			MethodInfo IConstructorAmendment.Finally { get { return FinallyMethod; } }

			internal MethodInfo ImplementationMethod { get; set; }

			internal MethodInfo BeforeMethod { get; set; }

			internal MethodInfo AfterMethod { get; set; }

			internal MethodInfo CatchMethod { get; set; }

			internal MethodInfo FinallyMethod { get; set; }
		}
	}

	#endregion
}
