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
