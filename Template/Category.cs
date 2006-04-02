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

namespace SMOz.Template
{
    [Serializable]
    public class Category : IEnumerable<CategoryItem>
    {
	   public Category() {
		  this.items = new List<CategoryItem>();
	   }

	   public Category(int count) {
		  this.items = new List<CategoryItem>(count);
	   }

	   public Category(string name) 
		  : this(null, name, string.Empty) { }

	   public Category(CategoryItem[] items)
		  : this(items, string.Empty, string.Empty) { }

	   public Category(string name, string restrictedPath)
		  : this(null, name, restrictedPath) { }

	   public Category(CategoryItem[] items, string name)
		  : this(items, name, string.Empty) { }

	   public Category(CategoryItem[] items, string name, string restrictedPath) {
		  this.items = new List<CategoryItem>();
		  if (items != null) { this.items.AddRange(items); }
		  this.name = name;
		  this.restrictedPath = restrictedPath;
	   }

	   protected string restrictedPath = string.Empty;
	   protected string name;
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

	   public void Add(CategoryItem item) {
		  this.items.Add(item);
	   }

	   public void AddRange(CategoryItem[] items) {
		  this.items.AddRange(items);
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
		  set { this.items[index] = value; }
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

	   #region IEnumerable<CategoryItem> Members

	   public IEnumerator<CategoryItem> GetEnumerator() {
		  return items.GetEnumerator();
	   }

	   #endregion

	   #region IEnumerable Members

	   System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
		  return items.GetEnumerator();
	   }

	   #endregion
    }
}
