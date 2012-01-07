using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using SMOz.Utilities;

namespace SMOz.User
{
    [Serializable]
    public sealed class Settings
    {
	   static object objectLock = new object();

	   public static void Save() {
		  Utility.Serialize<ApplicationSettings>(Settings.Instance, Utility.DEFAULT_SETTINGS_FILE);
	   }

	   static ApplicationSettings appSettings;

	   public static ApplicationSettings Instance {
		  get {
			 lock (objectLock) {
				if (appSettings == null) {
				    if (File.Exists(Utility.DEFAULT_SETTINGS_FILE)) {
					   appSettings = Utility.DeSerialize<ApplicationSettings>(Utility.DEFAULT_SETTINGS_FILE);
				    } else {
					   appSettings = new ApplicationSettings();
				    }
				}
			 }
			 return appSettings;
		  }
	   }

	   // Note: Properties of this class are NOT multi-thread safe
	   public class ApplicationSettings
	   {
		  internal ApplicationSettings() { }

		  string[] additionalPaths = new string[] { };
		  bool scanLocalPath = true;
		  bool scanUserPath = true;

		  public string[] AdditionalPaths {
			 get { return additionalPaths; }
			 set { additionalPaths = value; }
		  }

		  public bool ScanLocalPath {
			 get { return this.scanLocalPath; }
			 set { this.scanLocalPath = value; }
		  }

		  public bool ScanUserPath {
			 get { return this.scanUserPath; }
			 set { this.scanUserPath = value; }
		  }
	   }
    }
}
