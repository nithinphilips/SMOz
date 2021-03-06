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
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace SMOz.Utilities
{
    [Serializable]
    public class KnownCategories : IEnumerable<string>, ISerializable
    {

	   public void From(KnownCategories from) {
		  this.knownCategories = from.knownCategories;
	   }

	   // change the default constructor to private
	   private KnownCategories() {
		  knownCategories = new SortedList<string, int>(StringComparer.CurrentCultureIgnoreCase);
	   }

	   public static KnownCategories Instance {
		  get { return SerializationProxy.sharedOnly; }
	   }

	   [Serializable]
	   private class SerializationProxy : IObjectReference
	   {
		  internal static readonly KnownCategories sharedOnly = new KnownCategories();
		  object IObjectReference.GetRealObject(StreamingContext context) {
			 // When deserializing this object, return a reference to
			 // Foo's singleton object instead.
			 return sharedOnly;
		  }
	   }

	   [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	   void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {
		  info.SetType(typeof(KnownCategories.SerializationProxy));
	   }

	   // Umm... the int still takes 4bytes! may be something smaller
	   SortedList<string, int> knownCategories;

	   public void AddRange(IEnumerable<string> names) {
	       foreach (var name in names)
	       {
	           Add(name);
	       }
	   }

	   public bool Add(string name) {
		  if (name == "") { return false; }
		  if (!knownCategories.ContainsKey(name)) {
			 knownCategories.Add(name, 0);
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
		  return knownCategories.Keys.GetEnumerator();
	   }

	   #endregion

	   #region IEnumerable Members

	   System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
		  return knownCategories.Values.GetEnumerator();
	   }

	   #endregion

	   public string[] ToArray() {
		  string[] result = new string[knownCategories.Keys.Count];
		  for (int i = 0; i < knownCategories.Keys.Count; i++){ result[i] = knownCategories.Keys[i]; }
		  return result;
	   }
    }
}
