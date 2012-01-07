/*************************************************************************
 *  SMOz (Start Menu Organizer)
 *  Copyleft (C) 2006 Nithin Philips
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
 *  Author            :  Nithin Philips <nithin@nithinphilips.com>
 *  Original FileName :  IgnoreList.cs
 *  Created           :  Fri Apr 21 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using SMOz.Template;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace SMOz.Utilities
{
    [Serializable]
    public sealed class IgnoreList : Category, ISerializable
    {
	   private IgnoreList(){
		  this.items = new List<CategoryItem>();
	   }

	   public static IgnoreList Instance {
		  get {
			 return IgnoreListSerializationProxy.sharedOnly; }
	   }

	   [Serializable]
	   [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	   [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)] 
	   private sealed class IgnoreListSerializationProxy : IObjectReference{

		  internal static readonly IgnoreList sharedOnly = new IgnoreList();
		  object IObjectReference.GetRealObject(StreamingContext context) {
			 // When deserializing this object, return a reference to
			 // Foo's singleton object instead.
			 Console.WriteLine("Getting Real Object");
			 return sharedOnly;
		  }
	   }

	   // A method called when serializing a Singleton.
	   [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
	   void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {
		  // Instead of serializing this object, 
		  // serialize a IgnoreListSerializationProxy instead.
		  info.SetType(typeof(IgnoreList.IgnoreListSerializationProxy));
		  // No other values need to be added.
	   }

	   internal void From(Category category) {
		  this.name = category.Name;
		  this.RestrictedPath = category.RestrictedPath;
		  this.items.AddRange(category.Items);
	   }
    }
}
