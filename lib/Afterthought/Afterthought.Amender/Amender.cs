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

using Microsoft.Build;
using Microsoft.Build.Framework;
using System;
using Microsoft.Build.Utilities;

namespace Afterthought.Amender
{
	public sealed class Amender : AppDomainIsolatedTask
	{
		public override bool Execute()
		{
			DateTime start = DateTime.Now;
			Log.LogMessage("Amending {0}", AssemblyLocation);
			Program.Amend(AssemblyLocation);
			Log.LogMessage("Amending Complete ({0:000})", DateTime.Now.Subtract(start).TotalSeconds);
			return true;
		}

		[Required]
		public string AssemblyLocation { get; set; }
	}
}