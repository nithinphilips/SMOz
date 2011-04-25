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

			public Type ReturnType { get; private set; }

			public MethodInfo MethodInfo { get; private set; }

			public override bool IsAmended
			{
				get
				{
					return base.IsAmended || ImplementationMethod != null || BeforeMethod != null || AfterMethod != null;
				}
			}

			MethodInfo IMethodAmendment.Implements { get { return Implements; } }

			MethodInfo IMethodAmendment.Implementation { get { return ImplementationMethod; } }

			MethodInfo IMethodAmendment.Before { get { return BeforeMethod; } }

			MethodInfo IMethodAmendment.After { get { return AfterMethod; } }

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

			internal MethodInfo ImplementationMethod { get; set; }

			internal MethodInfo BeforeMethod { get; set; }

			internal MethodInfo AfterMethod { get; set; }
		}
	}

	#endregion

	#region Amendment<TType, TAmended>.Method

	public partial class Amendment<TType, TAmended> : Amendment
	{
		/// <summary>
		/// Represents an amendment for a new or existing method on a type.
		/// </summary>
		public new class Method : Amendment.Method
		{
			Method(string name)
				: base(name)
			{ }

			internal Method(MethodInfo method)
				: base(method)
			{ }

			#region Create (Action)

			public static Method Create(string name, ImplementMethodAction implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1>(string name, ImplementMethodAction<P1> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2>(string name, ImplementMethodAction<P1, P2> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, P3>(string name, ImplementMethodAction<P1, P2, P3> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, P3, P4>(string name, ImplementMethodAction<P1, P2, P3, P4> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5>(string name, ImplementMethodAction<P1, P2, P3, P4, P5> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, P6>(string name, ImplementMethodAction<P1, P2, P3, P4, P5, P6> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, P6, P7>(string name, ImplementMethodAction<P1, P2, P3, P4, P5, P6, P7> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, P6, P7, P8>(string name, ImplementMethodAction<P1, P2, P3, P4, P5, P6, P7, P8> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			#endregion

			#region Create (Func)

			public static Method Create<TResult>(string name, ImplementMethodFunc<TResult> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, TResult>(string name, ImplementMethodFunc<P1, TResult> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, TResult>(string name, ImplementMethodFunc<P1, P2, TResult> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, P3, TResult>(string name, ImplementMethodFunc<P1, P2, P3, TResult> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, P3, P4, TResult>(string name, ImplementMethodFunc<P1, P2, P3, P4, TResult> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, TResult>(string name, ImplementMethodFunc<P1, P2, P3, P4, P5, TResult> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, P6, TResult>(string name, ImplementMethodFunc<P1, P2, P3, P4, P5, P6, TResult> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, P6, P7, TResult>(string name, ImplementMethodFunc<P1, P2, P3, P4, P5, P6, P7, TResult> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, P6, P7, P8, TResult>(string name, ImplementMethodFunc<P1, P2, P3, P4, P5, P6, P7, P8, TResult> implementation)
			{
				return new Method(name) { ImplementationMethod = implementation.Method };
			}

			#endregion

			#region Implement (Action)

			public delegate void ImplementMethodAction(TAmended instance);

			public void Implement(ImplementMethodAction implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementMethodAction<P1>(TAmended instance, P1 param1);

			public void Implement<P1>(ImplementMethodAction<P1> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementMethodAction<P1, P2>(TAmended instance, P1 param1, P2 param2);

			public void Implement<P1, P2>(ImplementMethodAction<P1, P2> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementMethodAction<P1, P2, P3>(TAmended instance, P1 param1, P2 param2, P3 param3);

			public void Implement<P1, P2, P3>(ImplementMethodAction<P1, P2, P3> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementMethodAction<P1, P2, P3, P4>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4);

			public void Implement<P1, P2, P3, P4>(ImplementMethodAction<P1, P2, P3, P4> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementMethodAction<P1, P2, P3, P4, P5>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5);

			public void Implement<P1, P2, P3, P4, P5>(ImplementMethodAction<P1, P2, P3, P4, P5> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementMethodAction<P1, P2, P3, P4, P5, P6>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6);

			public void Implement<P1, P2, P3, P4, P5, P6>(ImplementMethodAction<P1, P2, P3, P4, P5, P6> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementMethodAction<P1, P2, P3, P4, P5, P6, P7>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, P7 param7);

			public void Implement<P1, P2, P3, P4, P5, P6, P7>(ImplementMethodAction<P1, P2, P3, P4, P5, P6, P7> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate void ImplementMethodAction<P1, P2, P3, P4, P5, P6, P7, P8>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, P7 param7, P8 param8);

			public void Implement<P1, P2, P3, P4, P5, P6, P7, P8>(ImplementMethodAction<P1, P2, P3, P4, P5, P6, P7, P8> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			#endregion

			#region Implement (Func)

			public delegate TResult ImplementMethodFunc<TResult>(TAmended instance);

			public void Implement<TResult>(ImplementMethodFunc<TResult> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate TResult ImplementMethodFunc<P1, TResult>(TAmended instance, P1 param1);

			public void Implement<P1, TResult>(ImplementMethodFunc<P1, TResult> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate TResult ImplementMethodFunc<P1, P2, TResult>(TAmended instance, P1 param1, P2 param2);

			public void Implement<P1, P2, TResult>(ImplementMethodFunc<P1, P2, TResult> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate TResult ImplementMethodFunc<P1, P2, P3, TResult>(TAmended instance, P1 param1, P2 param2, P3 param3);

			public void Implement<P1, P2, P3, TResult>(ImplementMethodFunc<P1, P2, P3, TResult> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate TResult ImplementMethodFunc<P1, P2, P3, P4, TResult>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4);

			public void Implement<P1, P2, P3, P4, TResult>(ImplementMethodFunc<P1, P2, P3, P4, TResult> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate TResult ImplementMethodFunc<P1, P2, P3, P4, P5, TResult>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5);

			public void Implement<P1, P2, P3, P4, P5, TResult>(ImplementMethodFunc<P1, P2, P3, P4, P5, TResult> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate TResult ImplementMethodFunc<P1, P2, P3, P4, P5, P6, TResult>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6);

			public void Implement<P1, P2, P3, P4, P5, P6, TResult>(ImplementMethodFunc<P1, P2, P3, P4, P5, P6, TResult> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate TResult ImplementMethodFunc<P1, P2, P3, P4, P5, P6, P7, TResult>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, P7 param7);

			public void Implement<P1, P2, P3, P4, P5, P6, P7, TResult>(ImplementMethodFunc<P1, P2, P3, P4, P5, P6, P7, TResult> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			public delegate TResult ImplementMethodFunc<P1, P2, P3, P4, P5, P6, P7, P8, TResult>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, P7 param7, P8 param8);

			public void Implement<P1, P2, P3, P4, P5, P6, P7, P8, TResult>(ImplementMethodFunc<P1, P2, P3, P4, P5, P6, P7, P8, TResult> implementation)
			{
				ImplementationMethod = implementation.Method;
			}

			#endregion

			#region Before

			public delegate void BeforeMethodArray(TAmended instance, string method, object[] parameters);

			public void Before(BeforeMethodArray before)
			{
				base.BeforeMethod = before.Method;
			}

			public new delegate void BeforeMethod(TAmended instance);

			public void Before(BeforeMethod before)
			{
				base.BeforeMethod = before.Method;
			}

			public delegate void BeforeMethod<P1>(TAmended instance, ref P1 param1);

			public void Before<P1>(BeforeMethod<P1> before)
			{
				base.BeforeMethod = before.Method;
			}

			public delegate void BeforeMethod<P1, P2>(TAmended instance, ref P1 param1, ref P2 param2);

			public void Before<P1, P2>(BeforeMethod<P1, P2> before)
			{
				base.BeforeMethod = before.Method;
			}

			public delegate void BeforeMethod<P1, P2, P3>(TAmended instance, ref P1 param1, ref P2 param2, ref P3 param3);

			public void Before<P1, P2, P3>(BeforeMethod<P1, P2, P3> before)
			{
				base.BeforeMethod = before.Method;
			}

			public delegate void BeforeMethod<P1, P2, P3, P4>(TAmended instance, ref P1 param1, ref P2 param2, ref P3 param3, ref P4 param4);

			public void Before<P1, P2, P3, P4>(BeforeMethod<P1, P2, P3, P4> before)
			{
				base.BeforeMethod = before.Method;
			}

			public delegate void BeforeMethod<P1, P2, P3, P4, P5>(TAmended instance, ref P1 param1, ref P2 param2, ref P3 param3, ref P4 param4, ref P5 param5);

			public void Before<P1, P2, P3, P4, P5>(BeforeMethod<P1, P2, P3, P4, P5> before)
			{
				base.BeforeMethod = before.Method;
			}

			public delegate void BeforeMethod<P1, P2, P3, P4, P5, P6>(TAmended instance, ref P1 param1, ref P2 param2, ref P3 param3, ref P4 param4, ref P5 param5, ref P6 param6);

			public void Before<P1, P2, P3, P4, P5, P6>(BeforeMethod<P1, P2, P3, P4, P5, P6> before)
			{
				base.BeforeMethod = before.Method;
			}

			public delegate void BeforeMethod<P1, P2, P3, P4, P5, P6, P7>(TAmended instance, ref P1 param1, ref P2 param2, ref P3 param3, ref P4 param4, ref P5 param5, ref P6 param6, ref P7 param7);

			public void Before<P1, P2, P3, P4, P5, P6, P7>(BeforeMethod<P1, P2, P3, P4, P5, P6, P7> before)
			{
				base.BeforeMethod = before.Method;
			}

			public delegate void BeforeMethod<P1, P2, P3, P4, P5, P6, P7, P8>(TAmended instance, ref P1 param1, ref P2 param2, ref P3 param3, ref P4 param4, ref P5 param5, ref P6 param6, ref P7 param7, ref P8 param8);

			public void Before<P1, P2, P3, P4, P5, P6, P7, P8>(BeforeMethod<P1, P2, P3, P4, P5, P6, P7, P8> before)
			{
				base.BeforeMethod = before.Method;
			}

			#endregion

			#region After (Action)

			public delegate void AfterMethodActionArray(TAmended instance, string method, object[] parameters);

			public void After(AfterMethodActionArray after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterMethodAction(TAmended instance);

			public void After(AfterMethodAction after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterMethodAction<P1>(TAmended instance, P1 param1);

			public void After<P1>(AfterMethodAction<P1> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterMethodAction<P1, P2>(TAmended instance, P1 param1, P2 param2);

			public void After<P1, P2>(AfterMethodAction<P1, P2> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterMethodAction<P1, P2, P3>(TAmended instance, P1 param1, P2 param2, P3 param3);

			public void After<P1, P2, P3>(AfterMethodAction<P1, P2, P3> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterMethodAction<P1, P2, P3, P4>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4);

			public void After<P1, P2, P3, P4>(AfterMethodAction<P1, P2, P3, P4> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterMethodAction<P1, P2, P3, P4, P5>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5);

			public void After<P1, P2, P3, P4, P5>(AfterMethodAction<P1, P2, P3, P4, P5> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterMethodAction<P1, P2, P3, P4, P5, P6>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6);

			public void After<P1, P2, P3, P4, P5, P6>(AfterMethodAction<P1, P2, P3, P4, P5, P6> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterMethodAction<P1, P2, P3, P4, P5, P6, P7>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, P7 param7);

			public void After<P1, P2, P3, P4, P5, P6, P7>(AfterMethodAction<P1, P2, P3, P4, P5, P6, P7> after)
			{
				AfterMethod = after.Method;
			}

			public delegate void AfterMethodAction<P1, P2, P3, P4, P5, P6, P7, P8>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, P7 param7, P8 param8);

			public void After<P1, P2, P3, P4, P5, P6, P7, P8>(AfterMethodAction<P1, P2, P3, P4, P5, P6, P7, P8> after)
			{
				AfterMethod = after.Method;
			}

			#endregion

			#region After (Func)

			public delegate object AfterMethodFuncArray(TAmended instance, string method, object[] parameters, object result);

			public void After(AfterMethodFuncArray after)
			{
				AfterMethod = after.Method;
			}

			public delegate TResult AfterMethodFunc<TResult>(TAmended instance, TResult result);

			public void After<TResult>(AfterMethodFunc<TResult> after)
			{
				AfterMethod = after.Method;
			}

			public delegate TResult AfterMethodFunc<P1, TResult>(TAmended instance, P1 param1, TResult result);

			public void After<P1, TResult>(AfterMethodFunc<P1, TResult> after)
			{
				AfterMethod = after.Method;
			}

			public delegate TResult AfterMethodFunc<P1, P2, TResult>(TAmended instance, P1 param1, P2 param2, TResult result);

			public void After<P1, P2, TResult>(AfterMethodFunc<P1, P2, TResult> after)
			{
				AfterMethod = after.Method;
			}

			public delegate TResult AfterMethodFunc<P1, P2, P3, TResult>(TAmended instance, P1 param1, P2 param2, P3 param3, TResult result);

			public void After<P1, P2, P3, TResult>(AfterMethodFunc<P1, P2, P3, TResult> after)
			{
				AfterMethod = after.Method;
			}

			public delegate TResult AfterMethodFunc<P1, P2, P3, P4, TResult>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, TResult result);

			public void After<P1, P2, P3, P4, TResult>(AfterMethodFunc<P1, P2, P3, P4, TResult> after)
			{
				AfterMethod = after.Method;
			}

			public delegate TResult AfterMethodFunc<P1, P2, P3, P4, P5, TResult>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, TResult result);

			public void After<P1, P2, P3, P4, P5, TResult>(AfterMethodFunc<P1, P2, P3, P4, P5, TResult> after)
			{
				AfterMethod = after.Method;
			}

			public delegate TResult AfterMethodFunc<P1, P2, P3, P4, P5, P6, TResult>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, TResult result);

			public void After<P1, P2, P3, P4, P5, P6, TResult>(AfterMethodFunc<P1, P2, P3, P4, P5, P6, TResult> after)
			{
				AfterMethod = after.Method;
			}

			public delegate TResult AfterMethodFunc<P1, P2, P3, P4, P5, P6, P7, TResult>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, P7 param7, TResult result);

			public void After<P1, P2, P3, P4, P5, P6, P7, TResult>(AfterMethodFunc<P1, P2, P3, P4, P5, P6, P7, TResult> after)
			{
				AfterMethod = after.Method;
			}

			public delegate TResult AfterMethodFunc<P1, P2, P3, P4, P5, P6, P7, P8, TResult>(TAmended instance, P1 param1, P2 param2, P3 param3, P4 param4, P5 param5, P6 param6, P7 param7, P8 param8, TResult result);

			public void After<P1, P2, P3, P4, P5, P6, P7, P8, TResult>(AfterMethodFunc<P1, P2, P3, P4, P5, P6, P7, P8, TResult> after)
			{
				AfterMethod = after.Method;
			}

			#endregion
		}
	}

	#endregion
}
