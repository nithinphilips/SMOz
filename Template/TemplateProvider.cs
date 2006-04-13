/*************************************************************************
 *  SMOz (Start Menu Organizer)
 *  Copyright (C) 2006 Nithin Philips
 *
 *  This program is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU General Public License
 *  as published by the Free Software Foundation; either version 2
 *  of the License, or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation,Inc.,59 Temple Place - Suite 330,Boston,MA 02111-1307, USA.
 *
 *  Author            :  Nithin Philips <spikiermonkey@users.sourceforge.net>
 *  Original FileName :  TemplateProvider.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using SMOz.Ini;
using SMOz.Utilities;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace SMOz.Template
{
    [Serializable]
    public class TemplateProvider : ICategoryProvider
    {
	   public TemplateProvider()
		  : this(new List<Category>()) { }

	   public TemplateProvider(IEnumerable<Category> categories) {
		  this.categories = new List<Category>(categories);
	   }

	   public static TemplateProvider FromFile(string file) {
		  TemplateProvider template = new TemplateProvider();
		  IniSection[] sections = IniParser.Parse(file);

		  for (int i = 0; i < sections.Length; i++) {
			 Category category = Category.FromFormat(sections[i].Name);

			 template.categories.Add(category);

			 CategoryItem[] items = new CategoryItem[sections[i].Count];
			 for (int j = 0; j < sections[i].Count; j++) {
				items[j] = CategoryItem.FromFormat(sections[i][j]);
			 }
			 category.AddRange(items);
		  }
		  return template;
	   }

	   List<Category> categories;

	   public ReadOnlyCollection<Category> Categories {
		  get {
			 return categories.AsReadOnly();
		  }
	   }

	   public Category this[int index] {
		  get { return categories[index]; }
	   }

	   public int Count {
		  get { return categories.Count; }
	   }

	   public string[] ToStringArray() {
		  List<string> newList = new List<string>();
		  for (int i = 0; i < this.categories.Count; i++) {
		      string[] tree = Utility.PathToTree(categories[i].Name);
		      for (int j = 0; j < tree.Length; j++) {
		          if (!newList.Contains(tree[j])) {
		              newList.Add(tree[j]);
//		              Debug.WriteLine(tree[j]);
		          }
		      }
		  }
		  return newList.ToArray();
	   }

	   public void Add(Category category) {
		  this.categories.Add(category);
	   }

	   public void AddRange(Category[] category) {
		  this.categories.AddRange(categories);
	   }

	   public void Remove(Category category) {
		  this.categories.Remove(category);
	   }

	   public void Remove(string name) {
		  for (int i = 0; i < this.categories.Count; i++) {
			 if (categories[i].Name == name) {
				categories.RemoveAt(i);
				break;
			 }
		  }
	   }

	   public bool Contains(Category category) {
		  return this.categories.Contains(category);
	   }

	   public bool Contains(string format) {
		  for (int i = 0; i < this.categories.Count; i++) {
			 if (categories[i].ToFormat().CompareTo(format) == 0) {
				return true;
			 }
		  }
		  return false;
	   }

	   public Category FindCategory(string item) {
		  for (int i = 0; i < this.categories.Count; i++) {
			 if (this.categories[i].Match(item)) {
				return this.categories[i];
			 }
		  }
		  return null;
	   }


	   public void Merge(TemplateProvider other) {
		  foreach (Category otherCategory in other.categories) {
			 Category existingCategory = this.categories.Find(
				delegate(Category match) {
				    return (string.Compare(match.ToFormat(), otherCategory.ToFormat(), Utility.IGNORE_CASE) == 0); 
				});
			 if (existingCategory != null) {
				for (int i = 0; i < otherCategory.Count; i++) {
				    if (!existingCategory.Contains(otherCategory[i])) {
					   existingCategory.Add(otherCategory[i]);
				    }
				}
			 } else {
				this.categories.Add(otherCategory);
			 }
		  }
	   }
    }
}
