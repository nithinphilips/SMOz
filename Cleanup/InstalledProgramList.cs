using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using SMOz.Utilities;

namespace SMOz.Cleanup
{
    [Serializable]
    public class ApplicationAssociationList : SortedDictionary<string, string> {

	   public void Save(string fileName) {
		  Utility.Serialize<ApplicationAssociationList>(this, fileName);
	   }

	   public static ApplicationAssociationList Load(string fileName) {
		  return Utility.DeSerialize<ApplicationAssociationList>(fileName);
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
