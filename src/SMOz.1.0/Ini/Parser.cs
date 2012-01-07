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
 *  Original FileName :  Parser.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SMOz.Ini
{
    public class IniParser
    {
	   public static IniSection[] Parse(string file) {
		  // http://www.devhood.com/tutorials/tutorial_details.aspx?tutorial_id=469
		  return Parse(File.ReadAllLines(file, System.Text.Encoding.UTF8));
	   }

	   public static IniSection[] Parse(string[] lines) {
		  SectionBuilder builder = new SectionBuilder();

		  for (int i = 0; i < lines.Length; i++) {
			 if(string.IsNullOrEmpty(lines[i])){ continue; } // ignore empty lines
			 string currentLine = lines[i].Trim();
			 if (currentLine.StartsWith("#")) {
				continue;	  // ignore comments
			 }else if (currentLine.StartsWith("[") && currentLine.EndsWith("]")) {
				builder.AddSection(currentLine.Substring(1, currentLine.Length - 2));
			 } else {
				builder.Add(lines[i]);
			 }
		  }
		  builder.Pop();

		  builder.sections.Sort();
		  return builder.sections.ToArray();
	   }


	   protected class SectionBuilder
	   {
		  public List<IniSection> sections = new List<IniSection>();
		  public bool IgnoreCase = Utilities.Utility.IGNORE_CASE;

		  IniSection currentSection;

		  public void AddSection(string name) {
			 if (currentSection != null) {
				Pop();
			 }

			 currentSection = Find(name);
			 if (currentSection == null) {
				currentSection = new IniSection(name);
			 }
		  }

		  // This can be better implemented by using a HashTable, but
		  // then case-insensitive comparison won't be possible.
		  public IniSection Find(string name) {
			 if ((sections == null) || (sections.Count <= 0)) { return null;  }
			 for (int i = 0; i < sections.Count; i++) {
				if (string.Compare(sections[i].Name, name, this.IgnoreCase) == 0) {
				    return sections[i];
				}
			 }
			 return null;
		  }

		  public void Add(string value) {
			 if (currentSection == null) {
				throw new Exception("Invalid Formatting. Orphan values.");
			 }
			 currentSection.Add(value);
		  }

		  public void Pop() {
			 if (!sections.Contains(currentSection)) {
				sections.Add(currentSection);
			 }
			 currentSection = null;
		  }
	   }
    }
}
