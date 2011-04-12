using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Afterthought.UnitTest.Target
{
	public class TestAmendment<T> : Amendment<T,T>
		where T : Calculator
	{
		public TestAmendment()
		{
			Build();
		}

		public override void Amend()
		{
			// public int Count { get; set; }
			AddProperty(new Property<int>("Count"));

			// public int DoubleCount { get { return Count * 2; } }
			AddProperty(new Property<int>("Three")
			{
				Getter = (instance, property) => instance.One + instance.Two
			});

			// public string LazyRandomName { get { if (lazyRandomName == null) lazyRandomName = "r" + new Random().Next(); return lazyRandomName; } }
			AddProperty(new Property<string>("LazyRandomName")
			{
				LazyInitializer = (instance, property) => "r" + new Random().Next()
			});

			// public int SetResult { set { Result = value; } }
			AddProperty(new Property<int>("SetResult")
			{
				Setter = (instance, property, value) => instance.Result = value
			});

			// public int InitiallyTwelve { get; set; } = 12
			AddProperty(new Property<int>("InitiallyTwelve")
			{
				Initializer = (instance, property) => 12
			});

			// Implement IMath interface
			ImplementInterface<IMath>(

				// Pi
				new Property<decimal>("Pi") { Getter = (instance, property) => 3.14159m },

				// Subtract()
				Method.Create<decimal, decimal, decimal>("Subtract", (instance, method, parameters) => parameters.Param1 - parameters.Param2)
			);
		}

		public override void Amend<F>(Field<F> field)
		{
			
		}

		public override void Amend(Constructor constructor)
		{
			
		}

		/// <summary>
		/// Apply amendments to existing properties to implement the behavior being tested.
		/// </summary>
		/// <typeparam name="P"></typeparam>
		/// <param name="property"></param>
		public override void Amend<P>(Property<P> property)
		{
			switch (property.Name)
			{
				// Modify Random1 getter set value of Random1 to GetRandom(1) before returning the underlying property value
				case "Random1":
					property.BeforeGet = (instance, propertyName) => instance.Random1 = instance.GetRandom(1);
					break;

				// Modify Random2 to calculate a random number based on an assigned seed value
				case "Random2":
					property.OfType<int>().AfterGet = (instance, propertyName, result) => instance.GetRandom(result);
					break;

				// Modify ILog.e property
				case "Afterthought.UnitTest.Target.ILog.e":
					property.OfType<decimal>().Getter = (instance, propertyName) => 2.71828m;
					break;

				// Modify Random5 to be just a simple getter/setter using Result property as the backing store
				case "Random5":
					property.OfType<int>().Getter = (instance, propertyName) => instance.Result;
					property.OfType<int>().Setter = (instance, propertyName, value) => instance.Result = value;
					break;

				// Update Result to equal the value assigned to CopyToResult
				case "CopyToResult":
					property.OfType<int>().BeforeSet = (instance, propertyName, oldValue, value) => { instance.Result = value; return value; };
					break;

				// Modify Add to add the old value to the value being assigned
				case "Add":
					property.OfType<int>().BeforeSet = (instance, propertyName, oldValue, value) => oldValue + value;
					break;

				// Initialize InitiallyThirteen to 13
				case "InitiallyThirteen":
					property.OfType<int>().Initializer = (instance, propertyName) => 13;
					break;

				// Initialize ExistingLazyRandomName
				case "ExistingLazyRandomName":
					property.OfType<string>().LazyInitializer = (instance, propertyName) => "r" + new Random().Next();
					break;
			}
		}
			
		/// <summary>
		/// Apply amendments to existing methods to implement the behavior being tested.
		/// </summary>
		/// <param name="method"></param>
		public override void Amend(Method method)
		{
			switch (method.Name)
			{
				// Modify Multiply to also set the Result property to the resulting value
				case "Multiply" :
				    method.Before<int, int>((instance, methodName, parameters) =>
				    {
				        instance.Result = parameters.Param1 * parameters.Param2;

				        // Return null to indicate that the original parameters should not be modified
				        return null;
				    });
				    break;

				// Modify Divide to change the second parameter value to 1 every time
				case "Divide":
				    method.Before<int, int>((instance, methodName, parameters) =>
				    {
				        parameters.Param2 = 1;

				        // Return the updated parameters to cause the new values to be used by the original implementation
				        return parameters;
				    });
				    break;
			}
		}
	}
}
