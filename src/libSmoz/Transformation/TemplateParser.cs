﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibSmoz.Ini;

namespace LibSmoz.Transformation
{
    /// <summary>
    /// The TemplateParser provides methods to parse a template ini file can create a Template object and vice versa.
    /// </summary>
    public static class TemplateParser
    {
        /// <summary>
        /// Persisis a Template object into a file.
        /// </summary>
        /// <param name="template">The template object to save.</param>
        /// <param name="file">The file to save the data into.</param>
        public static void Save(Template template, string file)
        {
            IniWriter writer = new IniWriter();
            foreach (Category category in template)
            {
                string strCategory = CategoryToFormat(category);

                foreach (var item in category)
                {
                    writer.AddValue(CategoryItemToFormat(item), strCategory);
                }
            }
            string tempFile = Path.GetTempFileName();
            writer.Save(tempFile);
            File.Copy(tempFile, file, true);
            File.Delete(tempFile);
        }

        /// <summary>
        /// Reads a template ini file and creates a Template object from it.
        /// </summary>
        /// <param name="lines">The template file as an enumeration of lines.</param>
        /// <returns>The new template.</returns>
        public static Template Parse(IEnumerable<string> lines)
        {
            return Parse(IniParser.Parse(lines));
        }

        /// <summary>
        /// Reads a template ini file and creates a Template object from it.
        /// </summary>
        /// <param name="file">The template file.</param>
        /// <returns>The new template.</returns>
        public static Template Parse(string file)
        {
            return Parse(IniParser.Parse(file));
        }

        /// <summary>
        /// Reads a template ini file and creates a Template object from it.
        /// </summary>
        /// <param name="sections">The template file as a dictionary, keyed by the category</param>
        /// <returns>The new template.</returns>
        public static Template Parse(Dictionary<string, HashSet<string>> sections)
        {
            Template template = new Template();

            foreach (var pair in sections)
            {
                Category category = CategoryFromFormat(pair.Key);
                foreach (var categoryItem in pair.Value.Select(CategoryItemFromFormat))
                {
                    category.Add(categoryItem);
                }
                template.Add(category);
            }

            return template;
        }

        internal static Category CategoryFromFormat(string format)
        {
            Category newCategory = new Category();
            if (format.Contains(Common.RestrictedCategorySelector))
            {
                string[] pieces = format.Split(new string[] { Common.RestrictedCategorySelector }, StringSplitOptions.None);
                if (pieces.Length == 2)
                {
                    newCategory.Name = pieces[0];
                    newCategory.RestrictedPath = pieces[1];
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                newCategory.Name = format;
                newCategory.RestrictedPath = string.Empty;
            }
            return newCategory;
        }

        internal static string CategoryToFormat(Category category)
        {
            return string.IsNullOrEmpty(category.RestrictedPath)
                       ? category.Name
                       : category.Name + Common.RestrictedCategorySelector + category.RestrictedPath;
        }


        internal static CategoryItem CategoryItemFromFormat(string format)
        {
            // use the implicit cast operator
            return format;
        }

        internal static string CategoryItemToFormat(CategoryItem item)
        {
            // use the implicit cast operator
            return item;
        }
    }
}
