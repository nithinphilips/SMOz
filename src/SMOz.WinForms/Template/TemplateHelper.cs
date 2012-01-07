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
 *  Original FileName :  TemplateHelper.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using SMOz.Ini;
using SMOz.Utilities;
using System.IO;

namespace SMOz.Template
{
    public class TemplateHelper
    {
	   public static void Save(TemplateProvider template, string file) {
		  Save(template.Categories, file);
	   }

	   public static void Save(IEnumerable<Category> categories, string file) {
		  IniWriter writer = new IniWriter();
		  foreach (Category category in categories){
			 for (int i = 0; i < category.Count; i++){
				writer.AddValue(category[i].ToFormat(), category.ToFormat());
			 }
		  }
		  string tempFile = Path.GetTempFileName();
		  writer.Save(tempFile);
		  File.Copy(tempFile, file, true);
		  File.Delete(tempFile);
	   }


	   public static TemplateProvider Build(string file) {
		  return Build(IniParser.Parse(file));
	   }

	   public static TemplateProvider Build(IniSection[] sections) {
		  Category[] categories = BuildCategories(sections);
		  TemplateProvider template = new TemplateProvider(categories);
		  KnownCategories.Instance.AddRange(template.ToStringArray());
		  return template;
	   }

	   public static Category[] BuildCategories(string file) {
		  return BuildCategories(IniParser.Parse(file));
	   }

	   public static Category[] BuildCategories(IniSection[] sections) {
		  List<Category> categories = new List<Category>(sections.Length);
		  for (int i = 0; i < sections.Length; i++) {
			 Category category = Category.FromFormat(sections[i].Name, sections[i].Count);
			 for (int j = 0; j < sections[i].Count; j++) {
				category.Add(CategoryItem.FromFormat(sections[i][j]));
			 }
			 categories.Add(category);
		  }
		  return categories.ToArray();
	   }
    }
}
