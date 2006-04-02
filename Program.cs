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
 *  Original FileName :  Program.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SMOz.UI;
using SMOz.Utilities;
using System.IO;
using SMOz.Template;

namespace SMOz
{
    static class Program
    {
	   /// <summary>
	   /// The main entry point for the application.
	   /// </summary>
	   [STAThread]
	   static void Main() {

		  LoadRuntimeData();

		  Application.EnableVisualStyles();
		  Application.SetCompatibleTextRenderingDefault(false);
		  Application.Run(new MainForm());
	   }

	   public static void PersistRuntimeData() {
		  Utility.Serialize<KnownCategories>(KnownCategories.Instance, Utility.KNOWN_CATEGORIES_FILE_PATH);
		  Utility.Serialize<IgnoreList>(IgnoreList.Instance, Utility.IGNORE_LIST_FILE_PATH);
	   }

	   private static void LoadRuntimeData() {
		  if (File.Exists(Utility.KNOWN_CATEGORIES_FILE_PATH)) {
			 KnownCategories.Instance.From(Utility.DeSerialize<KnownCategories>(Utility.KNOWN_CATEGORIES_FILE_PATH));
		  } else {
			 Directory.CreateDirectory(Path.GetDirectoryName(Utility.KNOWN_CATEGORIES_FILE_PATH));
		  }

		  if (File.Exists(Utility.IGNORE_LIST_FILE_PATH)) {
			 IgnoreList.Instance.From(Utility.DeSerialize<IgnoreList>(Utility.IGNORE_LIST_FILE_PATH));
		  } else {
			 Directory.CreateDirectory(Path.GetDirectoryName(Utility.IGNORE_LIST_FILE_PATH));
			 IgnoreList.Instance.Name = "Ignore List";
			 IgnoreList.Instance.Add(new CategoryItem("desktop.ini", CategoryItemType.WildCard));
			 IgnoreList.Instance.Add(new CategoryItem("Startup", CategoryItemType.String));
		  }
	   }

	   

    }
}