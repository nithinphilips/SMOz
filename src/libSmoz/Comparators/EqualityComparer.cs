using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibSmoz.Comparators
{
    ///<summary>
    /// A generic Equality comparer.
    /// It calls the Equals method on the type.
    ///</summary>
    ///<typeparam name="T">Tye type to compare</typeparam>
    public class EqualityComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
