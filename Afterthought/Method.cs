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
					return ImplementationMethod != null || BeforeMethod != null || AfterMethod != null;
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
		/// Represents a method on a type with a return value of <see cref="M"/>.
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

			public delegate void CreateMethodAction(TAmended instance, string method);

			public delegate void CreateMethodAction<P>(TAmended instance, string method, P parameters);

			public static Method Create(string name, CreateMethodAction create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1>(string name, CreateMethodAction<Parameter<P1>> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2>(string name, CreateMethodAction<Parameter<P1, P2>> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, P3>(string name, CreateMethodAction<Parameter<P1, P2, P3>> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, P3, P4>(string name, CreateMethodAction<Parameter<P1, P2, P3, P4>> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5>(string name, CreateMethodAction<Parameter<P1, P2, P3, P4, P5>> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, P6>(string name, CreateMethodAction<Parameter<P1, P2, P3, P4, P5, P6>> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, P6, P7>(string name, CreateMethodAction<Parameter<P1, P2, P3, P4, P5, P6, P7>> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, P6, P7, P8>(string name, CreateMethodAction<Parameter<P1, P2, P3, P4, P5, P6, P7, P8>> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			#endregion

			#region Create (Func)

			public delegate R CreateMethodFunc<R>(TAmended instance, string method);

			public delegate R CreateMethodFunc<P, R>(TAmended instance, string method, P parameters);

			public static Method Create<TResult>(string name, CreateMethodFunc<TResult> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, TResult>(string name, CreateMethodFunc<Parameter<P1>, TResult> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, TResult>(string name, CreateMethodFunc<Parameter<P1, P2>, TResult> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, P3, TResult>(string name, CreateMethodFunc<Parameter<P1, P2, P3>, TResult> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, P3, P4, TResult>(string name, CreateMethodFunc<Parameter<P1, P2, P3, P4>, TResult> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, TResult>(string name, CreateMethodFunc<Parameter<P1, P2, P3, P4, P5>, TResult> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, P6, TResult>(string name, CreateMethodFunc<Parameter<P1, P2, P3, P4, P5, P6>, TResult> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, P6, P7, TResult>(string name, CreateMethodFunc<Parameter<P1, P2, P3, P4, P5, P6, P7>, TResult> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			public static Method Create<P1, P2, P3, P4, P5, P6, P7, P8, TResult>(string name, CreateMethodFunc<Parameter<P1, P2, P3, P4, P5, P6, P7, P8>, TResult> create)
			{
				return new Method(name) { ImplementationMethod = create.Method };
			}

			#endregion

			#region Before

			public new delegate void BeforeMethod(TAmended instance, string method);

			public delegate P BeforeMethod<P>(TAmended instance, string method, P parameters);

			public void Before(BeforeMethod before)
			{
				base.BeforeMethod = before.Method;
			}

			public void Before(BeforeMethod<object[]> before)
			{
				base.BeforeMethod = before.Method;
			}

			public void Before<P1>(BeforeMethod<Parameter<P1>> before)
			{
				if (MethodInfo.GetParameters().Length != 1)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2>(BeforeMethod<Parameter<P1, P2>> before)
			{
				if (MethodInfo.GetParameters().Length != 2)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2, P3>(BeforeMethod<Parameter<P1, P2, P3>> before)
			{
				if (MethodInfo.GetParameters().Length != 3)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2, P3, P4>(BeforeMethod<Parameter<P1, P2, P3, P4>> before)
			{
				if (MethodInfo.GetParameters().Length != 4)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2, P3, P4, P5>(BeforeMethod<Parameter<P1, P2, P3, P4, P5>> before)
			{
				if (MethodInfo.GetParameters().Length != 5)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2, P3, P4, P5, P6>(BeforeMethod<Parameter<P1, P2, P3, P4, P5, P6>> before)
			{
				if (MethodInfo.GetParameters().Length != 6)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2, P3, P4, P5, P6, P7>(BeforeMethod<Parameter<P1, P2, P3, P4, P5, P6, P7>> before)
			{
				if (MethodInfo.GetParameters().Length != 7)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			public void Before<P1, P2, P3, P4, P5, P6, P7, P8>(BeforeMethod<Parameter<P1, P2, P3, P4, P5, P6, P7, P8>> before)
			{
				if (MethodInfo.GetParameters().Length != 8)
					throw new ArgumentException("The number of parameters must match the method signature.");

				base.BeforeMethod = before.Method;
			}

			#endregion

			#region After (Action)

			public delegate void AfterMethodAction(TAmended instance, string method);

			public delegate void AfterMethodAction<P>(TAmended instance, string method, P parameters);

			public void After(AfterMethodAction after)
			{
				AfterMethod = after.Method;
			}

			public void After(AfterMethodAction<object[]> after)
			{
				AfterMethod = after.Method;
			}

			public void After<P1>(AfterMethodAction<Parameter<P1>> after)
			{
				if (MethodInfo.GetParameters().Length != 1)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2>(AfterMethodAction<Parameter<P1, P2>> after)
			{
				if (MethodInfo.GetParameters().Length != 2)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3>(AfterMethodAction<Parameter<P1, P2, P3>> after)
			{
				if (MethodInfo.GetParameters().Length != 3)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4>(AfterMethodAction<Parameter<P1, P2, P3, P4>> after)
			{
				if (MethodInfo.GetParameters().Length != 4)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, P5>(AfterMethodAction<Parameter<P1, P2, P3, P4, P5>> after)
			{
				if (MethodInfo.GetParameters().Length != 5)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, P5, P6>(AfterMethodAction<Parameter<P1, P2, P3, P4, P5, P6>> after)
			{
				if (MethodInfo.GetParameters().Length != 6)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, P5, P6, P7>(AfterMethodAction<Parameter<P1, P2, P3, P4, P5, P6, P7>> after)
			{
				if (MethodInfo.GetParameters().Length != 7)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, P5, P6, P7, P8>(AfterMethodAction<Parameter<P1, P2, P3, P4, P5, P6, P7, P8>> after)
			{
				if (MethodInfo.GetParameters().Length != 8)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			#endregion

			#region After (Func)

			public delegate R AfterMethodFunc<R>(TAmended instance, string method);

			public delegate R AfterMethodFunc<P, R>(TAmended instance, string method, P parameters);

			public void After<TResult>(AfterMethodFunc<TResult> after)
			{
				AfterMethod = after.Method;
			}

			public void After<TResult>(AfterMethodFunc<object[], TResult> after)
			{
				AfterMethod = after.Method;
			}

			public void After<P1, TResult>(AfterMethodFunc<Parameter<P1>, TResult> after)
			{
				if (MethodInfo.GetParameters().Length != 7)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, TResult>(AfterMethodFunc<Parameter<P1, P2>, TResult> after)
			{
				if (MethodInfo.GetParameters().Length != 8)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, TResult>(AfterMethodFunc<Parameter<P1, P2, P3>, TResult> after)
			{
				if (MethodInfo.GetParameters().Length != 7)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, TResult>(AfterMethodFunc<Parameter<P1, P2, P3, P4>, TResult> after)
			{
				if (MethodInfo.GetParameters().Length != 8)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, P5, TResult>(AfterMethodFunc<Parameter<P1, P2, P3, P4, P5>, TResult> after)
			{
				if (MethodInfo.GetParameters().Length != 7)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, P5, P6, TResult>(AfterMethodFunc<Parameter<P1, P2, P3, P4, P5, P6>, TResult> after)
			{
				if (MethodInfo.GetParameters().Length != 8)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, P5, P6, P7, TResult>(AfterMethodFunc<Parameter<P1, P2, P3, P4, P5, P6, P7>, TResult> after)
			{
				if (MethodInfo.GetParameters().Length != 7)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			public void After<P1, P2, P3, P4, P5, P6, P7, P8, TResult>(AfterMethodFunc<Parameter<P1, P2, P3, P4, P5, P6, P7, P8>, TResult> after)
			{
				if (MethodInfo.GetParameters().Length != 8)
					throw new ArgumentException("The number of parameters must match the method signature.");

				AfterMethod = after.Method;
			}

			#endregion
		}
	}

	#endregion
}
