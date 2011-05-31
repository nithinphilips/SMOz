using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afterthought;

namespace Logging.Amender
{
    /// <summary>
    /// This object aments a given class
    /// </summary>
    public class LoggingAmender<T> : Amendment<T, T>
    {
        /// <summary>
        /// The construcotr
        /// </summary>
        public LoggingAmender()
        {
			// Constructors
			Constructors
				.Where(c => ShouldAmend(c.ConstructorInfo))
				.Before(Logger<T>.LogMethodBefore)
				.After(Logger<T>.LogMethodAfter);

			// Properties
			Properties
				.Where(p => ShouldAmend(p.PropertyInfo))
				.BeforeGet(Logger<T>.LogGetPropertyBefore)
				.AfterGet(Logger<T>.LogGetPropertyAfter)
				.BeforeSet(Logger<T>.LogSetPropertyBefore)
				.AfterSet(Logger<T>.LogSetPropertyAfter);

			// Methods
			Methods
				.Where(m => ShouldAmend(m.MethodInfo))
				.Before(Logger<T>.LogMethodBefore)
				.After(Logger<T>.LogMethodAfter);
		}

        private bool ShouldAmend(dynamic info)
        {
            var v = info.GetCustomAttributes(typeof(DisableLoggingAttribute), true);
            return v.Length == 0;
        }
    }
}

