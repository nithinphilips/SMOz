using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibSmoz.Comparators;

namespace LibSmoz.ProgramsMenu
{
    public class ProgramCategory : HashSet<ProgramItem>, IEquatable<ProgramCategory>, IComparable<ProgramCategory>, IStartMenuItem
    {

        /// <summary>
        /// Creates a new instance of ProgramCategory.
        /// </summary>
        /// <param name="startMenu">The start menu to which this category belongs to.</param>
        /// <param name="name">The name of this category.</param>
        public ProgramCategory(StartMenu startMenu, string name = null)
            : base(EqualityComparers.ProgramItemComparer)
        {
            this.Root = startMenu;
            this.Name = name ?? string.Empty;
        }

        private string _name;
        /// <summary>
        /// The name of this category.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                SetSelectors();
            }
        }

        private StartMenu _root;

        /// <summary>
        /// The start menu item to which the category belongs to.
        /// </summary>
        public StartMenu Root
        {
            get { return _root; }
            internal set
            {
                if (_root == value) return;
                _root = value;
                SetSelectors();
            }
        }

        /// <summary>
        /// A list of all possible locations where this category could exist.
        /// </summary>
        public IEnumerable<string> Locations { get; protected set; }

        /// <summary>
        /// A list of all locations where this category actually exists.
        /// </summary>
        public IEnumerable<string> RealLocations { get; protected set; }

        /// <summary>
        /// Creates the LINQ selectors for Locations and RealLocations properties.
        /// While the selectors are evaluated lazily, they must be re-created if the 
        /// Name property changes, in order to get the correct results.
        /// </summary>
        void SetSelectors()
        {

            this.Locations = from l in Root.Locations
                             select Path.Combine(l, this.Name);

            this.RealLocations = from l in this.Locations
                                 where Directory.Exists(l)
                                 select l;
        }

       

        /// <summary>
        /// Searches all the RealLocations of this category and finds all ProgramItems within.
        /// </summary>
        public void FindItems()
        {
            foreach (var item in RealLocations.SelectMany(Directory.GetDirectories))
            {
                string fileName = Path.GetFileName(item);
                string fullName = Path.Combine(Name, fileName);
                if (!Root.KnownCategories.Contains(fullName))
                    Add(new ProgramItem(this, fileName, true));
                else
                    Console.WriteLine("Ignoring Known Category: {0}", fullName);

            }
            
            foreach (var item in RealLocations.SelectMany(location => Directory.GetFiles(location, "*.lnk")))
            {
                Add(new ProgramItem(this, Path.GetFileName(item), false));
            }
        }

        #region Overrides

        public override string ToString()
        {
            return string.Format("{0} ({1})", string.IsNullOrEmpty(this.Name) ? "<default>" : this.Name,
                                 RealLocations.Count());
        }

        public int CompareTo(ProgramCategory other)
        {
            if (other == null) return 1;
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(ProgramCategory other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            var cObj = obj as ProgramCategory;
            return cObj == null || cObj == this;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// Determines whether two program items are equal.
        /// Two ProgramCategorys are equal when they both have the same name.
        /// </summary>
        /// <param name="a">The first item.</param>
        /// <param name="b">The second item.</param>
        /// <returns>Returns true, if a and be are equal. Otherwise, false.</returns>
        public static bool operator ==(ProgramCategory a, ProgramCategory b)
        {
            if ((object)a == null && (object)b == null) return true;
            if ((object)a == null || (object)b == null) return false;
            return a.Name.Equals(b.Name, Common.DefaultStringComparison);
        }

        /// <summary>
        /// Determines whether two category items are not equal.
        /// Two ProgramCategorys are equal when they both have the same name.
        /// </summary>
        /// <param name="a">The first item.</param>
        /// <param name="b">The second item.</param>
        /// <returns>Returns true, if a and be are not equal. Otherwise, false.</returns>
        public static bool operator !=(ProgramCategory a, ProgramCategory b)
        {
            return !(a == b);
        }

        #endregion
    }
}
