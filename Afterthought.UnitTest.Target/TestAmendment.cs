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

		public override void Amend<P>(Property<P> property)
		{
			// Modify Random1 getter set value of Random1 to GetRandom(1) before returning the underlying property value
			if (property.Name == "Random1")
				property.BeforeGet = (instance, propertyName) => instance.Random1 = instance.GetRandom(1);

			// Modify Random2 to calculate a random number based on an assigned seed value
			else if (property.Name == "Random2")
				property.OfType<int>().AfterGet = (instance, propertyName, result) => instance.GetRandom(result);

			// Modify ILog.e property
			else if (property.Name == "Afterthought.UnitTest.Target.ILog.e")
				property.OfType<decimal>().Getter = (instance, propertyName) => 2.71828m;

			// Modify Random5 to be just a simple getter/setter using Result property as the backing store
			else if (property.Name == "Random5")
			{
				property.OfType<int>().Getter = (instance, propertyName) => instance.Result;
				property.OfType<int>().Setter = (instance, propertyName, value) => instance.Result = value;
			}

			// Update Result to equal the value assigned to CopyToResult
			else if (property.Name == "CopyToResult")
				property.OfType<int>().BeforeSet = (instance, propertyName, oldValue, value) => { instance.Result = value; return value; };

			// Modify Add to add the old value to the value being assigned
			else if (property.Name == "Add")
				property.OfType<int>().BeforeSet = (instance, propertyName, oldValue, value) => oldValue + value;

			// Initialize InitiallyThirteen to 13
			else if (property.Name == "InitiallyThirteen")
				property.OfType<int>().Initializer = (instance, propertyName) => 13;

			// Initialize ExistingLazyRandomName
			else if (property.Name == "ExistingLazyRandomName")
				property.OfType<string>().LazyInitializer = (instance, propertyName) => "r" + new Random().Next();
		}

		public override void Amend(Method method)
		{
			
		}
	}
}
