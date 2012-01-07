using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibSmoz
{
    /// <summary>
    /// Contains commonly used or helper class for building SMOz applications.
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class NamespaceDoc { }

    public class KnownCategories
    {
        private KnownCategories()
        {
            Items = new HashSet<string>();
        }

        public ICollection<string> Items { get; set; }

        private static KnownCategories _instance;

        public static KnownCategories Instance
        {
            get { return _instance ?? (_instance = new KnownCategories()); }
        }
    }
}
