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
using LibSmoz.Comparators;

namespace LibSmoz.Transformation
{
    /// <summary>
    /// Represents a template category.
    /// </summary>
    [Serializable]
    public class Category : HashSet<CategoryItem>, IComparable<Category>, IEquatable<Category>
    {

        public Category()
            :this(string.Empty, string.Empty)
        {
        }

        public Category(string name)
            :this(name, string.Empty)
        {
        }

        public Category(string name, string restrictedPath)
            : base(EqualityComparers.CategoryItemComparer)
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
        
        public void Merge(Category category)
        {
            foreach (var item in category)
                this.Add(item);
        }
  
        #region Overrides

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(TemplateParser.CategoryToFormat(this) + " (" + this.Count + ")");
            foreach (var item in this)
            {
                sb.AppendLine(" " + item);
            }
            return sb.ToString();
        }

        public int CompareTo(Category other)
        {
            if (other == null) return -1;

            int nameCmp = this.Name.CompareTo(other.Name);
            return nameCmp == 0 ? this.RestrictedPath.CompareTo(other.RestrictedPath) : nameCmp;
        }

        public bool Equals(Category other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            var cObj = obj as Category;
            return cObj == null || cObj == this;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() ^ this.RestrictedPath.GetHashCode();
        }

        /// <summary>
        /// Determines whether two categories  are equal.
        /// Two categories are equal when they both generate the same regex pattern.
        /// </summary>
        /// <param name="a">The first item.</param>
        /// <param name="b">The second item.</param>
        /// <returns>Returns true, if a and be are equal. Otherwise, false.</returns>
        public static bool operator ==(Category a, Category b)
        {
            if ((object)a == null || (object)b == null) return false;
            return a.Name.Equals(b.Name, Common.DefaultStringComparison) && a.RestrictedPath.Equals(b.RestrictedPath, Common.DefaultStringComparison);
        }

        /// <summary>
        /// Determines whether two categories are not equal.
        /// Two categories are equal when they both generate the same regex pattern.
        /// </summary>
        /// <param name="a">The first item.</param>
        /// <param name="b">The second item.</param>
        /// <returns>Returns true, if a and be are not equal. Otherwise, false.</returns>
        public static bool operator !=(Category a, Category b)
        {
            return !(a == b);
        }

        #endregion
        
    }
}
