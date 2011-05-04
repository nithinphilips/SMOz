using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logging
{
    /// <summary>
    /// Marker attribute used on a class to specify that a given class should be logged.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false,  Inherited = true)]
    public class LoggingAttribute : Attribute
    { }
}
