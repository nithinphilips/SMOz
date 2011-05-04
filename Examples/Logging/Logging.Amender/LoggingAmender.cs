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
        }

        /// <summary>
        /// Perform any generic amdendments here
        /// </summary>
        public override void Amend()
        {
            base.Amend();
        }

        /// <summary>
        /// Method which is called on every method in a class to be amended
        /// </summary>
        public override void Amend(Method method)
        {
            if(!ShouldAmend(method.MethodInfo))
                return;

            Console.WriteLine(String.Format("Amending {0}", method.Name));
            base.Amend(method);

            method.Before(Logger<T>.LogMethodBefore);
			method.After(Logger<T>.LogMethodAfter);
        }

        /// <summary>
        /// Amending Constructors
        /// </summary>
        public override void Amend(Constructor constructor)
        {
            if (!ShouldAmend(constructor.ConstructorInfo))
                return;

            Console.WriteLine(String.Format("Amending {0}", constructor.Name));
            base.Amend(constructor);

			constructor.Before(Logger<T>.LogMethodBefore);
			constructor.After(Logger<T>.LogMethodAfter);
        }
        
        /// <summary>
        /// Amending properties
        /// </summary>
        public override void Amend<TProperty>(Property<TProperty> property)
        {
            if (!ShouldAmend(property.PropertyInfo))
                return;

            Console.WriteLine(String.Format("Amending {0}", property.Name));

            base.Amend<TProperty>(property);
			property.AfterGet = Logger<T>.LogGetPropertyAfter<TProperty>;
			property.BeforeGet = Logger<T>.LogGetPropertyBefore;

			property.BeforeSet = Logger<T>.LogSetPropertyBefore<TProperty>;
			property.AfterSet = Logger<T>.LogSetPropertyAfter<TProperty>;
        }

        private bool ShouldAmend(dynamic info)
        {
            var v = info.GetCustomAttributes(typeof(DisableLoggingAttribute), true);
            return v.Length == 0;
        }
    }
}

