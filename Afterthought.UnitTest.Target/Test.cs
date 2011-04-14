using System;

namespace Afterthought.UnitTest.Target
{
	[Afterthought.Amendment(typeof(amend<>))]
	public class junk
	{
		public static void Main()
		{
			Console.WriteLine("Test");
		}
	}

	public class amend<T> : Amendment<T, T>
	{
		public override void Amend()
		{
			AddProperty(new Property<int>("Count"));
		}
	}
}
