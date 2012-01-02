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
 *  Original FileName :  CategoryItem.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Text.RegularExpressions;

namespace LibSmoz.Transformation
{

    /// <summary>
    /// Represents an item in a category that belongs to a template.
    /// </summary>
    [Serializable]
    public class CategoryItem : IComparable<CategoryItem>, IEquatable<CategoryItem>
    {

        internal CategoryItem()
            : this(string.Empty, CategoryItemType.Regex) { }

        /// <summary>
        /// Creates a new instance of a CategoryItem.
        /// </summary>
        /// <param name="value">The value of the item.</param>
        /// <param name="type">The type of the item.</param>
        public CategoryItem(string value, CategoryItemType type)
        {
            this.Value = value;
            this.Type = type;
        }


        private string _pattern;
        private Regex _regexObject;

        /// <summary>
        /// Gets a valid Regex patten that represents this object.
        /// </summary>
        public string Pattern { get { return _pattern ?? (_pattern = GetPattern()); } }

        private string _value;

        /// <summary>
        /// Gets the value of this item as set by the user.
        /// </summary>
        public string Value
        {
            get { return _value; }
            set
            {
                if (value == _value) return;
                _value = value;
                _regexObject = null;
                _pattern = null;
            }
        }

        private CategoryItemType _type;

        /// <summary>
        /// Gets the type of this CategoryItem.
        /// </summary>
        public CategoryItemType Type
        {
            get { return _type; }
            set
            {
                if (value == _type) return;
                _type = value;
                _regexObject = null;
                _pattern = null;
            }
        }

        /// <summary>
        /// Gets the pattern of this item. When performing Regex match, use this value.
        /// </summary>
        private string GetPattern()
        {
            if (string.IsNullOrEmpty(Value)) return string.Empty;

            switch (Type)
            {
                case CategoryItemType.String:
                    return "^" + Regex.Escape(this.Value) + "$";
                case CategoryItemType.WildCard:
                    return ".*" + Regex.Escape(this.Value) + ".*";
                case CategoryItemType.Regex:
                    return this.Value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        /// <summary>
        /// Returns a Regex object that can be used to compare real start menu items to this CategoryItem.
        /// </summary>
        public Regex RegexObject
        {
            get { return _regexObject ?? (_regexObject = new Regex(this.GetPattern())); }
        }


        #region Overrides

        public override string ToString()
        {
            return Pattern;
        }

        public int CompareTo(CategoryItem other)
        {
            if (other == null) return -1;
            return this.Pattern.CompareTo(other.Pattern);
        }

        public bool Equals(CategoryItem other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if(obj == null) return false;
           
            var cObj = obj as CategoryItem;

            return cObj == null || cObj == this;
        }

        public override int GetHashCode()
        {
            return this.Pattern.GetHashCode();
        }

        /// <summary>
        /// Returns the string represenation of this category item.
        /// The string representation is the same as CategoryItem.Pattern.
        /// </summary>
        /// <param name="x">The category item.</param>
        /// <returns>The string represenation.</returns>
        public static implicit operator string(CategoryItem x)
        {
            return x.Pattern;
        }

        /// <summary>
        /// Determines whether two category items are equal.
        /// Two CategoryItems are equal when they both generate the same regex pattern.
        /// </summary>
        /// <param name="a">The first item.</param>
        /// <param name="b">The second item.</param>
        /// <returns>Returns true, if a and be are equal. Otherwise, false.</returns>
        public static bool operator ==(CategoryItem a, CategoryItem b)
        {
            if ((object)a == null || (object)b == null) return false;
            return a.Pattern.Equals(b.Pattern, Common.DefaultStringComparison);
        }

        /// <summary>
        /// Determines whether two category items are not equal.
        /// Two CategoryItems are equal when they both generate the same regex pattern.
        /// </summary>
        /// <param name="a">The first item.</param>
        /// <param name="b">The second item.</param>
        /// <returns>Returns true, if a and be are not equal. Otherwise, false.</returns>
        public static bool operator !=(CategoryItem a, CategoryItem b)
        {
            return !(a == b);
        }

        #endregion
    }
}
