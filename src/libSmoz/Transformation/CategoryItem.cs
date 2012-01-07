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

        /// <summary>
        /// Creates a new instance of a CategoryItem.
        /// </summary>
        public CategoryItem()
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
            if (other == null) return 1;
            return this.Pattern.CompareTo(other.Pattern);
        }

        public bool Equals(CategoryItem other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            var cObj = obj as CategoryItem;
            return cObj == null || cObj == this;
        }

        public override int GetHashCode()
        {
            return this.Pattern.GetHashCode();
        }

        /// <summary>
        /// Returns the string representation of a category item.
        /// </summary>
        /// <param name="x">The category item.</param>
        /// <returns>The string represenation.</returns>
        /// <seealso cref="op_Implicit(string)"/>
        public static implicit operator string(CategoryItem x)
        {
            if((object)x == null) return string.Empty;

            string prefix = "";
            switch (x.Type)
            {
                case CategoryItemType.WildCard:
                    prefix = "*";
                    break;
                case CategoryItemType.Regex:
                    prefix = "@";
                    break;
            }

            return prefix + x.Value;
        }

        /// <summary>
        /// Creates a CategoryItem from its string representation.
        /// </summary>
        /// <param name="str">
        /// <para>A string representation of a Category Item.</para>
        /// <para>The type of the string is denoted by its first chracter.</para>
        /// <list type="bullet">
        ///     <item>*: A wildcard item</item>
        ///     <item>@: A regular expression</item>
        ///     <item>All else: An exact string match</item>
        /// </list>
        /// </param>
        /// <example>
        /// <code>
        /// class Program
        /// {
        ///     
        ///     static void Main()
        ///     {
        ///         CategoryItem a = "*A WildCard";
        ///         // c.Value = "A WildCard";
        ///         // c.Type  = CategoryItemType.WildCard;
        /// 
        ///         CategoryItem b = "@A [Re]gex";
        ///         // c.Value = "A [Re]gex";
        ///         // c.Type  = CategoryItemType.Regex;
        /// 
        ///         CategoryItem c = "An exact match";
        ///         // c.Value = "An exact match";
        ///         // c.Type  = CategoryItemType.String;
        ///     }
        /// 
        /// }
        /// </code>
        /// </example>
        /// <returns>A category item.</returns>
        public static implicit operator CategoryItem(string str)
        {
            CategoryItem item = new CategoryItem();

            if (!string.IsNullOrEmpty(str))
            {
                char firstChar = str[0];
                switch (firstChar)
                {
                    case '*':
                        item.Type = CategoryItemType.WildCard;
                        item.Value = str.Substring(1);
                        break;
                    case '@':
                        item.Type = CategoryItemType.Regex;
                        item.Value = str.Substring(1);
                        break;
                    default:
                        item.Value = str;
                        item.Type = CategoryItemType.String;
                        break;
                }
            }

            return item;
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
            if ((object)a == null && (object)b == null) return true;
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
