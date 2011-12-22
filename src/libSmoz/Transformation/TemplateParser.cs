using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibSmoz.Ini;

namespace LibSmoz.Transformation
{
    public class TemplateParser
    {
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

        public static Template Parse(string file)
        {
            return Parse(IniParser.Parse(file));
        }

        public static Template Parse(Dictionary<string, HashSet<string>> sections)
        {
            Template template = new Template();

            foreach (var pair in sections)
            {
                Category category = CategoryFromFormat(pair.Key);
                category.AddRange(pair.Value.Select(item => CategoryItemFromFormat(item)));
                template.Add(category);
            }

            return template;
        }


        public static Category CategoryFromFormat(string format)
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

        public static string CategoryToFormat(Category category)
        {
            return string.IsNullOrEmpty(category.RestrictedPath)
                       ? category.Name
                       : category.Name + Category.RestCatSelector + category.RestrictedPath;
        }


        public static CategoryItem CategoryItemFromFormat(string format)
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

        public static string CategoryItemToFormat(CategoryItem item)
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
