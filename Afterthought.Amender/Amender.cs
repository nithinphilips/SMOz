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