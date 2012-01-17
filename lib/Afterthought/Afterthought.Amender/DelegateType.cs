using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Afterthought.Amender
{
	/// <summary>
	/// Internal enumeration used to describe a method delegate to simplify the amending logic.
	/// </summary>
	[Flags]
	internal enum MethodDelegateType
	{
		// Modes
		Implement = 0x1,
		Before = 2,
		Catch = 4,
		Finally = 8,
		After = 16,

		// Characteristics
		ExplicitSyntax = 32,
		ArraySyntax = 64,
		WithContext = 128,
		Action = 256,
		Function = 512,
		HasResultParameter = 1024
	}
}
