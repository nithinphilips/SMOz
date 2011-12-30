using System;
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
                category.AddRange(pair.Value.Select(CategoryItemFromFormat));
                template.Add(category);
            }

            return template;
        }

        internal static Category CategoryFromFormat(string format)
        {
            Category newCategory = new Category();
            if (format.Contains(Category.RestCatSelector))
            {
                string[] pieces = format.Split(new string[] { Category.RestCatSelector }, StringSplitOptions.None);
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
                       : category.Name + Category.RestCatSelector + category.RestrictedPath;
        }


        internal static CategoryItem CategoryItemFromFormat(string format)
        {
            CategoryItem item = new CategoryItem();

            if (string.IsNullOrEmpty(format))
            {
                item.Value = "";
                item.Type = CategoryItemType.String;
            }
            else
            {
                char firstChar = format[0];
                switch (firstChar)
                {
                    case '*':
                        item.Type = CategoryItemType.WildCard;
                        item.Value = format.Substring(1);
                        break;
                    case '@':
                        item.Type = CategoryItemType.Regex;
                        item.Value = format.Substring(1);
                        break;
                    default:
                        item.Value = format;
                        item.Type = CategoryItemType.String;
                        break;
                }
            }
            return item;
        }

        internal static string CategoryItemToFormat(CategoryItem item)
        {
            string prefix = "";
            switch (item.Type)
            {
                case CategoryItemType.WildCard:
                    prefix = "*";
                    break;
                case CategoryItemType.Regex:
                    prefix = "@";
                    break;
            }

            return prefix + item.Value;
        }
    }
}
