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
 *  Original FileName :  IniSection.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace SMOz.Ini
{
    /// <summary>
    /// Represents a section in an Ini file.
    /// </summary>
    public class IniSection : List<string>, IComparable<IniSection>
    {
	   public IniSection() : base() { }
	   public IniSection(string name) : base() { this.name = name; }
	   public IniSection(int capacity) : base(capacity) { }
	   public IniSection(int capacity, string name) : base(capacity) { this.name = name; }
	   public IniSection(IEnumerable<string> collection) : base(collection) { }
	   public IniSection(IEnumerable<string> collection, string name) : base(collection) { this.name = name; }


	   string name;

	   public string Name {
		  get { return name; }
		  set { name = value; }
	   }

	   #region IComparable<IniSection> Members

	   public int CompareTo(IniSection other) {
		  return this.name.CompareTo(other.name);
	   }

	   #endregion
    }
}
