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
 *  Original FileName :  Utility.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SMOz.Template;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace SMOz.Utilities
{
    public sealed class Utility
    {
	   public const bool IGNORE_CASE = true;
	   public const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.Singleline;

	   public static readonly string KNOWN_CATEGORIES_FILE_PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\SMOz\\KnownCategories."
	   + Application.ProductVersion + ".xml";
	   public static readonly string IGNORE_LIST_FILE_PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\SMOz\\IgnoreList."
	   + Application.ProductVersion + ".xml";

	   public static readonly string USER_START_ROOT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs\\");
	   public static readonly string LOCAL_START_ROOT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs\\").Replace(Environment.UserName, "All Users");

	   public static readonly string USER_TRASH_ROOT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SMOz\\Trash\\");
	   public static readonly string LOCAL_TRASH_ROOT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SMOz\\Trash\\");

	   public static string[] PathToTree(string path) {
		  string[] parts = path.Split("\\".Split(), StringSplitOptions.None);
		  string[] tree = new string[parts.Length];
		  for (int j = 0; j < parts.Length; j++) {
			 for (int k = 0; k <= j; k++) {
				tree[j] += parts[k] + "\\";
			 }
			 tree[j] = tree[j].Substring(0, tree[j].Length - 1);
		  }
		  return tree;
	   }


	   public static bool IsValidPath(string path) {
		  foreach (char illegalChar in Path.GetInvalidPathChars()) {
			 if (path.IndexOf(illegalChar) >= 0) { return false; }
		  }
		  return true;
	   }

	   public static bool IsValidFileName(string path) {
		  foreach (char illegalChar in Path.GetInvalidFileNameChars()) {
			 if (path.IndexOf(illegalChar) >= 0) { return false; }
		  }
		  return true;
	   }

	   public static void Serialize<T>(T instance, string fileName) where T : class {
		  using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)) {
			 System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
			 xs.Serialize(fs, instance);
			 fs.Flush();
		  }
	   }

	   public static T DeSerialize<T>(string fileName) where T : class {
		  object up;
		  using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)) {
			 System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
			 up = xs.Deserialize(fs);
		  }
		  return up as T;
	   }
    }

    public class IgnoreList : Category, ISerializable
    {
	   private IgnoreList() {}

	   public void From(IgnoreList from) {
		  this.items = from.items;
		  this.name = from.name;
		  this.restrictedPath = from.restrictedPath;
	   }

	   public static IgnoreList Instance {
		  get { return SerializationProxy.sharedOnly; }
	   }

	   [Serializable]
	   private class SerializationProxy : IObjectReference
	   {
		  internal static readonly IgnoreList sharedOnly = new IgnoreList();
		  object IObjectReference.GetRealObject(StreamingContext context) {
			 // When deserializing this object, return a reference to
			 // Foo's singleton object instead.
			 return sharedOnly;
		  }
	   }

	   [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	   void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {
		  info.SetType(typeof(IgnoreList.SerializationProxy));
	   }
    }
}
