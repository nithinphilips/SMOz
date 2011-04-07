using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Afterthought.UnitTest.Target
{
	public interface IMath
	{
		//decimal Add(decimal x, decimal y);

		//decimal Subtract(decimal x, decimal y);

		//decimal Multiply(decimal x, decimal y);

		//decimal Divide(decimal x, decimal y);

		decimal Pi { get; }

		decimal SqRt2 { get; }

		decimal Base { get; set; }

		decimal Subtract(decimal x, decimal y);
	}
}
