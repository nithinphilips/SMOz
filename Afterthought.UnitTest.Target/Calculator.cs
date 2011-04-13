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

namespace Afterthought.UnitTest.Target
{
	[Amendment(typeof(TestAmendment<>))]
	public class Calculator : ILog
	{
		public int Result { get; set; }

		public int CopyToResult { get; set; }

		public int InitiallyThirteen { get; set; }

		public string ExistingLazyRandomName { get; private set; }

		public int Add { get; set; }

		public int One { get { return 1; } }

		public int Two { get { return 2; } }

		public int Random1 { get; set; }

		public int Random2 { get; set; }

		public int Random3 { get; set; }

		public int Random4
		{
			get
			{
				return Result;
			}
			set
			{
				Result = value;
			}
		}

		int random5;
		public int Random5
		{
			get
			{
				return GetRandom(random5);
			}
			set
			{
				random5 = GetRandom(value);
			}
		}

		/// <summary>
		/// Generates a random number that is different from the number passed it, but is 
		/// consistently the same number each time.
		/// </summary>
		/// <param name="except"></param>
		/// <returns></returns>
		public int GetRandom(int except)
		{
			var r = new System.Random(except);
			int random;
			do
			{
				random = r.Next();
			}
			while (random == except);

			return random;
		}

		/// <summary>
		/// Explicit (an incorrect) implementation of ILog.e.
		/// </summary>
		decimal ILog.e
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Implicit implementation of IMath.SqRt2.
		/// </summary>
		public virtual decimal SqRt2
		{
			get 
			{
				return 1.61803398874m;
			}
		}

		public int Multiply(int x, int y)
		{
			return x * y;
		}

		public int Multiply2(int x, int y)
		{
			return x * y;
		}

		public int Divide(int x, int y)
		{
			return x / y;
		}

		public int Divide2(int x, int y)
		{
			return x / y;
		}

		public int Square(int x)
		{
			// Intentionally wrong
			return x;
		}
	}
}
