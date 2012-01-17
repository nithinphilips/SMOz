using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logging
{
    /// <summary>
    /// Attribute used to disable logging a given item
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor, AllowMultiple = false, Inherited = true)]
    public class DisableLoggingAttribute : Attribute
    {
    }
}
