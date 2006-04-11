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
 *  Original FileName :  StartManager.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using SMOz.Utilities;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using SMOz.Cleanup;

namespace SMOz.StartMenu
{
    //TODO: Move SmozDataManager methods to here
    [Serializable]
    public class StartManager : IEnumerable<StartItem>
    {
	   public StartManager() {
		  startItems = new List<StartItem>();
		  startItemCache = new List<string>();
	   }

	   List<StartItem> startItems;
	   List<string> startItemCache;

	   public StartItem[] StartItems {
		  get {
			 return startItems.ToArray();
		  }
	   }

	   /*
	    *  Note: These operations are inherently case insensitive.
	    */

	   public bool AddItem(string relativePath, StartItemType type, string category) {
		  return AddItem(new StartItem(relativePath, type, category));
	   }

	   public bool AddItem(string relativePath, StartItemType type) {
		  return AddItem(new StartItem(relativePath, type, ""));
	   }

	   public void Add(StartItem item) {
		  AddItem(item);
	   }

	   public bool AddItem(StartItem item) {
		  string _name = item.Name.ToLower();
		  if (startItemCache.Contains(_name)) {
			 return false;
		  } else {
			 startItems.Add(item);
			 startItemCache.Add(_name);
			 return true;
		  }
	   }

	   public void RemoveItem(StartItem item) {
		  startItems.Remove(item);
		  startItemCache.Remove(item.Name.ToLower()); // Important
	   }

	   public void RemoveFirstItem(string name) {
		  foreach (StartItem item in GetByName(name)) {
			 startItems.Remove(item);
			 startItemCache.Remove(item.Name.ToLower());
			 break;
		  }
	   }

	   public void RemoveAllItems(string name) {
		  foreach (StartItem item in GetByName(name)) {
			 startItems.Remove(item);
			 startItemCache.Remove(item.Name.ToLower());
		  }
	   }

	   public void SaveAssociationList(string fileName) {
		  ApplicationAssociationList assocList = new ApplicationAssociationList();
		  foreach(StartItem item in this.startItems){
			 if (!string.IsNullOrEmpty(item.Application)) {
				assocList.Add(item.RealName, item.Application);
			 }
		  }
		  assocList.Save(fileName);
	   }

	   public void LoadAssociationList(string fileName) {
		  if (!File.Exists(fileName)) {
			 Directory.CreateDirectory(Path.GetDirectoryName(fileName));
			 return;
		  }
		  ApplicationAssociationList assocList = ApplicationAssociationList.Load(fileName);
		  string value;
		  foreach (StartItem item in this.startItems) {
			 if (assocList.TryGetValue(item.RealName, out value)) {
				item.Application = value;
			 }
		  }
	   }

	   /// <summary>
	   /// Searches the start items' categories and returns ones that matches the string.
	   /// </summary>
	   /// <param name="category">The category to search for.</param>
	   /// <returns>The result. An array of zero length is returned if no matches are found.</returns>
	   public StartItem[] GetByCategory(string category) {
		  List<StartItem> result = new List<StartItem>();

		  foreach (StartItem startItem in this.startItems) {
			 if (string.Compare(startItem.Category, category, Utility.IGNORE_CASE) == 0) {
				result.Add(startItem);
			 }
		  }
		  result.Sort();
		  return result.ToArray();
	   }


	   /// <summary>
	   /// Searches the start items' names and returns ones that matches the string.
	   /// </summary>
	   /// <param name="pattern">A regex pattern to apply.</param>
	   /// <returns>The result. An array of zero length is returned if no matches are found.</returns>
	   public StartItem[] GetByName(string name) {
		  return GetByName(name, "", true);
	   }

	   /// <summary>
	   /// Searches the start items' names and returns ones that matches the wildcard pattern.
	   /// </summary>
	   /// <param name="pattern">A regex pattern to apply.</param>
	   /// <param name="category">A category to restict the search to.</param>
	   /// <returns>The result. An array of zero length is returned if no matches are found.</returns>
	   public StartItem[] GetByName(string name, string category) {
		  return GetByName(name, category, false);
	   }

	   private StartItem[] GetByName(string name, string category, bool allCategories) {
		  List<StartItem> result = new List<StartItem>();

		  if (allCategories) {
			 foreach (StartItem startItem in this.startItems) {
				if (string.Compare(startItem.Name, name, Utility.IGNORE_CASE) == 0) {
				    result.Add(startItem);
				}
			 }
		  } else {
			 foreach (StartItem startItem in GetByCategory(category)) {
				if (string.Compare(startItem.Name, name, Utility.IGNORE_CASE) == 0) {
				    result.Add(startItem);
				}
			 }
		  }
		  result.Sort();
		  return result.ToArray();
	   }

	   /// <summary>
	   /// Searches the start items' names and returns ones that matches the wildcard pattern.
	   /// </summary>
	   /// <param name="pattern">A regex pattern to apply.</param>
	   /// <returns>The result. An array of zero length is returned if no matches are found.</returns>
	   public StartItem[] GetByWildcard(string pattern) {
		  return GetByPattern(".*" + Regex.Escape(pattern) + ".*", "", true);
	   }

	   /// <summary>
	   /// Searches the start items' names and returns ones that matches the wildcard pattern.
	   /// </summary>
	   /// <param name="pattern">A regex pattern to apply.</param>
	   /// <param name="category">A category to restict the search to.</param>
	   /// <returns>The result. An array of zero length is returned if no matches are found.</returns>
	   public StartItem[] GetByWildcard(string pattern, string category) {
		  return GetByPattern(".*" + Regex.Escape(pattern) + ".*", category, false);
	   }

	   /// <summary>
	   /// Searches the start items' names and returns ones that matches the pattern.
	   /// </summary>
	   /// <param name="pattern">A regex pattern to apply.</param>
	   /// <returns>The result. An array of zero length is returned if no matches are found.</returns>
	   /// <exception cref="ArgumentException">Regular expression parsing error.</exception>
	   public StartItem[] GetByPattern(string pattern) {
		  return GetByPattern(pattern, "", true);
	   }

	   /// <summary>
	   /// Searches the start items' names and returns ones that matches the pattern.
	   /// </summary>
	   /// <param name="pattern">A regex pattern to apply.</param>
	   /// <param name="category">A category to restict the search to.</param>
	   /// <returns>The result. An array of zero length is returned if no matches are found.</returns>
	   /// <exception cref="ArgumentException">Regular expression parsing error.</exception>
	   public StartItem[] GetByPattern(string pattern, string category) {
		  return GetByPattern(pattern, category, false);
	   }

	   private StartItem[] GetByPattern(string pattern, string category, bool allCategories) {
		  List<StartItem> result = new List<StartItem>();

		  Regex regex = new Regex(pattern, Utility.REGEX_OPTIONS);

		  if (allCategories) {
			 foreach (StartItem startItem in this.startItems) {
				if (regex.IsMatch(Path.GetFileName(startItem.Name))) {
				    result.Add(startItem);
				}
			 }
		  } else {
			 foreach (StartItem startItem in GetByCategory(category)) {
				// search only name
				if (regex.IsMatch(Path.GetFileName(startItem.Name))) {
				    result.Add(startItem);
				}
			 }
		  }
		  result.Sort();
		  return result.ToArray();
	   }

	   #region IEnumerable<StartItem> Members

	   public IEnumerator<StartItem> GetEnumerator() {
		  return startItems.GetEnumerator();
	   }

	   #endregion

	   #region IEnumerable Members

	   System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
		  return startItems.GetEnumerator();
	   }

	   #endregion
    }
}
