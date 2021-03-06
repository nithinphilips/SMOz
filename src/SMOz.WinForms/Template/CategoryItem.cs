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
 *  Original FileName :  CategoryItem.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SMOz.Utilities;

namespace SMOz.Template{

    public enum CategoryItemType { String, WildCard, Regex };

    [Serializable]
    public class CategoryItem : IComparable<CategoryItem>, IEquatable<CategoryItem>
    {

	   public CategoryItem() : this(string.Empty, CategoryItemType.String) { }

	   public CategoryItem(string value, CategoryItemType type) {
		  this.value = value;
		  this.type = type;
	   }
	   
	   string value = string.Empty;
	   string pattern = string.Empty;
	   CategoryItemType type = CategoryItemType.String;
	   Category parent;

	   public Category Parent {
		  get { return parent; }
		  internal set {
			 if (this.parent != null) { this.parent.Remove(this); }
			 parent = value;
		  }
	   }

	   /// <summary>
	   /// Gets or Sets the type of this category item.
	   /// </summary>
	   public CategoryItemType Type {
		  get { return type; }
		  set {
			 this.pattern = string.Empty;
			 type = value; 
		  }
	   }

	   /// <summary>
	   /// Get or sets the value of this category item.
	   /// </summary>
	   public string Value {
		  get { return this.value; }
		  set {
			 this.pattern = string.Empty;
			 this.value = value; 
		  }
	   }

	   /// <summary>
	   /// Gets the pattern of this item. When performing Regex match, use this property.
	   /// </summary>
	   public string Pattern {
		  get {
			 if (this.type == CategoryItemType.Regex) {
				// Regex values are parsed as-is
				return this.value;
			 } else {
				if (string.IsNullOrEmpty(this.pattern)) {
				    switch (this.type) {
					   case CategoryItemType.WildCard:
						  // Escape wild card
						  this.pattern = ".*" + Regex.Escape(this.value) + ".*";
						  break;
					   case CategoryItemType.String:
						  this.pattern = "^" + Regex.Escape(this.value) + "$";
						  break;
					   case CategoryItemType.Regex:
						  // Escape anything else by default
						  this.pattern = this.value;
						  break;
					   default:
						  this.pattern = Regex.Escape(this.value);
						  break;
				    }
				}
				return this.pattern;
			 }
		  }
	   }

	   public static CategoryItem FromFormat(string format) {
		  CategoryItem item = new CategoryItem();

		  if (string.IsNullOrEmpty(format)) {
			 item.value = "";
			 item.type = CategoryItemType.String;
		  } else {
			 char firstChar = format[0];
			 switch (firstChar) {
				case '*':
				    item.type = CategoryItemType.WildCard;
				    item.value = format.Substring(1);
				    break;
				case '@':
				    item.type = CategoryItemType.Regex;
				    item.value = format.Substring(1);
				    break;
				default:
				    item.value = format;
				    item.type = CategoryItemType.String;
				    break;
			 }
		  }
		  return item;
	   }

	   public string ToFormat() {
		  switch (this.type) {
			 case CategoryItemType.WildCard:
				return "*" + this.value;
			 case CategoryItemType.Regex:
				return "@" + this.value;
			 default:
				return this.Value;
		  }
	   }

	   public override string ToString() {
		  return string.Format("{{ {0}, {1} }}", this.type, this.value);
	   }

	   #region IComparable<CategoryItem> Members

	   // Compared by: type, value
	   public int CompareTo(CategoryItem other) {
		  int result = 0;
		  result = this.type.CompareTo(other.type);
		  if (result == 0) { result = string.Compare(this.value, other.value, Utility.IGNORE_CASE); }
		  if (result == 0) { if ((this.parent != null) && (other.parent != null)) {  result = this.parent.CompareTo(other.parent); }}
		  return result;
	   }

	   #endregion

	   #region IEquatable<CategoryItem> Members

	   public bool Equals(CategoryItem other) {
		  return (this.CompareTo(other) == 0);
	   }

	   #endregion
    }
}
