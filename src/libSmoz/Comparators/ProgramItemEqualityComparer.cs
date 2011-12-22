using System;
using System.Collections.Generic;
using LibSmoz.Model;

namespace LibSmoz.Comparators
{
    public class ProgramItemEqualityComparer : IEqualityComparer<ProgramItem>
    {

        public static ProgramItemEqualityComparer Instance { get; private set; }

        static ProgramItemEqualityComparer()
        {
            Instance = new ProgramItemEqualityComparer();
        }

        public bool Equals(ProgramItem x, ProgramItem y)
        {
            return x.Name.Equals(y.Name, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(ProgramItem obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}