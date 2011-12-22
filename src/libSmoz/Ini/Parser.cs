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
using System.IO;
using LibSmoz.Transformation;

namespace LibSmoz.Ini
{
    public class IniParser
    {
        public static Dictionary<string, HashSet<string>> Parse(string file)
        {
            return Parse(File.ReadAllLines(file, System.Text.Encoding.UTF8));
        }

        public static Dictionary<string, HashSet<string>> Parse(string[] lines)
        {
            SectionBuilder builder = new SectionBuilder();

            foreach (string t in lines)
            {
                string currentLine = t.Trim();

                if (string.IsNullOrEmpty(currentLine)) continue;  // ignore empty lines
                if (currentLine.StartsWith("#")) continue;	   // ignore comments

                if (currentLine.StartsWith("[") && currentLine.EndsWith("]"))
                {
                    builder.BeginSection(currentLine.Substring(1, currentLine.Length - 2));
                }
                else
                {
                    builder.Add(t);
                }
            }

            return builder.sections;
        }
        protected class SectionBuilder
        {
            public Dictionary<string, HashSet<string>> sections = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
            public bool IgnoreCase = Category.IgnoreCase;

            private string _currentSectionName = "";

            public void BeginSection(string name)
            {
                _currentSectionName = name;

                if(!sections.ContainsKey(name))
                    sections.Add(_currentSectionName, new HashSet<string>());
            }

            public void Add(string value)
            {
                sections[_currentSectionName].Add(value);
            }
        }
    }
}
