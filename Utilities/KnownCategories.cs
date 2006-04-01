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
 *  Original FileName :  KnownCategories.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace SMOz.Utilities
{
    public class KnownCategories : IEnumerable<string>
    {
	   private KnownCategories(){
		  knownCategories = new Dictionary<string, string>();
	   }

	   private static KnownCategories instance;

	   public static KnownCategories Instance {
		  get {
			 if (instance == null) {
				instance = new KnownCategories();
			 }
			 return instance;
		  }
	   }

	   Dictionary<string, string> knownCategories;

	   public void AddCategories(string[] names) {
		  for (int i = 0; i < names.Length; i++) {
			 AddCategory(names[i]);
		  }
	   }

	   public bool AddCategory(string name) {
		  string _name = name.ToLower();
		  if (!knownCategories.ContainsKey(_name)) {
			 knownCategories.Add(_name, name);
			 return true;
		  } else {
			 return false;
		  }
	   }

	   public bool RemoveCategory(string name) {
		  string _name = name.ToLower();
		  return knownCategories.Remove(_name);
	   }

	   public bool IsCategory(string name) {
		  string _name = name.ToLower();
		  return knownCategories.ContainsKey(_name);
	   }

	   #region IEnumerable<string> Members

	   public IEnumerator<string> GetEnumerator() {
		  return knownCategories.Values.GetEnumerator();
	   }

	   #endregion

	   #region IEnumerable Members

	   System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
		  return knownCategories.Values.GetEnumerator();
	   }

	   #endregion

	   public string[] ToArray() {
		  string[] result = new string[knownCategories.Count];
		  int index = 0;
		  foreach (string item in knownCategories.Values) {
			 result[index] = item;
			 index++;
		  }
		  return result;
	   }
    }
}
