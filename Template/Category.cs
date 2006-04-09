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
 *  Original FileName :  Category.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using SMOz.Utilities;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace SMOz.Template
{
    [Serializable]
    public class Category : IComparable<Category>
    {
	   public Category() {
		  this.items = new List<CategoryItem>();
	   }

	   public Category(int count) {
		  this.items = new List<CategoryItem>(count);
	   }

	   public Category(string name, string restrictedPath) : this() {
		  this.name = name;
		  this.restrictedPath = restrictedPath;
	   }

	   protected string restrictedPath = string.Empty;
	   protected string name = string.Empty;
	   protected List<CategoryItem> items;

	   public ReadOnlyCollection<CategoryItem> Items { 
		  get { return this.items.AsReadOnly(); }
	   }

	   public string Name {
		  get { return name; }
		  set { name = value; }
	   }

	   public string RestrictedPath {
		  get { return restrictedPath; }
		  set { restrictedPath = value; }
	   }

	   public bool IsRestricted {
		  get { return !string.IsNullOrEmpty(restrictedPath); }
	   }

	   public void Add(CategoryItem item) {
		  item.Parent = this;
		  this.items.Add(item);
	   }

	   public void AddRange(CategoryItem[] items) {
		  for (int i = 0; i < items.Length; i++) {
			 items[i].Parent = this;
		  }
		  this.items.AddRange(items);
	   }

	   public bool Contains(CategoryItem item) {
		  return items.Contains(item);
	   }

	   public bool Remove(CategoryItem categoryItem) {
		  return items.Remove(categoryItem);
	   }
	   
	   public bool Match(string value) {
		  CategoryItem result;
		  return Match(value, out result);
	   }

	   public bool Match(string value, out CategoryItem item) {
		  item = null;
		  for (int i = 0; i < this.items.Count; i++) {
			 Regex regex = new Regex(this.items[i].Pattern, Utility.REGEX_OPTIONS);
			 if (regex.Match(value).Success) {
				item = this.items[i];
				return true;
			 }
		  }
		  return false;
	   }

	   public void TrimExcess() {
		  this.items.TrimExcess();
	   }

	   public CategoryItem this[int index] {
		  get { return this.items[index];  }
	   }

	   public int Count {
		  get { return items.Count; }
	   }

	   public override string ToString() {
		  return this.ToFormat();
	   }

	   public string ToFormat() {
		  if (string.IsNullOrEmpty(this.restrictedPath)) {
			 return this.name;
		  } else {
			 return this.name + "->" + this.restrictedPath;
		  }
	   }

	   public static Category FromFormat(string format) {
		  return FromFormat(format, 10);
	   }

	   public static Category FromFormat(string format, int capacity) {
		  Category newCategory = new Category(capacity);
		  if (format.Contains("->")) {
			 string[] pieces = format.Split(new string[] { "->" }, StringSplitOptions.None);
			 if (pieces.Length == 2) {
				newCategory.Name = pieces[0];
				newCategory.RestrictedPath = pieces[1];
			 } else {
				throw new ArgumentException();
			 }
		  } else {
			 newCategory.Name = format;
			 newCategory.RestrictedPath = string.Empty;
		  }
		  return newCategory;
	   }

	   #region IComparable<Category> Members

	   // Compared by: name, restrictedPath
	   public int CompareTo(Category other) {
		  int result = 0;
		  result = string.Compare(this.name, other.name, Utility.IGNORE_CASE);
		  if (result == 0) { result = string.Compare(this.restrictedPath, other.restrictedPath, Utility.IGNORE_CASE); }
		  return result;
	   }

	   #endregion

	   
    }
}
