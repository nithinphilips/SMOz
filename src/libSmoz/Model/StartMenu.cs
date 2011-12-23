using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LibSmoz.Comparators;

namespace LibSmoz.Model
{
    public class StartMenu : HashSet<ProgramCategory>
    {
        private List<string> locations = new List<string>();

        /// <summary>
        /// A list of folder names that are to be considered categories.
        /// </summary>
        public ICollection<string> KnownCategories { get; set; }

        /// <summary>
        /// All the directories where the StartMenu is located.
        /// </summary>
        public IEnumerable<string> Locations { get { return locations; } }

        /// <summary>
        /// Creates a new instance of StartMenu.
        /// </summary>
        public StartMenu()
            : base(new ProgramCategoryEqualityComparer())
        { }

        /// <summary>
        /// Retrives the instance of a ProgramCategory, if any, in this StartMenu which is identical to <code>item</code>.
        /// </summary>
        /// <param name="item">The item to look for.</param>
        /// <returns>If matched, a ProgramCategory, otherwise null.</returns>
        public ProgramCategory Get(ProgramCategory item)
        {
            return this.FirstOrDefault(category => category.Equals(item));
        }

        /// <summary>
        /// Retrives the instance of a ProgramCategory, if any, in this StartMenu whose name is <code>name</code>.
        /// </summary>
        /// <param name="name">The name of the item to look for.</param>
        /// <returns>If matched, a ProgramCategory, otherwise null.</returns>
        public ProgramCategory Get(string name)
        {
            return Get(new ProgramCategory(this, name));
        }

        /// <summary>
        /// Retrives the instance of a ProgramCategory, if any, in this StartMenu whose name is <code>name</code>.
        /// If none are found, any existing directories will be checked.
        /// <remarks>
        /// If the category was not already in this StartMenu, it will not be added. You must do that manually by calling the <code>Add</code> method.
        /// </remarks>
        /// </summary>
        /// <param name="name">The name of the item to look for.</param>
        /// <returns>If found, a ProgramCategory, otherwise null.</returns>
        public ProgramCategory GetOrLoad(string name)
        {
            ProgramCategory cat = Get(new ProgramCategory(this, name));
            if (cat != null) return cat;

            cat = new ProgramCategory(this, name);
            if (cat.RealLocations.Count() > 0)
            {
                cat.FindItems();
                return cat;
            }
            return null;
        }

        /// <summary>
        /// Retrives the instance of a ProgramCategory in this StartMenu whose name is <code>name</code> or creates it.
        /// </summary>
        /// <param name="name">The name of the item to look for.</param>
        /// <returns>If matched, a ProgramCategory, otherwise null.</returns>
        public ProgramCategory GetOrCreate(string name)
        {
            ProgramCategory cat = Get(new ProgramCategory(this, name));
            if (cat != null) return cat;

            cat = new ProgramCategory(this, name);
            cat.FindItems();
            this.Add(cat);
            return cat;
        }

        /// <summary>
        /// Adds some locations to be managed by this StartMenu instance.
        /// </summary>
        /// <param name="locations">Locations to manage</param>
        public void AddLocations(IEnumerable<string> locations)
        {
            if (locations == null) throw new ArgumentNullException("locations");

            foreach (var location in locations)
                AddLocation(location);
        }

        /// <summary>
        /// Adds a location to be managed by this StartMenu instance.
        /// </summary>
        /// <param name="location">The locations to manage.</param>
        public void AddLocation(string location)
        {
            var nLocation = NormalizeLocation(location);
            locations.Add(nLocation);

            // The root is also a category
            var pCategory = new ProgramCategory(this, "");
            if (Add(pCategory)) pCategory.FindItems();
            else Get(pCategory).FindItems();

            FindCategories(nLocation);
        }

        /// <summary>
        /// Removes a location from this StartMenu.
        /// </summary>
        /// <param name="location">The location to remove.</param>
        public void RemoveLocation(string location)
        {
            var nLocation = NormalizeLocation(location);
            locations.Remove(nLocation);
        }

        /// <summary>
        /// Searches the location to find known categories.
        /// </summary>
        /// <param name="location">The directory to search in.</param>
        void FindCategories(string location)
        {
            foreach (var item in Directory.GetDirectories(location, "*", SearchOption.AllDirectories))
            {
                var itemStub = item.Replace(location, "");

                if (!KnownCategories.Contains(itemStub)) continue;

                var pCategory = new ProgramCategory(this, itemStub);
                if (Add(pCategory)) pCategory.FindItems();
                else Get(pCategory).FindItems();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var category in this)
            {
                sb.AppendLine(category.ToString());
                foreach (var location in category.RealLocations) sb.AppendLine("  " + location);
                foreach (var items in category)
                    sb.AppendLine("  " + items);

            }

            return sb.ToString();
        }

        static string NormalizeLocation(string location)
        {
            if (!Path.IsPathRooted(location)) location = Path.GetFullPath(location);
            return location.EndsWith(Path.DirectorySeparatorChar.ToString()) ? location : location + Path.DirectorySeparatorChar;
        }
    }
}
