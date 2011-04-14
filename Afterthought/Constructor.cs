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

			public ConstructorInfo ConstructorInfo { get; private set; }

			public override bool IsAmended
			{
				get
				{
					return base.IsAmended || ImplementationMethod != null || BeforeMethod != null || AfterMethod != null;
				}
			}

			MethodInfo IConstructorAmendment.Implementation { get { return ImplementationMethod; } }

			MethodInfo IConstructorAmendment.Before { get { return BeforeMethod; } }

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
			Constructor(string name)
				: base(name)
			{ }

			internal Constructor(ConstructorInfo constructor)
				: base(constructor)
			{ }

			#region Create

			public delegate void CreateConstructor(TAmended instance, string method);

			public delegate void CreateConstructor<P>(TAmended instance, string method, P parameters);

			public static Constructor Create(string name, CreateConstructor create)
			{
				return new Constructor(name) { ImplementationMethod = create.Method };
			}

			public static Constructor Create<P1>(string name, CreateConstructor<Parameter<P1>> create)
			{
				return new Constructor(name) { ImplementationMethod = create.Method };
			}

			public static Constructor Create<P1, P2>(string name, CreateConstructor<Parameter<P1, P2>> create)
			{
				return new Constructor(name) { ImplementationMethod = create.Method };
			}

			public static Constructor Create<P1, P2, P3>(string name, CreateConstructor<Parameter<P1, P2, P3>> create)
			{
				return new Constructor(name) { ImplementationMethod = create.Method };
			}

			public static Constructor Create<P1, P2, P3, P4>(string name, CreateConstructor<Parameter<P1, P2, P3, P4>> create)
			{
				return new Constructor(name) { ImplementationMethod = create.Method };
			}

			public static Constructor Create<P1, P2, P3, P4, P5>(string name, CreateConstructor<Parameter<P1, P2, P3, P4, P5>> create)
			{
				return new Constructor(name) { ImplementationMethod = create.Method };
			}

			public static Constructor Create<P1, P2, P3, P4, P5, P6>(string name, CreateConstructor<Parameter<P1, P2, P3, P4, P5, P6>> create)
			{
				return new Constructor(name) { ImplementationMethod = create.Method };
			}

			public static Constructor Create<P1, P2, P3, P4, P5, P6, P7>(string name, CreateConstructor<Parameter<P1, P2, P3, P4, P5, P6, P7>> create)
			{
				return new Constructor(name) { ImplementationMethod = create.Method };
			}

			public static Constructor Create<P1, P2, P3, P4, P5, P6, P7, P8>(string name, CreateConstructor<Parameter<P1, P2, P3, P4, P5, P6, P7, P8>> create)
			{
				return new Constructor(name) { ImplementationMethod = create.Method };
			}

			#endregion

			#region Implement

			public delegate void ImplementConstructor(TAmended instance, string method);

			public delegate void ImplementConstructor<P>(TAmended instance, string method, P parameters);

			public void Implement(ImplementConstructor implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public void Implement<P1>(ImplementConstructor<Parameter<P1>> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public void Implement<P1, P2>(ImplementConstructor<Parameter<P1, P2>> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public void Implement<P1, P2, P3>(ImplementConstructor<Parameter<P1, P2, P3>> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public void Implement<P1, P2, P3, P4>(ImplementConstructor<Parameter<P1, P2, P3, P4>> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public void Implement<P1, P2, P3, P4, P5>(ImplementConstructor<Parameter<P1, P2, P3, P4, P5>> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public void Implement<P1, P2, P3, P4, P5, P6>(ImplementConstructor<Parameter<P1, P2, P3, P4, P5, P6>> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public void Implement<P1, P2, P3, P4, P5, P6, P7>(ImplementConstructor<Parameter<P1, P2, P3, P4, P5, P6, P7>> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public void Implement<P1, P2, P3, P4, P5, P6, P7, P8>(ImplementConstructor<Parameter<P1, P2, P3, P4, P5, P6, P7, P8>> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			#endregion

			#region Before

			public new delegate void BeforeConstructor(TAmended instance, string method);

			public delegate P BeforeConstructor<P>(TAmended instance, string method, P parameters);

			public void Before(BeforeConstructor before)
			{
				base.BeforeMethod = before.Method;
			}

			public void Before(BeforeConstructor<object[]> before)
			{
				base.BeforeMethod = before.Method;
			}

			public void Before<P1>(BeforeConstructor<Parameter<P1>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 1)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2>(BeforeConstructor<Parameter<P1, P2>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 2)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2, P3>(BeforeConstructor<Parameter<P1, P2, P3>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 3)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2, P3, P4>(BeforeConstructor<Parameter<P1, P2, P3, P4>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 4)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2, P3, P4, P5>(BeforeConstructor<Parameter<P1, P2, P3, P4, P5>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 5)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2, P3, P4, P5, P6>(BeforeConstructor<Parameter<P1, P2, P3, P4, P5, P6>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 6)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2, P3, P4, P5, P6, P7>(BeforeConstructor<Parameter<P1, P2, P3, P4, P5, P6, P7>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 7)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2, P3, P4, P5, P6, P7, P8>(BeforeConstructor<Parameter<P1, P2, P3, P4, P5, P6, P7, P8>> before)
			{
				if (ConstructorInfo.GetParameters().Length != 8)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			#endregion

			#region After

			public delegate void AfterConstructor(TAmended instance, string method);

			public delegate void AfterConstructor<P>(TAmended instance, string method, P parameters);

			public void After(AfterConstructor after)
			{
				AfterMethod = after.Method;
			}

			public void After(AfterConstructor<object[]> after)
			{
				AfterMethod = after.Method;
			}

			public void After<P1>(AfterConstructor<Parameter<P1>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 1)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2>(AfterConstructor<Parameter<P1, P2>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 2)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3>(AfterConstructor<Parameter<P1, P2, P3>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 3)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4>(AfterConstructor<Parameter<P1, P2, P3, P4>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 4)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, P5>(AfterConstructor<Parameter<P1, P2, P3, P4, P5>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 5)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, P5, P6>(AfterConstructor<Parameter<P1, P2, P3, P4, P5, P6>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 6)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, P5, P6, P7>(AfterConstructor<Parameter<P1, P2, P3, P4, P5, P6, P7>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 7)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, P5, P6, P7, P8>(AfterConstructor<Parameter<P1, P2, P3, P4, P5, P6, P7, P8>> after)
			{
				if (ConstructorInfo.GetParameters().Length != 8)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			#endregion
		}
	}

	#endregion
}
