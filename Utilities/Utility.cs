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

namespace SMOz.Utilities
{
    public sealed class Utility
    {
	   public static bool IGNORE_CASE = true;

	   public static string USER_START_ROOT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs\\");
	   public static string LOCAL_START_ROOT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs\\").Replace(Environment.UserName, "All Users");

//	   public static string USER_START_ROOT = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\";
//	   public static string LOCAL_START_ROOT = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu).Replace(Environment.UserName, "All Users") + "\\";

	   public static string USER_TRASH_ROOT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SMOz\\Trash\\");
	   public static string LOCAL_TRASH_ROOT = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SMOz\\Trash\\");

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
    }

    public class IgnoreList : Category
    {
	   private IgnoreList() {
		  this.AddItem(new CategoryItem("desktop.ini", CategoryItemType.WildCard));
		  this.AddItem(new CategoryItem("Startup", CategoryItemType.String));
	   }

	   private static IgnoreList instance;

	   public static IgnoreList Instance {
		  get {
			 if (instance == null) {
				instance = new IgnoreList();
			 }
			 return instance;
		  }
	   }
    }
}
