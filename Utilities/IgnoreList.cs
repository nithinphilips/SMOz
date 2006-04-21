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
