using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace LibSmoz
{
    public class ProgramItem : IEquatable<ProgramItem>, IComparable<ProgramItem>
    {
        /// <summary>
        /// The name of the ProgramItem.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category that holds this item.
        /// If the programs is not associated with any category, returns null.
        /// </summary>
        public ProgramCategory Category { get; internal set; }

        /// <summary>
        /// Gets all paths that this item can refer to. Some of the paths may not exist.
        /// </summary>
        public ReadOnlyCollection<string> Paths { get; protected set; }

        internal void UpdatePaths()
        {
            List<string> list = new List<string>(this.Category.Paths.Count);
            foreach (var item in this.Category.Paths)
            {
                list.Add(System.IO.Path.Combine(item, this.Name));
            }
            this.Paths = list.AsReadOnly();
        }

        public bool Equals(ProgramItem other)
        {
            return this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public int CompareTo(ProgramItem other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            string s = this.Name;
            foreach (var item in Paths)
            {
                s += Environment.NewLine + item;
            }
            return s;
        }

    }
}
