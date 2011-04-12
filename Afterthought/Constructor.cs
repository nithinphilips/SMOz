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
			public Constructor(string name, Delegate implementation)
				: base(name)
			{ }

			internal Constructor(ConstructorInfo constructor)
				: base(constructor.Name)
			{
				this.ConstructorInfo = constructor;
			}

			public ConstructorInfo ConstructorInfo { get; private set; }

			public override bool IsAmended
			{
				get
				{
					return ImplementationMethod != null || BeforeMethod != null || AfterMethod != null;
				}
			}

			MethodInfo IConstructorAmendment.Implementation { get { return ImplementationMethod; } }

			MethodInfo IConstructorAmendment.Before { get {	return BeforeMethod; } }

			MethodInfo IConstructorAmendment.After { get { return AfterMethod; } }

			internal MethodInfo ImplementationMethod { get; set; }

			internal MethodInfo BeforeMethod { get; set; }

			internal MethodInfo AfterMethod { get; set; }
		}
	}

	#endregion

	#region Amendment<TType, TAmended>.Constructor

	public partial class Amendment<TType, TAmended> : Amendment
	{
		/// <summary>
		/// Represents a constructor on a type.
		/// </summary>
		public new class Constructor : Amendment.Constructor
		{
			public Constructor(string name, Delegate implementation)
				: base(name, implementation)
			{ }

			internal Constructor(ConstructorInfo constructor)
				: base(constructor)
			{ }

			#region Before

			public delegate void BeforeConstructor(TAmended instance, string constructor);

			public delegate A BeforeConstructor<A>(TAmended instance, string constructor, A arguments);

			public void Before(BeforeConstructor before)
			{
				base.BeforeMethod = before.Method;
			}

			public void Before(BeforeConstructor<object[]> before)
			{
				base.BeforeMethod = before.Method;
			}

			public void Before<A1>(BeforeConstructor<Parameter<A1>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 1)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<A1, A2>(BeforeConstructor<Parameter<A1, A2>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 2)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<A1, A2, A3>(BeforeConstructor<Parameter<A1, A2, A3>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 3)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<A1, A2, A3, A4>(BeforeConstructor<Parameter<A1, A2, A3, A4>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 4)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<A1, A2, A3, A4, A5>(BeforeConstructor<Parameter<A1, A2, A3, A4, A5>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 5)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<A1, A2, A3, A4, A5, A6>(BeforeConstructor<Parameter<A1, A2, A3, A4, A5, A6>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 6)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<A1, A2, A3, A4, A5, A6, A7>(BeforeConstructor<Parameter<A1, A2, A3, A4, A5, A6, A7>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 7)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<A1, A2, A3, A4, A5, A6, A7, A8>(BeforeConstructor<Parameter<A1, A2, A3, A4, A5, A6, A7, A8>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 8)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				base.BeforeMethod = before.Method;
			}

			#endregion

			#region After

			public delegate void AfterConstructor(TAmended instance, string constructor);

			public delegate void AfterConstructor<A>(TAmended instance, string constructor, A arguments);

			public void After(AfterConstructor after)
			{
				AfterMethod = after.Method;
			}

			public void After(AfterConstructor<object[]> after)
			{
				AfterMethod = after.Method;
			}

			public void After<A1>(AfterConstructor<Parameter<A1>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 1)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				AfterMethod = after.Method;
			}

			public void After<A1, A2>(AfterConstructor<Parameter<A1, A2>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 2)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				AfterMethod = after.Method;
			}

			public void After<A1, A2, A3>(AfterConstructor<Parameter<A1, A2, A3>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 3)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				AfterMethod = after.Method;
			}

			public void After<A1, A2, A3, A4>(AfterConstructor<Parameter<A1, A2, A3, A4>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 4)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				AfterMethod = after.Method;
			}

			public void After<A1, A2, A3, A4, A5>(AfterConstructor<Parameter<A1, A2, A3, A4, A5>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 5)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				AfterMethod = after.Method;
			}

			public void After<A1, A2, A3, A4, A5, A6>(AfterConstructor<Parameter<A1, A2, A3, A4, A5, A6>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 6)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				AfterMethod = after.Method;
			}

			public void After<A1, A2, A3, A4, A5, A6, A7>(AfterConstructor<Parameter<A1, A2, A3, A4, A5, A6, A7>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 7)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				AfterMethod = after.Method;
			}

			public void After<A1, A2, A3, A4, A5, A6, A7, A8>(AfterConstructor<Parameter<A1, A2, A3, A4, A5, A6, A7, A8>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 8)
					throw new ArgumentException("The number of parameters must match the constructor signature.");

				AfterMethod = after.Method;
			}

			#endregion
		}
	}

	#endregion
}
