using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logging
{
	public class Logger
	{
		public static List<String> Log { get; private set; }
		
		/// <summary>
		/// static constructor
		/// </summary>
		static Logger()
		{
			Reset();
		}

        /// <summary>
        /// Reset the logger
        /// </summary>
        public static void Reset()
        {
            Log = new List<string>();
        }

		/// <summary>
		/// The implementation of writeline
		/// </summary>
		internal static void InternalWriteLine(object instance, string name, object[] ps, string prefix)
		{
			var stringBuilder = new StringBuilder();

			//get function name and class name
			stringBuilder.Append(String.Format("{0} {1}.{2}(", prefix, instance != null ? instance.GetType().FullName : "static", name));
			bool first = true;
			//add parameters
			foreach (var p in ps)
			{
				if (!first)
				{
					stringBuilder.Append(",");
					first = false;
				}
				stringBuilder.Append(p != null ? p.ToString() : "NULL");
			}
			stringBuilder.Append(")");

			Log.Add(stringBuilder.ToString());
		}
	}

    /// <summary>
    /// Simple logger class. 
    /// </summary>
    public class Logger<T> : Logger
    {
        /// <summary>
        /// Simple logging
        /// </summary>
        public static void LogMethodBefore(T instance, string name, object[] ps)
        {
            InternalWriteLine(instance, name, ps, "Before");
        }

        /// <summary>
        /// Simple logging
        /// </summary>
        public static void LogMethodAfter(T instance, string name, object[] ps)
        {
            InternalWriteLine(instance, name, ps, "After");
        }

        public static K LogGetPropertyAfter<K>(T instance, string propertyName, K returnValue)
        {
            InternalWriteLine(instance, propertyName, new object[] { }, "After");
            return returnValue;
        }

        public static void LogGetPropertyBefore(T instance, string propertyName)
        {
            InternalWriteLine(instance, propertyName, new object[] { }, "Before");
        }

        public static K LogSetPropertyBefore<K>(T instance, string propertyName, K oldValue, K value)
        {
            InternalWriteLine(instance, propertyName, new object[] { value }, "Before");
            return value;
        }

        public static void LogSetPropertyAfter<K>(T instance, string propertyName, K oldValue, K value, K newValue)
        {
            InternalWriteLine(instance, propertyName, new object[] { value }, "After");
        }
    }
}
