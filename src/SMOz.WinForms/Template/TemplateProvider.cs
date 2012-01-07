using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SMOz.Ini;
using SMOz.Utilities;

namespace SMOz.Template
{
    [Serializable]
    public class TemplateProvider : ICategoryProvider
    {
        private List<Category> categories;

        public TemplateProvider()
            : this(new List<Category>())
        {
        }

        public TemplateProvider(IEnumerable<Category> categories)
        {
            this.categories = new List<Category>(categories);
        }

        public ReadOnlyCollection<Category> Categories
        {
            get { return categories.AsReadOnly(); }
        }

        public Category this[int index]
        {
            get { return categories[index]; }
        }

        public int Count
        {
            get { return categories.Count; }
        }

        #region ICategoryProvider Members

        public Category FindCategory(string item)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].Match(item))
                {
                    return categories[i];
                }
            }
            return null;
        }

        #endregion

        public static TemplateProvider FromFile(string file)
        {
            TemplateProvider template = new TemplateProvider();
            IniSection[] sections = IniParser.Parse(file);

            for (int i = 0; i < sections.Length; i++)
            {
                Category category = Category.FromFormat(sections[i].Name);

                template.categories.Add(category);

                CategoryItem[] items = new CategoryItem[sections[i].Count];
                for (int j = 0; j < sections[i].Count; j++)
                {
                    items[j] = CategoryItem.FromFormat(sections[i][j]);
                }
                category.AddRange(items);
            }
            return template;
        }

        public IEnumerable<string> ToStringArray()
        {
            List<string> newList = new List<string>();
            foreach (var category in categories)
            {
                var paths = new List<string>();
                paths.AddRange(Utility.PathToTree(category.Name));
                if(category.IsRestricted) paths.AddRange(Utility.PathToTree(category.RestrictedPath));

                foreach (var path in paths)
                    if(!newList.Contains(path)) newList.Add(path);
            }

            return newList;
        }

        public void Add(Category category)
        {
            categories.Add(category);
        }

        public void AddRange(Category[] category)
        {
            categories.AddRange(categories);
        }

        public void Remove(Category category)
        {
            categories.Remove(category);
        }

        public void Remove(string name)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].Name == name)
                {
                    categories.RemoveAt(i);
                    break;
                }
            }
        }

        public bool Contains(Category category)
        {
            return categories.Contains(category);
        }

        public bool Contains(string format)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].ToFormat().CompareTo(format) == 0)
                {
                    return true;
                }
            }
            return false;
        }


        public void Merge(TemplateProvider other)
        {
            foreach (Category otherCategory in other.categories)
            {
                Category existingCategory = categories.Find(
                    delegate(Category match) { return (string.Compare(match.ToFormat(), otherCategory.ToFormat(), Utility.IGNORE_CASE) == 0); });
                if (existingCategory != null)
                {
                    for (int i = 0; i < otherCategory.Count; i++)
                    {
                        if (!existingCategory.Contains(otherCategory[i]))
                        {
                            existingCategory.Add(otherCategory[i]);
                        }
                    }
                }
                else
                {
                    categories.Add(otherCategory);
                }
            }
        }
    }
}