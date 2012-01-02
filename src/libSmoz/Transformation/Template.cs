using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibSmoz.Commands;
using LibSmoz.Commands.IO;
using LibSmoz.Commands.UI;
using LibSmoz.Comparators;
using LibSmoz.Model;

namespace LibSmoz.Transformation
{
    public class Template : HashSet<Category>
    {
        public Template():
            base(EqualityComparers.CategoryComparer)
        {
        }

        public void Merge(Template t)
        {
            foreach (var category in t)
            {
                if (Add(category)) continue;

                var myCategory = this.FirstOrDefault(q => q.Equals(category));
                myCategory.Merge(category);
            }    
        }

        public IEnumerable<Command> CleanupStartMenu(StartMenu startMenu)
        {
            // Look through all existing program categories.
            foreach (var programCategory in startMenu.Where(p => p.RealLocations.Count() > 0))
            {
                int count = programCategory.RealLocations.Sum(
                                    l => Directory.GetFiles(l).Length + Directory.GetDirectories(l).Length
                            );

                if (count == 0)
                    yield return new DeleteFileCommand(programCategory.RealLocations);
            }

            // Look though all restricted categories
            foreach (var programCategory in this.Where(c => c.IsRestricted)
                                                .Select(c => new ProgramCategory(startMenu, c.RestrictedPath)))
            {
                if (programCategory.RealLocations.Count() == 0) continue;

                int count = programCategory.RealLocations.Sum(
                                l => Directory.GetFiles(l).Length + Directory.GetDirectories(l).Length
                            );

                if (count == 0)
                    yield return new DeleteFileCommand(programCategory.RealLocations);
            }
        }

        public IEnumerable<Command> TransformStartMenu(StartMenu startMenu)
        {
            string matchedSelector;

            // Apply to uncategorized items
            foreach (var item in startMenu.Get(""))
            {
                string catStr = FindCategory(item, false, out matchedSelector);
                if (catStr == null) continue;

                var newCategory = startMenu.GetOrCreate(catStr);
                yield return new MoveProgramItemCommand(item, newCategory, matchedSelector);
            }

            // Apply to restricted items
            foreach (var restrictedCat in this.Where(cat => cat.IsRestricted))
            {
                var programCategory = startMenu.GetOrLoad(restrictedCat.RestrictedPath);
                if (programCategory == null) continue;

                foreach (var item in programCategory)
                {
                    foreach (var catItem in restrictedCat.Where(catItem => catItem.RegexObject.IsMatch(item.Name)))
                    {
                        var newCategory = startMenu.GetOrCreate(restrictedCat.Name);
                        yield return new MoveProgramItemCommand(item, newCategory, catItem.Value);
                        break;                      
                    }                    
                }
            }
        }

        public string FindCategory(ProgramItem item, bool processRestricted, out string matchedSelector)
        {
            foreach (var category in this)
            {
                if (!processRestricted && category.IsRestricted) continue;
                foreach (var catItem in category.Where(catItem => catItem.RegexObject.IsMatch(item.Name)))
                {
                    matchedSelector = catItem.Value;
                    return category.Name;
                }
            }
            matchedSelector = null;
            return null;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var category in this)
            {
                sb.AppendLine(category.ToString());
            }
            return sb.ToString();
        }
    }
}
