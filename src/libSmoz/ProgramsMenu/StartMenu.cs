using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibSmoz.Comparators;

namespace LibSmoz.ProgramsMenu
{
    /// <summary>
    /// <para>Contains classes that can represent a Windows Start Menu.</para> 
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class NamespaceDoc { }
  

    /// <summary>
    /// <para>Represents a Windows Start Menu</para>
    /// <para>A Windows Start Menu can have multiple <see cref="StartMenu.Locations"/>, however it is addressed as if there is only a single location</para>
    /// <para>The StartMenu class will transparently merge the multiple locations and allows using the Start Menu like Windows does.</para>
    /// </summary>
    public class StartMenu : HashSet<ProgramCategory>
    {
        /// <summary>
        /// Creates a new instance of StartMenu.
        /// </summary>
        public StartMenu()
            :this(null)
        {
        }

        /// <summary>
        /// Creates a new instance of StartMenu.
        /// </summary>
        /// <param name="locations">A list of locations</param>
        public StartMenu(IEnumerable<string> locations)
            : this(new List<string>(), locations.ToArray())
        {
        }

        /// <summary>
        /// Creates a new instance of StartMenu
        /// </summary>
        /// <param name="knownCategories">A list of directories that are to be considered  categories.</param>
        /// <param name="locations"></param>
        public StartMenu(ICollection<string> knownCategories, IEnumerable<string> locations)
            : this(knownCategories, locations.ToArray())
        {
        }

        /// <summary>
        /// Creates a new instance of StartMenu.
        /// </summary>
        /// <param name="locations">A list of locations</param>
        public StartMenu(params string[] locations)
            :this(new List<string>(), locations)
        {
        }

        /// <summary>
        /// Creates a new instance of StartMenu.
        /// </summary>
        /// <param name="knownCategories">A list of directories that are to be considered  categories.</param>
        /// <param name="locations">A list of locations</param>
        public StartMenu(ICollection<string> knownCategories, params string[] locations)
            : base(EqualityComparers.ProgramCategoryComparer)
        {
            KnownCategories = knownCategories;

            if (locations != null)
                foreach (var location in locations) AddLocation(location);
        }

        private readonly List<string> _locations = new List<string>();

        /// <summary>
        /// Gets or sets a list of folder names that are to be considered categories.
        /// </summary>
        public ICollection<string> KnownCategories { get; set; }

        /// <summary>
        /// Gets all the directories where the Start Menu items are located.
        /// </summary>
        public IEnumerable<string> Locations { get { return _locations; } }

        /// <summary>
        /// Retrives the instance of a ProgramCategory, if any, in this StartMenu which is identical to <paramref name="item"/>
        /// </summary>
        /// <param name="item">The item to look for.</param>
        /// <returns>If matched, a ProgramCategory, otherwise null.</returns>
        public ProgramCategory Get(ProgramCategory item)
        {
            return this.FirstOrDefault(category => category.Equals(item));
        }

        /// <summary>
        /// Retrives the instance of a ProgramCategory, if any, in this StartMenu whose name is <see cref="ProgramCategory.Name"/>.
        /// </summary>
        /// <param name="name">The name of the item to look for.</param>
        /// <returns>If matched, a ProgramCategory, otherwise null.</returns>
        public ProgramCategory Get(string name)
        {
            return Get(new ProgramCategory(this, name));
        }

        /// <summary>
        /// Retrives the instance of a ProgramCategory, if any, in this StartMenu whose name is <see cref="ProgramCategory.Name"/>.
        /// If none are found, any existing directories will be checked.
        /// <remarks>
        /// If the category was not already in this StartMenu, it will not be added. You must do that manually by calling the <see cref="StartMenu.Add"/> method.
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
        /// Retrives the instance of a ProgramCategory in this StartMenu whose name is <see cref="ProgramCategory.Name"/> or creates it.
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
            _locations.Add(nLocation);

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
            _locations.Remove(nLocation);
        }

        /// <summary>
        /// Looks for any new items or categories.
        /// </summary>
        public void Refresh()
        {
            _locations.ForEach(FindCategories);
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
            //if (!Path.IsPathRooted(location)) location = Path.GetFullPath(location);
            return location.EndsWith(Path.DirectorySeparatorChar.ToString()) ? location : location + Path.DirectorySeparatorChar;
        }
    }
}
