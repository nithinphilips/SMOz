using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;

namespace Afterthought
{
	#region Amendment.Attribute

	/// <summary>
	/// Abstract base class for concrete <see cref="Amendment<TType, TAmended>"/> which supports amending code into
	/// a specific <see cref="Type"/> during compilation
	/// </summary>
	public abstract partial class Amendment
	{
		public abstract class Attribute : Member, IAttributeAmendment
		{
			internal Attribute(string name)
				: base(name)
			{ }

			public abstract Type Type { get; }

			object[] IAttributeAmendment.Arguments { get { return Arguments; } }
			ConstructorInfo IAttributeAmendment.Constructor { get { return Constructor; } }

			internal object[] Arguments { get; set; }
			internal ConstructorInfo Constructor { get; set; }

			public override bool IsAmended
			{
				get 
				{
					return Type != null;
				}
			}
		}
	}

	#endregion

	#region Amendment<TType, TAmended>.Attribute

	public partial class Amendment<TType, TAmended> : Amendment
	{
		public class Attribute<A> : Attribute
			where A : System.Attribute
		{
			internal Attribute()
				: base(typeof(A).Name)
			{ }

			public override Type Type
			{
				get
				{
					return typeof(A);
				}
			}

			#region Create<T...>

			/// <summary>
			/// Helper method to ensure a reasonable exception is thrown if no valid constructor is found
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="types"></param>
			/// <returns></returns>
			private static ConstructorInfo GetConstructor<T>(Type[] types)
			{
				ConstructorInfo constructorInfo = typeof(T).GetConstructor(types); ;

				if (constructorInfo == null)
				{
					var typeNames = (from t in types
										  select t.Name).ToArray();

					throw new ArgumentException(string.Format("No constructor for type {0} matches the set of argument types: {1}", typeof(T).AssemblyQualifiedName, string.Join(", ", typeNames)));
				}

				return constructorInfo;
			}

			private static bool IsConstantExpression(object value)
			{
				Type type = value.GetType();

				// Verify type
				return type == typeof(sbyte) ||
					type == typeof(byte) ||
					type == typeof(ushort) ||
					type == typeof(int) ||
					type == typeof(uint) ||
					type == typeof(long) ||
					type == typeof(ulong) ||
					type == typeof(char) ||
					type == typeof(float) ||
					type == typeof(double) ||
					type == typeof(decimal) ||
					type == typeof(bool) ||
					type == typeof(string) ||
					typeof(Type).IsAssignableFrom(type) ||
					type.IsArray ||
					type.IsEnum;
			}

			private static void ValidateParameters(IEnumerable parameters)
			{
				foreach (var obj in parameters)
					if (!IsConstantExpression(obj))
						throw new ArgumentException("An attribute argument must be a constant expression, typeof expression or array creation expression of an attribute parameter type");
					else if (obj.GetType().IsArray)
						ValidateParameters((IEnumerable) obj);
			}

			/// <summary>
			/// Create a new Attribute
			/// </summary>
			/// <returns></returns>
			public static Attribute<A> Create()
			{
				return new Attribute<A>
				{
					Constructor = GetConstructor<A>(Type.EmptyTypes),
					Arguments = new object[] { }
				};
			}

			/// <summary>
			/// Create a new Attribute
			/// </summary>
			/// <typeparam name="P1">Parameter 1 Type</typeparam>
			/// <param name="value1">Parameter 1 Value</param>
			/// <returns></returns>
			public static Attribute<A> Create<P1>(P1 value1)
			{
				object[] parameters = new object[] { value1 };

				ValidateParameters(parameters);

				return new Attribute<A>
				{
					Constructor = GetConstructor<A>(new Type[] { typeof(P1) }),
					Arguments = parameters
				};
			}

			/// <summary>
			/// Create a new Attribute
			/// </summary>
			/// <typeparam name="P1">Parameter 1 Type</typeparam>
			/// <param name="value1">Parameter 1 Value</param>
			/// <typeparam name="P2">Parameter 2 Type</typeparam>
			/// <param name="value2">Parameter 2 Value</param>
			/// <returns></returns>
			public static Attribute<A> Create<P1, P2>(P1 value1, P2 value2)
			{
				object[] parameters = new object[] { value1, value2 };

				ValidateParameters(parameters);

				return new Attribute<A>
				{
					Constructor = GetConstructor<A>(new Type[] { typeof(P1), typeof(P2) }),
					Arguments = parameters
				};
			}

			/// <summary>
			/// Create a new Attribute
			/// </summary>
			/// <typeparam name="P1">Parameter 1 Type</typeparam>
			/// <param name="value1">Parameter 1 Value</param>
			/// <typeparam name="P2">Parameter 2 Type</typeparam>
			/// <param name="value2">Parameter 2 Value</param>
			/// <typeparam name="P3">Parameter 3 Type</typeparam>
			/// <param name="value3">Parameter 3 Value</param>
			/// <returns></returns>
			public static Attribute<A> Create<P1, P2, P3>(P1 value1, P2 value2, P3 value3)
			{
				object[] parameters = new object[] { value1, value2, value3 };

				ValidateParameters(parameters);

				return new Attribute<A>
				{
					Constructor = GetConstructor<A>(new Type[] { typeof(P1), typeof(P2), typeof(P3) }),
					Arguments = parameters
				};
			}

			/// <summary>
			/// Create a new Attribute
			/// </summary>
			/// <typeparam name="P1">Parameter 1 Type</typeparam>
			/// <param name="value1">Parameter 1 Value</param>
			/// <typeparam name="P2">Parameter 2 Type</typeparam>
			/// <param name="value2">Parameter 2 Value</param>
			/// <typeparam name="P3">Parameter 3 Type</typeparam>
			/// <param name="value3">Parameter 3 Value</param>
			/// <typeparam name="P4">Parameter 4 Type</typeparam>
			/// <param name="value4">Parameter 4 Value</param>
			/// <returns></returns>
			public static Attribute<A> Create<P1, P2, P3, P4>(P1 value1, P2 value2, P3 value3, P4 value4)
			{
				object[] parameters = new object[] { value1, value2, value3, value4 };

				ValidateParameters(parameters);

				return new Attribute<A>
				{
					Constructor = GetConstructor<A>(new Type[] { typeof(P1), typeof(P2), typeof(P3), typeof(P4) }),
					Arguments = parameters
				};
			}

			/// <summary>
			/// Create a new Attribute
			/// </summary>
			/// <typeparam name="P1">Parameter 1 Type</typeparam>
			/// <param name="value1">Parameter 1 Value</param>
			/// <typeparam name="P2">Parameter 2 Type</typeparam>
			/// <param name="value2">Parameter 2 Value</param>
			/// <typeparam name="P3">Parameter 3 Type</typeparam>
			/// <param name="value3">Parameter 3 Value</param>
			/// <typeparam name="P4">Parameter 4 Type</typeparam>
			/// <param name="value4">Parameter 4 Value</param>
			/// <typeparam name="P5">Parameter 5 Type</typeparam>
			/// <param name="value5">Parameter 5 Value</param>
			/// <returns></returns>
			public static Attribute<A> Create<P1, P2, P3, P4, P5>(P1 value1, P2 value2, P3 value3, P4 value4, P5 value5)
			{
				object[] parameters = new object[] { value1, value2, value3, value4, value5 };

				ValidateParameters(parameters);

				return new Attribute<A>
				{
					Constructor = GetConstructor<A>(new Type[] { typeof(P1), typeof(P2), typeof(P3), typeof(P4), typeof(P5) }),
					Arguments = parameters
				};
			}

			/// <summary>
			/// Create a new Attribute
			/// </summary>
			/// <typeparam name="P1">Parameter 1 Type</typeparam>
			/// <param name="value1">Parameter 1 Value</param>
			/// <typeparam name="P2">Parameter 2 Type</typeparam>
			/// <param name="value2">Parameter 2 Value</param>
			/// <typeparam name="P3">Parameter 3 Type</typeparam>
			/// <param name="value3">Parameter 3 Value</param>
			/// <typeparam name="P4">Parameter 4 Type</typeparam>
			/// <param name="value4">Parameter 4 Value</param>
			/// <typeparam name="P5">Parameter 5 Type</typeparam>
			/// <param name="value5">Parameter 5 Value</param>
			/// <typeparam name="P6">Parameter 6 Type</typeparam>
			/// <param name="value6">Parameter 6 Value</param>
			/// <returns></returns>
			public static Attribute<A> Create<P1, P2, P3, P4, P5, P6>(P1 value1, P2 value2, P3 value3, P4 value4, P5 value5, P6 value6)
			{
				object[] parameters = new object[] { value1, value2, value3, value4, value5, value6 };

				ValidateParameters(parameters);

				return new Attribute<A>
				{
					Constructor = GetConstructor<A>(new Type[] { typeof(P1), typeof(P2), typeof(P3), typeof(P4), typeof(P5), typeof(P6) }),
					Arguments = parameters
				};
			}

			/// <summary>
			/// Create a new Attribute
			/// </summary>
			/// <typeparam name="P1">Parameter 1 Type</typeparam>
			/// <param name="value1">Parameter 1 Value</param>
			/// <typeparam name="P2">Parameter 2 Type</typeparam>
			/// <param name="value2">Parameter 2 Value</param>
			/// <typeparam name="P3">Parameter 3 Type</typeparam>
			/// <param name="value3">Parameter 3 Value</param>
			/// <typeparam name="P4">Parameter 4 Type</typeparam>
			/// <param name="value4">Parameter 4 Value</param>
			/// <typeparam name="P5">Parameter 5 Type</typeparam>
			/// <param name="value5">Parameter 5 Value</param>
			/// <typeparam name="P6">Parameter 6 Type</typeparam>
			/// <param name="value6">Parameter 6 Value</param>
			/// <typeparam name="P7">Parameter 7 Type</typeparam>
			/// <param name="value7">Parameter 7 Value</param>
			/// <returns></returns>
			public static Attribute<A> Create<P1, P2, P3, P4, P5, P6, P7>(P1 value1, P2 value2, P3 value3, P4 value4, P5 value5, P6 value6, P7 value7)
			{
				object[] parameters = new object[] { value1, value2, value3, value4, value5, value6, value7 };

				ValidateParameters(parameters);

				return new Attribute<A>
				{
					Constructor = GetConstructor<A>(new Type[] { typeof(P1), typeof(P2), typeof(P3), typeof(P4), typeof(P5), typeof(P6), typeof(P7) }),
					Arguments = parameters
				};
			}

			/// <summary>
			/// Create a new Attribute
			/// </summary>
			/// <typeparam name="P1">Parameter 1 Type</typeparam>
			/// <param name="value1">Parameter 1 Value</param>
			/// <typeparam name="P2">Parameter 2 Type</typeparam>
			/// <param name="value2">Parameter 2 Value</param>
			/// <typeparam name="P3">Parameter 3 Type</typeparam>
			/// <param name="value3">Parameter 3 Value</param>
			/// <typeparam name="P4">Parameter 4 Type</typeparam>
			/// <param name="value4">Parameter 4 Value</param>
			/// <typeparam name="P5">Parameter 5 Type</typeparam>
			/// <param name="value5">Parameter 5 Value</param>
			/// <typeparam name="P6">Parameter 6 Type</typeparam>
			/// <param name="value6">Parameter 6 Value</param>
			/// <typeparam name="P7">Parameter 7 Type</typeparam>
			/// <param name="value7">Parameter 7 Value</param>
			/// <typeparam name="P8">Parameter 8 Type</typeparam>
			/// <param name="value8">Parameter 8 Value</param>
			/// <returns></returns>
			public static Attribute<A> Create<P1, P2, P3, P4, P5, P6, P7, P8>(P1 value1, P2 value2, P3 value3, P4 value4, P5 value5, P6 value6, P7 value7, P8 value8)
			{
				object[] parameters = new object[] { value1, value2, value3, value4, value5, value6, value7, value8 };

				ValidateParameters(parameters);

				return new Attribute<A>
				{
					Constructor = GetConstructor<A>(new Type[] { typeof(P1), typeof(P2), typeof(P3), typeof(P4), typeof(P5), typeof(P6), typeof(P7), typeof(P8) }),
					Arguments = parameters
				};
			}

			#endregion
		}
	}

	#endregion
}
