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
using System.Linq;
using LibSmoz.Transformation;

namespace LibSmoz.Ini
{
    public class IniParser
    {
        public static Dictionary<string, HashSet<string>> Parse(string file)
        {
            return Parse(File.ReadAllLines(file, System.Text.Encoding.UTF8));
        }

        public static Dictionary<string, HashSet<string>> Parse(IEnumerable<string> lines)
        {
            SectionBuilder builder = new SectionBuilder();
            int lineNumber = 0;
            foreach (string t in lines)
            {
                lineNumber++;

                string currentLine = t.Trim();

                if (string.IsNullOrEmpty(currentLine)) continue;  // ignore empty lines
                if (currentLine.StartsWith("#")) continue;	   // ignore comments

                if (currentLine.StartsWith("[") && currentLine.EndsWith("]"))
                {
                    string sectionName = currentLine.Substring(1, currentLine.Length - 2);
                    ThrowIfContainsInvalidCharacters(sectionName, lineNumber);
                    builder.BeginSection(sectionName);
                }
                else
                {
                    builder.Add(currentLine, lineNumber);
                }
            }

            return builder.Sections;
        }

        private static void ThrowIfContainsInvalidCharacters(string str, int lineNumber)
        {
            str = str.Replace("->", "");
            int index = str.IndexOfAny(Path.GetInvalidPathChars());
            if (index >= 0) 
                throw new IniParseException("Section name contains an invalid character", lineNumber, index);
        }

        protected class SectionBuilder
        {
            public Dictionary<string, HashSet<string>> Sections = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);

            private string _currentSectionName = "";

            public void BeginSection(string name)
            {
                _currentSectionName = name;

                if(!Sections.ContainsKey(name))
                    Sections.Add(_currentSectionName, new HashSet<string>());
            }

            public void Add(string value, int lineNumber)
            {
                if(string.IsNullOrEmpty(_currentSectionName)) 
                    throw new IniParseException("Orphaned values found. All values must be within a section.", lineNumber);
                Sections[_currentSectionName].Add(value);
            }
        }
    }
}
