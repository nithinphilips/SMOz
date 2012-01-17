using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;
using System.Reflection;

namespace Afterthought
{
	public partial class Amendment
	{
		public class AttributeList : IEnumerable
		{
			IList<Attribute> attributes = new List<Attribute>();

			internal AttributeList()
			{ }

			#region Add

			Attribute<TAttribute> Add<TAttribute>(object[] parameters, Type[] parameterTypes)
				where TAttribute : System.Attribute
			{
				ValidateParameters(parameters);

				var attribute = new Attribute<TAttribute>
				{
					Constructor = GetConstructor<TAttribute>(parameterTypes),
					Arguments = parameters
				};

				attributes.Add(attribute);

				return attribute;
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public Attribute<TAttribute> Add<TAttribute>()
				where TAttribute : System.Attribute
			{
				return Add<TAttribute>(
					new object[] { }, 
					Type.EmptyTypes);
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public Attribute<TAttribute> Add<TAttribute, P1>(P1 value1)
				where TAttribute : System.Attribute
			{
				return Add<TAttribute>(
					new object[] { value1 },
					new Type[] { typeof(P1) });
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public Attribute<TAttribute> Add<TAttribute, P1, P2>(P1 value1, P2 value2)
				where TAttribute : System.Attribute
			{
				return Add<TAttribute>(
					new object[] { value1, value2 }, 
					new Type[] { typeof(P1), typeof(P2) });
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public Attribute<TAttribute> Add<TAttribute, P1, P2, P3>(P1 value1, P2 value2, P3 value3)
				where TAttribute : System.Attribute
			{
				return Add<TAttribute>(
					new object[] { value1, value2, value3 },
					new Type[] { typeof(P1), typeof(P2), typeof(P3) });
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public Attribute<TAttribute> Add<TAttribute, P1, P2, P3, P4>(P1 value1, P2 value2, P3 value3, P4 value4)
				where TAttribute : System.Attribute
			{
				return Add<TAttribute>(
					new object[] { value1, value2, value3, value4 }, 
					new Type[] { typeof(P1), typeof(P2), typeof(P3), typeof(P4) });
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public Attribute<TAttribute> Add<TAttribute, P1, P2, P3, P4, P5>(P1 value1, P2 value2, P3 value3, P4 value4, P5 value5)
				where TAttribute : System.Attribute
			{
				return Add<TAttribute>(
					new object[] { value1, value2, value3, value4, value5 }, 
					new Type[] { typeof(P1), typeof(P2), typeof(P3), typeof(P4), typeof(P5) });
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public Attribute<TAttribute> Add<TAttribute, P1, P2, P3, P4, P5, P6>(P1 value1, P2 value2, P3 value3, P4 value4, P5 value5, P6 value6)
				where TAttribute : System.Attribute
			{
				return Add<TAttribute>(
					new object[] { value1, value2, value3, value4, value5, value6 }, 
					new Type[] { typeof(P1), typeof(P2), typeof(P3), typeof(P4), typeof(P5), typeof(P6) });
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public Attribute<TAttribute> Add<TAttribute, P1, P2, P3, P4, P5, P6, P7>(P1 value1, P2 value2, P3 value3, P4 value4, P5 value5, P6 value6, P7 value7)
				where TAttribute : System.Attribute
			{
				return Add<TAttribute>(
					new object[] { value1, value2, value3, value4, value5, value6, value7 }, 
					new Type[] { typeof(P1), typeof(P2), typeof(P3), typeof(P4), typeof(P5), typeof(P6), typeof(P7) });
			}

			/// <summary>
			/// Add a new <see cref="Attribute"/> of the specified type.
			/// </summary>
			public Attribute<TAttribute> Add<TAttribute, P1, P2, P3, P4, P5, P6, P7, P8>(P1 value1, P2 value2, P3 value3, P4 value4, P5 value5, P6 value6, P7 value7, P8 value8)
				where TAttribute : System.Attribute
			{
				return Add<TAttribute>(
					new object[] { value1, value2, value3, value4, value5, value6, value7, value8 }, 
					new Type[] { typeof(P1), typeof(P2), typeof(P3), typeof(P4), typeof(P5), typeof(P6), typeof(P7), typeof(P8) });
			}

			#endregion

			#region Methods

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			{
				return attributes.GetEnumerator();
			}

			/// <summary>
			/// Helper method to ensure a reasonable exception is thrown if no valid constructor is found
			/// </summary>
			/// <typeparam name="T"></typeparam>
			/// <param name="types"></param>
			/// <returns></returns>
			static ConstructorInfo GetConstructor<T>(Type[] types)
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

			static bool IsConstantExpression(object value)
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

			static void ValidateParameters(IEnumerable parameters)
			{
				foreach (var obj in parameters)
					if (!IsConstantExpression(obj))
						throw new ArgumentException("An attribute argument must be a constant expression, typeof expression or array creation expression of an attribute parameter type");
					else if (obj.GetType().IsArray)
						ValidateParameters((IEnumerable)obj);
			}

			#endregion
		}
	}
}
