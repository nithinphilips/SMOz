using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using LibSmoz.Comparators;

namespace LibSmoz.Model
{
    public class ProgramItem : IEquatable<ProgramItem>, IComparable<ProgramItem>, IStartMenuItem
    {
        private ProgramCategory _category;
        private string _name;

        /// <summary>
        /// The category to which this ProgramItem belongs to.
        /// </summary>
        /// <remarks>
        /// Settings this property does not automatically add this item to the given category.
        /// You must call the Add method on the ProgramCategory to do that.
        /// </remarks>
        public ProgramCategory Category
        {
            get { return _category; }
            internal set
            {
                if(_category == value) return;
                _category = value;
                SetSelectors();
            }
        }

        /// <summary>
        /// The name of this item, including any extension.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if(_name == value) return;
                _name = value; 
                SetSelectors();
            }
        }

        /// <summary>
        /// If this item is a directory, returns true.
        /// </summary>
        public bool IsDirectory { get; private set; }

        /// <summary>
        /// A list of all possible locations where this item could exist.
        /// </summary>
        public IEnumerable<string> Locations { get; protected set; }

        /// <summary>
        /// A list of all locations where this item actually exists.
        /// </summary>
        public IEnumerable<string> RealLocations { get; protected set; }

        
        /// <summary>
        /// Creates the LINQ selectors for Locations and RealLocations properties.
        /// While the selectors are evaluated lazily, they must be re-created if the 
        /// Category or Name properties change, in order to get the correct results.
        /// </summary>
        void SetSelectors()
        {
            if (Category == null || Name == null) return;

            this.Locations = from l in Category.Locations
                             select Path.Combine(l, this.Name);

            this.RealLocations = from l in this.Locations
                                 where IsDirectory ? Directory.Exists(l) : File.Exists(l)
                                 select l;
        }

        /// <summary>
        /// Create a new instance of ProgramItem.
        /// </summary>
        /// <param name="category">The initial category to which this item belongs to.</param>
        /// <param name="name">The name of this item.</param>
        /// <param name="isDirectory">Whether or not this item is a directory.</param>
        public ProgramItem(ProgramCategory category, string name, bool isDirectory)
        {
            this.Name = name;
            this.Category = category;
            this.IsDirectory = isDirectory;
        }

        public bool Equals(ProgramItem other)
        {
            return ProgramItemEqualityComparer.Instance.Equals(this, other);
        }

        public int CompareTo(ProgramItem other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return string.Format("{2}{0} ({1})", this.Name, this.RealLocations.Count(), IsDirectory ? "[D] " : "");
        }

    }
}
