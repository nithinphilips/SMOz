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
 *  Original FileName :  Category.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace LibSmoz.Transformation
{
    /// <summary>
    /// Represents a template category.
    /// </summary>
    [Serializable]
    public class Category : List<CategoryItem>, IComparable<Category>
    {
        public const bool IgnoreCase = true;
        public const RegexOptions DefaultRegexOptions = RegexOptions.IgnoreCase | RegexOptions.Singleline;
        public const string RestCatSelector = "->";

        public Category()
        {
        }


        public Category(string name, string restrictedPath)
            : this()
        {
            this.Name = name;
            this.RestrictedPath = restrictedPath;
        }

        public string Name { get; set; }
        public string RestrictedPath { get; set; }

        public bool IsRestricted
        {
            get { return !string.IsNullOrEmpty(RestrictedPath); }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(TemplateParser.CategoryToFormat(this));
            foreach (var item in this)
            {
                sb.AppendLine(" " + item);
            }
            return sb.ToString();
        }
        
        #region IComparable<Category> Members

        // Compared by: name, restrictedPath
        public int CompareTo(Category other)
        {
            int result = 0;
            result = string.Compare(this.Name, other.Name, IgnoreCase);
            if (result == 0) { result = string.Compare(this.RestrictedPath, other.RestrictedPath, IgnoreCase); }
            return result;
        }

        #endregion


    }
}
