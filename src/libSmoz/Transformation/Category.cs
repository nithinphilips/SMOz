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

        /// <summary>
        /// Creates a new instance of Category.
        /// </summary>
        public Category()
            :this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Creates a new instance of Category.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        public Category(string name)
            :this(name, string.Empty)
        {
        }
        
        /// <summary>
        /// Creates a new instance of Category.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <param name="restrictedPath">The path to which the category is restricted to.</param>
        public Category(string name, string restrictedPath)
            : base(EqualityComparers.CategoryItemComparer)
        {
            this.Name = name;
            this.RestrictedPath = restrictedPath;
        }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <remarks>
        /// <para>The name should be identical to the name of the directory associated with this category.</para>
        /// <para>
        /// For categories that are in subdirectories, the directory separator character (\) can be
        /// used to separate directories.
        /// </para>
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path to which this category is restricted to.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Normally when the RestrictedPath is empty, the selectors in the Category are applied to the
        /// root folder of the start menu. However, if the user wants to restrict the selectors to only 
        /// a subfolder within the start menu, this property can be set.
        /// </para>
        /// <para>
        /// For example, if the user wants to apply the selector *Halo to pick an item in the start menu
        /// directory "Games\FPS" and move it to "Video Games", then <see cref="Name"/> would be 
        /// set to "Video Games" and <see cref="RestrictedPath"/> would be set to "Games\FPS"
        /// </para>
        /// </remarks>
        public string RestrictedPath { get; set; }

        /// <summary>
        /// Gets whether this category applies to only a restricted category.
        /// </summary>
        public bool IsRestricted
        {
            get { return !string.IsNullOrEmpty(RestrictedPath); }
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
            if (other == null) return 1;

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
            if ((object)a == null && (object)b == null) return true;
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
