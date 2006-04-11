using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using SMOz.Utilities;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace SMOz.Cleanup
{
    [Serializable]
    public class ApplicationAssociationList : SortedDictionary<string, string> {

	   public void Save(string fileName) {
		  using (Stream stream = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.None)) {
			 IFormatter formatter = new BinaryFormatter();
			 formatter.Serialize(stream, this);
		  }
	   }

	   public static ApplicationAssociationList Load(string fileName) {
		  using (Stream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None)) {
			 IFormatter formatter = new BinaryFormatter();
			 return (ApplicationAssociationList)formatter.Deserialize(stream);
		  }
	   }

    }

    public class InstalledProgramList{

	   const string APP_LIST_ROOT = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

	   private bool IsKnownProgram(string name) {
		  return true;
	   }

	   public static string[] RetrieveProgramList() {
		  List<string> result = new List<string>();
		  using (RegistryKey rootKey = Registry.LocalMachine.OpenSubKey(APP_LIST_ROOT)) {
			 foreach (string subKey in rootKey.GetSubKeyNames()) {
				using (RegistryKey programKey = rootKey.OpenSubKey(subKey, false)) {
				    string programValue = programKey.GetValue("DisplayName", string.Empty) as string;
				    if (programValue.Trim() != string.Empty) {
					   result.Add(programValue);
				    }
				}
			 }
		  }
		  return result.ToArray();
	   }

/*
	   public static Dictionary<string, string> RetrieveProgramList() {
		  Dictionary<string, string> result = new Dictionary<string, string>();
		  using (RegistryKey rootKey = Registry.LocalMachine.OpenSubKey(APP_LIST_ROOT)) {
			 foreach (string subKey in rootKey.GetSubKeyNames()) {
				using (RegistryKey programKey = rootKey.OpenSubKey(subKey, false)) {
				    string programValue = programKey.GetValue("DisplayName", string.Empty) as string;
				    if (programValue.Trim() != string.Empty) {
					   if (!result.ContainsKey(programValue)) {
						  result.Add(programValue, subKey);
					   }
				    }
				}
			 }
		  }
		  return result;
	   }
*/
    }
}
