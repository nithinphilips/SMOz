using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Smoz.Cli
{
    public class Win32
    {
	   public enum CSIDL
	   {
		  CSIDL_DESKTOP = 0x0000,			  // <desktop>
		  CSIDL_INTERNET = 0x0001,			  // Internet Explorer (icon on desktop)
		  CSIDL_PROGRAMS = 0x0002,			  // Start Menu\Programs
		  CSIDL_CONTROLS = 0x0003,			  // My Computer\Control Panel
		  CSIDL_PRINTERS = 0x0004,			  // My Computer\Printers
		  CSIDL_PERSONAL = 0x0005,			  // My Documents
		  CSIDL_FAVORITES = 0x0006,			  // <user name>\Favorites
		  CSIDL_STARTUP = 0x0007,			  // Start Menu\Programs\Startup
		  CSIDL_RECENT = 0x0008,				  // <user name>\Recent
		  CSIDL_SENDTO = 0x0009,				  // <user name>\SendTo
		  CSIDL_BITBUCKET = 0x000a,			  // <desktop>\Recycle Bin
		  CSIDL_STARTMENU = 0x000b,			  // <user name>\Start Menu
		  CSIDL_MYDOCUMENTS = 0x000c,			  // logical "My Documents" desktop icon
		  CSIDL_MYMUSIC = 0x000d,			  // "My Music" folder
		  CSIDL_MYVIDEO = 0x000e,			  // "My Videos" folder
		  CSIDL_DESKTOPDIRECTORY = 0x0010,		  // <user name>\Desktop
		  CSIDL_DRIVES = 0x0011,				  // My Computer
		  CSIDL_NETWORK = 0x0012,			  // Network Neighborhood (My Network Places)
		  CSIDL_NETHOOD = 0x0013,			  // <user name>\nethood
		  CSIDL_FONTS = 0x0014,				  // windows\fonts
		  CSIDL_TEMPLATES = 0x0015,
		  CSIDL_COMMON_STARTMENU = 0x0016,		  // All Users\Start Menu
		  CSIDL_COMMON_PROGRAMS = 0X0017,		  // All Users\Start Menu\Programs
		  CSIDL_COMMON_STARTUP = 0x0018,		  // All Users\Startup
		  CSIDL_COMMON_DESKTOPDIRECTORY = 0x0019, // All Users\Desktop
		  CSIDL_APPDATA = 0x001a,			  // <user name>\Application Data
		  CSIDL_PRINTHOOD = 0x001b,			  // <user name>\PrintHood

		  CSIDL_LOCAL_APPDATA = 0x001c,	       // <user name>\Local Settings\Applicaiton Data (non roaming)

		  CSIDL_ALTSTARTUP = 0x001d,			  // non localized startup
		  CSIDL_COMMON_ALTSTARTUP = 0x001e,       // non localized common startup
		  CSIDL_COMMON_FAVORITES = 0x001f,

		  CSIDL_INTERNET_CACHE = 0x0020,
		  CSIDL_COOKIES = 0x0021,
		  CSIDL_HISTORY = 0x0022,
		  CSIDL_COMMON_APPDATA = 0x0023,		  // All Users\Application Data
		  CSIDL_WINDOWS = 0x0024,			  // GetWindowsDirectory()
		  CSIDL_SYSTEM = 0x0025,			       // GetSystemDirectory()
		  CSIDL_PROGRAM_FILES = 0x0026,	       // C:\Program Files
		  CSIDL_MYPICTURES = 0x0027,			  // C:\Program Files\My Pictures

		  CSIDL_PROFILE = 0x0028,			  // USERPROFILE
		  CSIDL_SYSTEMX86 = 0x0029,			  // x86 system directory on RISC
		  CSIDL_PROGRAM_FILESX86 = 0x002a,		  // x86 C:\Program Files on RISC

		  CSIDL_PROGRAM_FILES_COMMON = 0x002b,    // C:\Program Files\Common

		  CSIDL_PROGRAM_FILES_COMMONX86 = 0x002c, // x86 Program Files\Common on RISC
		  CSIDL_COMMON_TEMPLATES = 0x002d,        // All Users\Templates

		  CSIDL_COMMON_DOCUMENTS = 0x002e,        // All Users\Documents
		  CSIDL_COMMON_ADMINTOOLS = 0x002f,       // All Users\Start Menu\Programs\Administrative Tools
		  CSIDL_ADMINTOOLS = 0x0030,              // <user name>\Start Menu\Programs\Administrative Tools

		  CSIDL_CONNECTIONS = 0x0031,             // Network and Dial-up Connections
		  CSIDL_COMMON_MUSIC = 0x0035,            // All Users\My Music
		  CSIDL_COMMON_PICTURES = 0x0036,         // All Users\My Pictures
		  CSIDL_COMMON_VIDEO = 0x0037,            // All Users\My Video

		  CSIDL_CDBURN_AREA = 0x003b              // USERPROFILE\Local Settings\Application Data\Microsoft\CD Burning
	   }

	   [DllImport("shell32.dll")]
	   static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out]StringBuilder lpszPath, int nFolder, bool fCreate);

	   public static string GetFolderPath(CSIDL specialFolder) {
		  StringBuilder resultPath = new StringBuilder(255);
		  SHGetSpecialFolderPath((IntPtr)0, resultPath, (int)specialFolder, true);
		  return resultPath.ToString();
	   }
    }
}
