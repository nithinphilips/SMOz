using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibSmoz.Commands;
using LibSmoz.Commands.IO;
using LibSmoz.Commands.UI;
using LibSmoz.Model;

namespace LibSmoz.Transformation
{
    public class Template : HashSet<Category>
    {
        public IEnumerable<Command> CleanupStartMenu(StartMenu startMenu)
        {
            foreach (var programCategory in startMenu)
            {
                int count = 0;
                foreach (var realLocation in programCategory.RealLocations)
                {
                    count += Directory.GetFiles(realLocation).Length;
                    count += Directory.GetDirectories(realLocation).Length;
                }

                if (count == 0)
                    yield return new DeleteFileCommand(programCategory.RealLocations);
            }

            foreach (var category in this)
            {
                if (!category.IsRestricted) continue;
                ProgramCategory programCategory = new ProgramCategory(startMenu, category.RestrictedPath);

                int count = 0;
                foreach (var realLocation in programCategory.RealLocations)
                {
                    count += Directory.GetFiles(realLocation).Length;
                    count += Directory.GetDirectories(realLocation).Length;
                }

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
