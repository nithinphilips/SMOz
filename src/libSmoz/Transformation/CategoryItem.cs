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

    [Serializable]
    public class CategoryItem : IComparable<CategoryItem>, IEquatable<CategoryItem>
    {

        public CategoryItem() 
            : this(string.Empty, CategoryItemType.String) { }

        public CategoryItem(string value, CategoryItemType type)
        {
            this.Value = value;
            this.Type = type;
        }

        public string Value { get; set; }
        public CategoryItemType Type { get; set; }
        public Category Parent { get; set; }


        /// <summary>
        /// Gets the pattern of this item. When performing Regex match, use this value.
        /// </summary>
        public string GetPattern()
        {

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

        private Regex _regexObject;
        public Regex RegexObject
        {
            get { return _regexObject ?? (_regexObject = new Regex(this.GetPattern())); }
        }

        public override string ToString()
        {
            return TemplateParser.CategoryItemToFormat(this);
        }

        #region IComparable<CategoryItem> Members

        // Compared by: type, value
        public int CompareTo(CategoryItem other)
        {
            int result = 0;
            result = this.Type.CompareTo(other.Type);
            if (result == 0) { result = string.Compare(this.Value, other.Value, Category.IgnoreCase); }
            if (result == 0) { if ((this.Parent != null) && (other.Parent != null)) { result = this.Parent.CompareTo(other.Parent); } }
            return result;
        }

        #endregion

        #region IEquatable<CategoryItem> Members

        public bool Equals(CategoryItem other)
        {
            return (this.CompareTo(other) == 0);
        }

        #endregion
    }
}
