using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibSmoz.Comparators;

namespace LibSmoz.Model
{
    public class ProgramCategory : HashSet<ProgramItem>, IEquatable<ProgramCategory>, IComparable<ProgramCategory>, IStartMenuItem
    {
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

        /// <summary>
        /// The start menu item to which the category belongs to.
        /// </summary>
        public StartMenu Root { get; internal set; }

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
        /// Creates a new instance of ProgramCategory.
        /// </summary>
        /// <param name="startMenu">The start menu to which this category belongs to.</param>
        /// <param name="name">The name of this category.</param>
        public ProgramCategory(StartMenu startMenu, string name)
            : base(new ProgramItemEqualityComparer())
        {
            this.Root = startMenu;
            this.Name = name;
        }

        /// <summary>
        /// Searches all the RealLocations of this category and finds all ProgramItems within.
        /// </summary>
        public void FindItems()
        {
            foreach (var item in RealLocations.SelectMany(location => Directory.GetFiles(location, "*.lnk")))
            {
                Add(new ProgramItem(this, Path.GetFileName(item), false));
            }

            foreach (var item in RealLocations.SelectMany(Directory.GetDirectories))
            {
                string fileName = Path.GetFileName(item);
                string fullName = Path.Combine(Name, fileName);
                if (!Root.KnownCategories.Contains(fullName))
                    Add(new ProgramItem(this, fileName, true));
                else
                    Console.WriteLine("Ignoring Known Category: {0}", fullName);

            }
        }

        public bool Equals(ProgramCategory other)
        {
            return ProgramCategoryEqualityComparer.Instance.Equals(this, other);
        }

        public int CompareTo(ProgramCategory other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", string.IsNullOrEmpty(this.Name) ? "<default>" : this.Name,
                                 RealLocations.Count());
        }
    }
}
