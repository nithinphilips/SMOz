﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibSmoz.Commands;
using LibSmoz.Commands.Io;
using LibSmoz.Commands.Ui;
using LibSmoz.Comparators;
using LibSmoz.ProgramsMenu;

namespace LibSmoz.Transformation
{
    /// <summary>
    /// Contains classes that apply a <see cref="Template"/> to a <see cref="StartMenu"/>
    /// and generate a collection of <see cref="Command"/> objects to effect the transformation.
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class NamespaceDoc { }

    /// <summary>
    /// <para>A Template holds a collection of categories and regex selectors within those categories.</para>
    /// <para>
    /// When a template is applied to a StartMenu (via the <see cref="Template.TransformStartMenu"/> method),
    /// we compare each start menu item to each of the selectors in a template and identifies the first <see cref="Category"/>
    /// that the <see cref="ProgramItem"/> can belong to. We then create a set of <see cref="Command"/>, 
    /// which when <see cref="Command.Execute">executed</see>, transforms the <see cref="StartMenu"/>.
    /// </para>
    /// </summary>
    public class Template : HashSet<Category>
    {
        /// <summary>
        /// Creates a new instance of Template.
        /// </summary>
        public Template():
            base(EqualityComparers.CategoryComparer)
        {
        }

        /// <summary>
        /// <para>Deep merges two templates.</para>
        /// <para>
        /// Any <see cref="Category"/> objects or <see cref="CategoryItem"/> objects that 
        /// are present in <paramref name="t"/>, but not in this instance, are copied.
        /// </para> 
        /// </summary>
        /// <param name="t">The template to merge.</param>
        public void Merge(Template t)
        {
            foreach (var category in t)
            {
                if (Add(category)) continue;

                var myCategory = this.FirstOrDefault(q => q.Equals(category));
                myCategory.UnionWith(category);
            }    
        }

        /// <summary>
        /// Looks through all the categories in <paramref name="startMenu"/> and returns 
        /// <see cref="DeleteFileCommand"/> commands for one that are empty.
        /// </summary>
        /// <remarks>
        /// This method should be called after the actual files in the start menu has been transformed.
        /// Applying the commands generated by this method should not invalidate the <paramref name="startMenu"/>,
        /// but it is not guaranteed.
        /// </remarks>
        /// <param name="startMenu">The <see cref="StartMenu"/> to cleanup.</param>
        /// <returns>A list of commands that can be applied to cleanup the <paramref name="startMenu"/></returns>
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
