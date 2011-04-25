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

			public static Constructor Create(string name, ImplementConstructor implementation)
			{
				return new Constructor(name) { ImplementationMethod = implementation.Method };
			}

			public static Constructor Create<P1>(string name, ImplementConstructor<P1> implementation)
			{
				return new Constructor(name) { ImplementationMethod = implementation.Method };
			}

			public static Constructor Create<P1, P2>(string name, ImplementConstructor<P1, P2> implementation)
			{
				return new Constructor(name) { ImplementationMethod = implementation.Method };
			}

			public static Constructor Create<P1, P2, P3>(string name, ImplementConstructor<P1, P2, P3> implementation)
			{
				return new Constructor(name) { ImplementationMethod = implementation.Method };
			}

			public static Constructor Create<P1, P2, P3, P4>(string name, ImplementConstructor<P1, P2, P3, P4> implementation)
			{
				return new Constructor(name) { ImplementationMethod = implementation.Method };
			}

			public static Constructor Create<P1, P2, P3, P4, P5>(string name, ImplementConstructor<P1, P2, P3, P4, P5> implementation)
			{
				return new Constructor(name) { ImplementationMethod = implementation.Method };
			}

			public static Constructor Create<P1, P2, P3, P4, P5, P6>(string name, ImplementConstructor<P1, P2, P3, P4, P5, P6> implementation)
			{
				return new Constructor(name) { ImplementationMethod = implementation.Method };
			}

			public static Constructor Create<P1, P2, P3, P4, P5, P6, P7>(string name, ImplementConstructor<P1, P2, P3, P4, P5, P6, P7> implementation)
			{
				return new Constructor(name) { ImplementationMethod = implementation.Method };
			}

			public static Constructor Create<P1, P2, P3, P4, P5, P6, P7, P8>(string name, ImplementConstructor<P1, P2, P3, P4, P5, P6, P7, P8> implementation)
			{
				return new Constructor(name) { ImplementationMethod = implementation.Method };
			}

			#endregion

			#region Implement

			public delegate void ImplementConstructor(TAmended instance);

			public void Implement(ImplementConstructor implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementConstructor<P1>(TAmended instance, P1 param1);

			public void Implement<P1>(ImplementConstructor<P1> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementConstructor<P1, P2>(TAmended instance, P1 param1, P2 param2);

			public void Implement<P1, P2>(ImplementConstructor<P1, P2> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementConstructor<P1, P2, P3>(TAmended instance, P1 param1, P2 param2, P3 param3);

			public void Implement<P1, P2, P3>(ImplementConstructor<P1, P2, P3> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementConstructor<P1, P2, P3, P4>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4);

			public void Implement<P1, P2, P3, P4>(ImplementConstructor<P1, P2, P3, P4> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementConstructor<P1, P2, P3, P4, P5>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5);

			public void Implement<P1, P2, P3, P4, P5>(ImplementConstructor<P1, P2, P3, P4, P5> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementConstructor<P1, P2, P3, P4, P5, P6>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6);

			public void Implement<P1, P2, P3, P4, P5, P6>(ImplementConstructor<P1, P2, P3, P4, P5, P6> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementConstructor<P1, P2, P3, P4, P5, P6, P7>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, P7 param7);

			public void Implement<P1, P2, P3, P4, P5, P6, P7>(ImplementConstructor<P1, P2, P3, P4, P5, P6, P7> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementConstructor<P1, P2, P3, P4, P5, P6, P7, P8>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, P7 param7, P8 param8);

			public void Implement<P1, P2, P3, P4, P5, P6, P7, P8>(ImplementConstructor<P1, P2, P3, P4, P5, P6, P7, P8> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			#endregion

			#region Before

			public delegate void BeforeConstructorArray(TAmended instance, string method, object[] parameters);

			public void Before(BeforeConstructorArray before)
			{
				BeforeMethod = before.Method;
			}

			public new delegate void BeforeConstructor(TAmended instance);

			public void Before(BeforeConstructor before)
			{
				BeforeMethod = before.Method;
			}

			public delegate void BeforeConstructor<P1>(TAmended instance, ref P1 param1);

			public void Before<P1>(BeforeConstructor<P1> before)
			{
				BeforeMethod = before.Method;
			}

			public delegate void BeforeConstructor<P1, P2>(TAmended instance, ref P1 param1, ref P2 param2);

			public void Before<P1, P2>(BeforeConstructor<P1, P2> before)
			{
				BeforeMethod = before.Method;
			}

			public delegate void BeforeConstructor<P1, P2, P3>(TAmended instance, ref P1 param1, ref P2 param2, ref P3 param3);

			public void Before<P1, P2, P3>(BeforeConstructor<P1, P2, P3> before)
			{
				BeforeMethod = before.Method;
			}

			public delegate void BeforeConstructor<P1, P2, P3, P4>(TAmended instance, ref P1 param1, ref P2 param2, ref P3 param3, ref P4 param4);

			public void Before<P1, P2, P3, P4>(BeforeConstructor<P1, P2, P3, P4> before)
			{
				BeforeMethod = before.Method;
			}

			public delegate void BeforeConstructor<P1, P2, P3, P4, P5>(TAmended instance, ref P1 param1, ref P2 param2, ref P3 param3, ref P4 param4, ref P5 param5);

			public void Before<P1, P2, P3, P4, P5>(BeforeConstructor<P1, P2, P3, P4, P5> before)
			{
				BeforeMethod = before.Method;
			}

			public delegate void BeforeConstructor<P1, P2, P3, P4, P5, P6>(TAmended instance, ref P1 param1, ref P2 param2, ref P3 param3, ref P4 param4, ref P5 param5, ref P6 param6);

			public void Before<P1, P2, P3, P4, P5, P6>(BeforeConstructor<P1, P2, P3, P4, P5, P6> before)
			{
				BeforeMethod = before.Method;
			}

			public delegate void BeforeConstructor<P1, P2, P3, P4, P5, P6, P7>(TAmended instance, ref P1 param1, ref P2 param2, ref P3 param3, ref P4 param4, ref P5 param5, ref P6 param6, ref P7 param7);

			public void Before<P1, P2, P3, P4, P5, P6, P7>(BeforeConstructor<P1, P2, P3, P4, P5, P6, P7> before)
			{
				BeforeMethod = before.Method;
			}

			public delegate void BeforeConstructor<P1, P2, P3, P4, P5, P6, P7, P8>(TAmended instance, ref P1 param1, ref P2 param2, ref P3 param3, ref P4 param4, ref P5 param5, ref P6 param6, ref P7 param7, ref P8 param8);

			public void Before<P1, P2, P3, P4, P5, P6, P7, P8>(BeforeConstructor<P1, P2, P3, P4, P5, P6, P7, P8> before)
			{
				BeforeMethod = before.Method;
			}

			#endregion

			#region After

			public delegate void AfterConstructorArray(TAmended instance, string method, object[] parameters);

			public void After(AfterConstructorArray after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterConstructor(TAmended instance);

			public void After(AfterConstructor after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterConstructor<P1>(TAmended instance, P1 param1);

			public void After<P1>(AfterConstructor<P1> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterConstructor<P1, P2>(TAmended instance, P1 param1, P2 param2);

			public void After<P1, P2>(AfterConstructor<P1, P2> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterConstructor<P1, P2, P3>(TAmended instance, P1 param1, P2 param2, P3 param3);

			public void After<P1, P2, P3>(AfterConstructor<P1, P2, P3> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterConstructor<P1, P2, P3, P4>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4);

			public void After<P1, P2, P3, P4>(AfterConstructor<P1, P2, P3, P4> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterConstructor<P1, P2, P3, P4, P5>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5);

			public void After<P1, P2, P3, P4, P5>(AfterConstructor<P1, P2, P3, P4, P5> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterConstructor<P1, P2, P3, P4, P5, P6>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6);

			public void After<P1, P2, P3, P4, P5, P6>(AfterConstructor<P1, P2, P3, P4, P5, P6> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterConstructor<P1, P2, P3, P4, P5, P6, P7>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, P7 param7);

			public void After<P1, P2, P3, P4, P5, P6, P7>(AfterConstructor<P1, P2, P3, P4, P5, P6, P7> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterConstructor<P1, P2, P3, P4, P5, P6, P7, P8>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, P7 param7, P8 param8);

			public void After<P1, P2, P3, P4, P5, P6, P7, P8>(AfterConstructor<P1, P2, P3, P4, P5, P6, P7, P8> after)
			{
				AfterMethod = after.Method;
			}

			#endregion
		}
	}

	#endregion
}
