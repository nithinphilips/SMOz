using System;
using System.Collections.Generic;
using LibSmoz.Model;

namespace LibSmoz.Comparators
{
    public class ProgramCategoryEqualityComparer : IEqualityComparer<ProgramCategory>
    {
        public static ProgramCategoryEqualityComparer Instance { get; private set; }

        static ProgramCategoryEqualityComparer()
        {
            Instance = new ProgramCategoryEqualityComparer();
        }

        public bool Equals(ProgramCategory x, ProgramCategory y)
        {
            return x.Name.Equals(y.Name, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsEqual(ProgramCategory x, ProgramCategory y)
        {
            return x.Name.Equals(y.Name, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(ProgramCategory obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}