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

namespace SMOz.Template
{
    public enum CategoryItemType { String, WildCard, Regex };


    //TODO: Can everything be regex
    public class CategoryItem{

	   public CategoryItem() { }

	   public CategoryItem(string value, CategoryItemType type) {
		  this.value = value;
		  this.type = type;
	   }
	   
	   string value;
	   CategoryItemType type = CategoryItemType.String;

	   public CategoryItemType Type {
		  get { return type; }
		  set { type = value; }
	   }

	   public string Value {
		  get { return this.value; }
		  set { this.value = value; }
	   }

	   public CategoryItem FromFormat(string format) {
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
		  }
		  return this.Value;
	   }

	   [Obsolete]
	   public string FormattedValue {
		  get {
			 switch (this.type) {
				case CategoryItemType.WildCard:
				    return "*" + this.value;
				case CategoryItemType.Regex:
				    return "@" + this.value;
			 }
			 return this.Value;
		  }
		  set {
			 if (string.IsNullOrEmpty(value)) {
				this.value = "";
				this.type = CategoryItemType.String;
			 } else {
				char firstChar = value[0];
				switch (firstChar) {
				    case '*':
					   this.type = CategoryItemType.WildCard;
					   this.value = value.Substring(1);
					   break;
				    case '@':
					   this.type = CategoryItemType.Regex;
					   this.value = value.Substring(1);
					   break;
				    default:
					   this.value = value;
					   this.type = CategoryItemType.String;
					   break;
				}
			 }
		  }
	   }
    }
}
