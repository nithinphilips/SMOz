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
	   static Utility() {
		  // Make sure that the paths ends with '\' or equivalent.
		  if (USER_START_ROOT[USER_START_ROOT.Length -1] != Path.DirectorySeparatorChar) {
			 USER_START_ROOT += Path.DirectorySeparatorChar;
		  }
		  if (LOCAL_START_ROOT[LOCAL_START_ROOT.Length - 1] != Path.DirectorySeparatorChar) {
			 LOCAL_START_ROOT += Path.DirectorySeparatorChar;
		  }
	   }

	   public const bool IGNORE_CASE = true;
	   public const RegexOptions REGEX_OPTIONS = RegexOptions.IgnoreCase | RegexOptions.Singleline;
	   
	   // See 'AssociationBuilder.cs' for details
	   public static readonly string ASSOCIATION_LIST_FILE_PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\SMOz\\Association."
		  + Application.ProductVersion + ".bin";
	   public static readonly string KNOWN_CATEGORIES_FILE_PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\SMOz\\KnownCategories."
		  + Application.ProductVersion + ".xml";
	   public static readonly string IGNORE_LIST_FILE_PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\SMOz\\IgnoreList."
		  + Application.ProductVersion + ".ini";

	   public static readonly string DEFAULT_SETTINGS_FILE = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\SMOz\\User." 
		  + Application.ProductVersion + ".settings";

	   public static readonly string USER_START_ROOT = Win32.GetFolderPath(Win32.CSIDL.CSIDL_PROGRAMS);
	   public static readonly string LOCAL_START_ROOT = Win32.GetFolderPath(Win32.CSIDL.CSIDL_COMMON_PROGRAMS);

	   public static readonly string USER_TRASH_ROOT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SMOz\\Trash\\");
	   public static readonly string LOCAL_TRASH_ROOT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SMOz\\Trash\\");

	   public static readonly string DEBUG_FILE_PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\SMOz\\Debug."
		  + Application.ProductVersion + ".txt";

	   /// <summary>
	   /// Splits a path to tree form. For example, C:\Folder1\Folder2, will be processed as {C:\, C:\Folder1\, C:\Folder1\Folder2}
	   /// </summary>
	   /// <param name="path"></param>
	   /// <returns></returns>
	   public static string[] PathToTree(string path) {
		  string[] parts = path.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.None);
		  string[] tree = new string[parts.Length];
		  for (int j = 0; j < parts.Length; j++) {
			 for (int k = 0; k <= j; k++) {
				tree[j] += parts[k] + Path.DirectorySeparatorChar;
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

	   /// <summary>
	   /// Retrives all root directories. Equal to Settings.Instance.AdditionalPaths + [User] + [Local]
	   /// </summary>
	   /// <returns></returns>
	   public static string[] GetAllRoots() {
		  List<string> src = new List<string>(SMOz.User.Settings.Instance.AdditionalPaths);
		  if (User.Settings.Instance.ScanLocalPath) { src.Add(Utility.LOCAL_START_ROOT); }
		  if (User.Settings.Instance.ScanUserPath) { src.Add(Utility.USER_START_ROOT); }
		  return src.ToArray();
	   }

	   public static void Serialize<T>(T instance, string fileName) where T : class {
		  string tempFile = Path.GetTempFileName();
		  using (System.IO.FileStream fs = new System.IO.FileStream(tempFile, System.IO.FileMode.Create, System.IO.FileAccess.Write)) {
			 System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
			 xs.Serialize(fs, instance);
			 fs.Flush();
		  }
		  File.Copy(tempFile, fileName, true);
		  File.Delete(tempFile);
	   }

	   public static T DeSerialize<T>(string fileName) where T : class {
		  object up;
		  using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read)) {
			 System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
			 up = xs.Deserialize(fs);
		  }
		  return up as T;
	   }

	   public enum MoveFileMode
	   {
		  Overwrite,
		  OverwriteIfNewer
	   }

	   // ...Because .NET is piece of .SHIT
	   // Watch for stack overflow, ye be warned
	   // Note: This method can be rewritten to 'merge' two directories together (like 'move' in explorer does)
	   public static void RecursiveMoveDirectory(string source, string target, MoveFileMode mode) {
		  Directory.CreateDirectory(target);

		  string[] subdirs = Directory.GetDirectories(source);

		  for (int i = 0; i < subdirs.Length; i++) {
			 RecursiveMoveDirectory(subdirs[i], Path.Combine(target, Path.GetFileName(subdirs[i])), mode);
		  }

		  string[] files = Directory.GetFiles(source);

		  for (int i = 0; i < files.Length; i++) {
			 string targetFile = Path.Combine(target, Path.GetFileName(files[i]));
			 MoveFile(files[i], targetFile, mode);
		  }

		  Directory.Delete(source, false);
		  
	   }

	   public static void MoveFile(string source, string target, MoveFileMode mode) {
		  if (File.Exists(target)){
			 switch (mode) {
				case MoveFileMode.Overwrite:
				    File.Delete(target);
				    File.Move(source, target);
				    break;
				case MoveFileMode.OverwriteIfNewer:
				    if (File.GetCreationTimeUtc(source) >= File.GetCreationTimeUtc(target)) {
					   File.Delete(target);
					   File.Move(source, target);
				    } else {
					   File.Delete(source);
				    }
				    break;
				default:
				    break;
			 }
		  } else {
			 File.Move(source, target);
		  }
	   }
    }
}
