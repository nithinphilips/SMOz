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
using System.Xml.Serialization;
using SMOz.User;

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
	   /// Item is located somewhere else in a user defined location.
	   /// </summary>
	   Other = 4,
	   /// <summary>
	   /// Item is located in both Local and User's directories.
	   /// </summary>
	   All = Local | User | Other,
    };

    /// <summary>
    /// A file or directory located at one or more places
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
	   string application;

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
	   }

	   public void SetRealName(string value) {
		 this.realName = value;
	   }

	   public StartItemType Type {
		  get { return type; }
		  set { type = value; }
	   }

	   public string Category {
		  get { return category; }
		  set { category = value; }
	   }

	   /// <summary>
	   /// Represents the application associated with this StartItem
	   /// </summary>
	   public string Application {
		  get { return application; }
		  set { application = value; }
	   }


	   /// <summary>
	   /// The location of the item.
	   /// </summary>
	   public StartItemLocation Location {
		  get {
			 string localPath = Path.Combine(Utility.LOCAL_START_ROOT, this.realName);
			 string userPath = Path.Combine(Utility.USER_START_ROOT, this.realName);
			 StartItemLocation location = StartItemLocation.None;

			 foreach (string validPath in GetValidPaths()) {
				if (validPath == localPath) {
				    location |= StartItemLocation.Local;
				} else if (validPath == userPath) {
				    location |= StartItemLocation.User;
				} else {
				    location |= StartItemLocation.Other;
				}
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

	   /// <summary>
	   /// Retrieves all locations of this item.
	   /// </summary>
	   /// <returns>An array of all valid paths.</returns>
	   public string[] GetValidPaths() {
		  List<string> validPaths = new List<string>();

		  string localFull = Path.Combine(Utility.LOCAL_START_ROOT, this.realName);
		  string userFull = Path.Combine(Utility.USER_START_ROOT, this.realName);

		  if (Settings.Instance.ScanLocalPath && Exists(localFull)) { validPaths.Add(localFull); }
		  if (Settings.Instance.ScanUserPath && Exists(userFull)) { validPaths.Add(userFull); }

		  foreach (string addt in Settings.Instance.AdditionalPaths) {
			 string addtFull = Path.Combine(addt, this.realName);
			 if (Exists(addtFull)) {
				validPaths.Add(addtFull);
			 }
		  }

		  return validPaths.ToArray();
	   }

	   public bool Exists(string path) {
		  if (this.Type == StartItemType.File) {
			 return File.Exists(path);
		  } else {
			 return Directory.Exists(path);
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
