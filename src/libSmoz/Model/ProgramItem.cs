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
        public ProgramItem(ProgramCategory category, string name, bool isDirectory = false)
        {
            this.Name = name;
            this.Category = category;
            this.IsDirectory = isDirectory;
        }


        #region Overrides

        public override string ToString()
        {
            return string.Format("{2}{0} ({1})", this.Name, this.RealLocations.Count(), IsDirectory ? "[D] " : "");
        }

        public int CompareTo(ProgramItem other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(ProgramItem other)
        {
            return this.Name.Equals(other.Name, Common.DefaultStringComparison);
        }

        public override bool Equals(object obj)
        {
            return Equals((ProgramItem)obj);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// Returns the string represenation of this program item.
        /// The string representation is the same as ProgramItem.Name
        /// </summary>
        /// <param name="x">The program item.</param>
        /// <returns>The string represenation.</returns>
        public static implicit operator string(ProgramItem x)
        {
            return x.Name;
        }

        /// <summary>
        /// Determines whether two program items are equal.
        /// Two ProgramItems are equal when they both have the same name.
        /// </summary>
        /// <param name="a">The first item.</param>
        /// <param name="b">The second item.</param>
        /// <returns>Returns true, if a and be are equal. Otherwise, false.</returns>
        public static bool operator ==(ProgramItem a, ProgramItem b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Determines whether two category items are not equal.
        /// Two ProgramItems are equal when they both have the same name.
        /// </summary>
        /// <param name="a">The first item.</param>
        /// <param name="b">The second item.</param>
        /// <returns>Returns true, if a and be are not equal. Otherwise, false.</returns>
        public static bool operator !=(ProgramItem a, ProgramItem b)
        {
            return !(a == b);
        }

        #endregion
    }
}
