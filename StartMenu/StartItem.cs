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
 *  Original FileName :  StartItem.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using SMOz.Utilities;
using System.IO;
using System.ComponentModel;

namespace SMOz.StartMenu
{
    /// <summary>
    /// Specifies the type of a start item
    /// </summary>
    public enum StartItemType { 
	   /// <summary>
	   /// Item is a directory.
	   /// </summary>
	   Directory = 0, 
	   /// <summary>
	   /// Item is a file.
	   /// </summary>
	   File = 1 
    };
    
    /// <summary>
    /// Specifies the location of the start item
    /// </summary>
    [Flags] public enum StartItemLocation { 
	   /// <summary>
	   /// Item is not located on disk, or location is undiscernible.
	   /// </summary>
	   None = 0, 
	   /// <summary>
	   /// Item is located in the Local (All Users) directory.
	   /// </summary>
	   Local = 1, 
	   /// <summary>
	   /// Item is located in the User's home directory.
	   /// </summary>
	   User = 2, 
	   /// <summary>
	   /// Item is located in both Local and User's directories.
	   /// </summary>
	   All = Local | User 
    };

    /// <summary>
    /// A file or directory located at two different places to once!
    /// </summary>
    [Serializable]
    public class StartItem : IComparable<StartItem>
    {
	   public StartItem()
		  :this(string.Empty, StartItemType.File, "") 
	   { }

	   /// <summary>
	   /// Create a new instance of StartItem
	   /// </summary>
	   /// <param name="name">The name of the item. Name is relative to the root path.</param>
	   /// <param name="type">Type of start item.</param>
	   /// <param name="category">The category this item belongs to. An empty string represents a uncategorized item.</param>
	   public StartItem(string name, StartItemType type, string category) {
		  this.name = name;
		  this.realName = name;
		  this.type = type;
		  this.category = category;
	   }

	   string name;
	   string realName;
	   StartItemType type;
	   string category;

	   /// <summary>
	   /// Name of the object. Relative to the root folder.
	   /// </summary>
	   public string Name {
		  get { return name; }
		  set { name = value; }
	   }

	   /// <summary>
	   /// Real name of the object that exists on disk. Relative to the root folder.
	   /// </summary>
	   public string RealName {
		  get { return realName; }
		  set { 
			 this.realName = value;
			 InvalidateDynamicData();
		  } 
	   }

	   public StartItemType Type {
		  get { return type; }
		  set { type = value; }
	   }

	   public string Category {
		  get { return category; }
		  set { category = value; }
	   }

	   private void InvalidateDynamicData() {
		  this.localPath = string.Empty;
		  this.userPath = string.Empty;
		  this.location = StartItemLocation.None;
	   }

	   string localPath = string.Empty;
	   string userPath = string.Empty;
	   StartItemLocation location = StartItemLocation.None;

	   /// <summary>
	   /// Absolute location of the item in the local folder. The path referred may not exist.
	   /// </summary>
	   public string LocalPath {
		  get {
			 if (string.IsNullOrEmpty(localPath)) {
				localPath = Path.Combine(Utility.LOCAL_START_ROOT, this.realName);
			 }
			 return localPath;
		  }
	   }

	   /// <summary>
	   /// Absolute location of the item in the user's folder. The path referred may not exist.
	   /// </summary>
	   public string UserPath {
		  get {
			 if (string.IsNullOrEmpty(userPath)) {
				userPath = Path.Combine(Utility.USER_START_ROOT, this.realName);
			 }
			 return userPath;
		  }
	   }

	   /// <summary>
	   /// The location of the item.
	   /// </summary>
	   public StartItemLocation Location {
		  get {
			 if (location == StartItemLocation.None) {
				StartItemLocation _newLocation = StartItemLocation.None;
				if (this.Type == StartItemType.File) {
				    if (File.Exists(this.UserPath)) { _newLocation |= StartItemLocation.User; }
				    if (File.Exists(this.LocalPath)) { _newLocation |= StartItemLocation.Local; }
				} else {
				    if (Directory.Exists(this.UserPath)) { _newLocation |= StartItemLocation.User; }
				    if (Directory.Exists(this.LocalPath)) { _newLocation |= StartItemLocation.Local; }
				}
				location = _newLocation;
			 }
			 return location;
		  }
	   }

	   public bool HasLocal {
		  get {
			 return ((Location & StartItemLocation.Local) == StartItemLocation.Local);
		  }
	   }

	   public bool HasUser {
		  get {
			 return ((Location & StartItemLocation.User) == StartItemLocation.User);
		  }
	   }

	   public override string ToString() {
		  return this.name;
	   }

	   #region IComparable<StartItem> Members

	   public int CompareTo(StartItem other) {
		  // Comparison Order: Type
		  int result = 0;
		  result = this.type.CompareTo(other.type);
		  if (result != 0) { return result; }
		  result = this.category.CompareTo(other.category);
		  if (result != 0) { return result; }
		  result = this.name.CompareTo(other.name);
		  return result;
	   }

	   #endregion
    }
}
