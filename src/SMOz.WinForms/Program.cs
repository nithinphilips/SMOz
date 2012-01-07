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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Diagnostics;
using SMOz.Resources.Localization;
using SMOz.StartMenu;

namespace SMOz
{
    static class Program
    {
	   /// <summary>
	   /// The main entry point for the Application.
	   /// </summary>
	   [STAThread]
	   static void Main() {

#if RELEASE
		  try {
#endif
//		  System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ml-IN");
//		  System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ml-IN");

			 // The application should be run in full trust mode; but just in case!
			 System.Security.Permissions.EnvironmentPermission envPermission = new System.Security.Permissions.EnvironmentPermission(System.Security.Permissions.PermissionState.Unrestricted);
			 envPermission.Demand();

			 LoadRuntimeData();
			 Application.EnableVisualStyles();
			 Application.SetCompatibleTextRenderingDefault(false);
			 Application.Run(new MainForm());
#if RELEASE
		  } catch (Exception ex) {
			 System.Security.Permissions.FileIOPermission ioperm = new System.Security.Permissions.FileIOPermission(System.Security.Permissions.FileIOPermissionAccess.AllAccess, System.Security.AccessControl.AccessControlActions.Change, Utility.DEBUG_FILE_PATH);
			 ioperm.Demand();
			 using (FileStream fs = new FileStream(Utility.DEBUG_FILE_PATH, FileMode.Append, FileAccess.Write, FileShare.None)) {
				string debugMessage = "-------------------------------------------" + Environment.NewLine;
				debugMessage += DateTime.UtcNow.ToLongDateString() + " " + DateTime.UtcNow.ToLongTimeString() + " UTC" + Environment.NewLine + Environment.NewLine;
				debugMessage += ex.Message + Environment.NewLine;
				debugMessage += ex.StackTrace + Environment.NewLine + Environment.NewLine;
				byte[] buffer = System.Text.UTF8Encoding.UTF8.GetBytes(debugMessage);
				fs.Write(buffer, 0, buffer.Length);
				fs.Flush();
			 }
			 MessageBox.Show("An unhandled error has occured in SMOz. SMOz cannot continue.\n\nMore information is available in the file '" + Utility.DEBUG_FILE_PATH + "'. Please include the contents of that file when requesting support. Sorry about the inconvenience.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		  }
#endif
	   }

	   public static void PersistRuntimeData() {
		  User.Settings.Save();
		  Template.TemplateHelper.Save(new Category[] { IgnoreList.Instance }, Utility.IGNORE_LIST_FILE_PATH);
		  Utility.Serialize<KnownCategories>(KnownCategories.Instance, Utility.KNOWN_CATEGORIES_FILE_PATH);
	   }

	   private static void LoadRuntimeData() {
		  if (File.Exists(Utility.KNOWN_CATEGORIES_FILE_PATH)) {
			 KnownCategories.Instance.From(Utility.DeSerialize<KnownCategories>(Utility.KNOWN_CATEGORIES_FILE_PATH));
		  }else{
			 Directory.CreateDirectory(Path.GetDirectoryName(Utility.KNOWN_CATEGORIES_FILE_PATH));
		  }

		  if (File.Exists(Utility.IGNORE_LIST_FILE_PATH)) {
			 Category[] ignore = Template.TemplateHelper.BuildCategories(Utility.IGNORE_LIST_FILE_PATH);
			 if ((ignore.Length != 1) || (ignore[0].Name != "Ignore List")) {
				MakeDefaultIgnoreList();
			 } else {
				IgnoreList.Instance.From(ignore[0]);
			 }
		  } else {
			 Directory.CreateDirectory(Path.GetDirectoryName(Utility.IGNORE_LIST_FILE_PATH));
			 MakeDefaultIgnoreList();
		  }
	   }

	   private static void MakeDefaultIgnoreList() {
		  IgnoreList.Instance.Name = "Ignore List";
		  IgnoreList.Instance.Add(new CategoryItem("desktop.ini", CategoryItemType.WildCard));
		  IgnoreList.Instance.Add(new CategoryItem("Startup", CategoryItemType.String));
	   }

	   public static string GetAboutVersion() {
		  return string.Format(Language.AboutVersionFormat, Application.ProductName, Application.ProductVersion);
	   }

	   public static string GetAboutContributors() {
		  return string.Format(Language.AboutContributorsFormat, Application.ProductName, Application.ProductVersion);
	   }

	   public static string GetLicenseInfo() {
		  return Properties.Resources.LicenseInfo;
	   }
    }
}