using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibSmoz.Commands;
using LibSmoz.Commands.UI;
using LibSmoz.Model;
using LibSmoz.Transformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SMOz.Tests
{
    [TestClass]
    public class TransformationTest
    {
        private StartMenu _startMenu;
        private HashSet<string> _knownCategories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private Template t;

        [TestInitialize]
        public void Setup()
        {
            // Create a dummy start menu
            Directory.CreateDirectory(@"StartA\Accessories");
            Directory.CreateDirectory(@"StartA\Accessories\Accessibility");
            File.Create(@"StartA\Accessories\Accessibility\Ease of Access.lnk").Close();
            File.Create(@"StartA\Accessories\Accessibility\Magnify.lnk").Close();
            File.Create(@"StartA\Accessories\Accessibility\Narrator.lnk").Close();
            File.Create(@"StartA\Accessories\Accessibility\On-Screen Keyboard.lnk").Close();
            Directory.CreateDirectory(@"StartA\Accessories\System Tools");
            File.Create(@"StartA\Accessories\System Tools\computer.lnk").Close();
            File.Create(@"StartA\Accessories\System Tools\Control Panel.lnk").Close();
            File.Create(@"StartA\Accessories\System Tools\Internet Explorer (No Add-ons).lnk").Close();
            File.Create(@"StartA\Accessories\System Tools\Private Character Editor.lnk").Close();
            File.Create(@"StartA\Accessories\Command Prompt.lnk").Close();
            File.Create(@"StartA\Accessories\Notepad.lnk").Close();
            File.Create(@"StartA\Accessories\Run.lnk").Close();
            File.Create(@"StartA\Accessories\Windows Explorer.lnk").Close();
            Directory.CreateDirectory(@"StartA\Administrative Tools");
            Directory.CreateDirectory(@"StartA\Album Art Downloader");
            File.Create(@"StartA\Album Art Downloader\Album Art Downloader.lnk").Close();
            File.Create(@"StartA\Album Art Downloader\Uninstall.lnk").Close();
            Directory.CreateDirectory(@"StartA\AMP Font Viewer");
            Directory.CreateDirectory(@"StartA\Appetizer");
            File.Create(@"StartA\Appetizer\Appetizer.lnk").Close();
            File.Create(@"StartA\Appetizer\Uninstall.lnk").Close();
            Directory.CreateDirectory(@"StartA\AsfTools");
            Directory.CreateDirectory(@"StartA\AviSynth 2.5");
            Directory.CreateDirectory(@"StartA\Axialis Software");
            Directory.CreateDirectory(@"StartA\Banshee");
            File.Create(@"StartA\Banshee\Banshee.lnk").Close();
            Directory.CreateDirectory(@"StartA\Blumind");
            Directory.CreateDirectory(@"StartA\Cain");
            Directory.CreateDirectory(@"StartA\ClipX");
            Directory.CreateDirectory(@"StartA\Dropbox");
            File.Create(@"StartA\Dropbox\Dropbox.lnk").Close();
            File.Create(@"StartA\Dropbox\Uninstall.lnk").Close();
            Directory.CreateDirectory(@"StartA\DVD Decrypter");
            Directory.CreateDirectory(@"StartA\ElcomSoft");
            Directory.CreateDirectory(@"StartA\ElcomSoft\Advanced Archive Password Recovery");
            File.Create(@"StartA\ElcomSoft\Advanced Archive Password Recovery\Advanced Archive Password Recovery Help.lnk").Close();
            File.Create(@"StartA\ElcomSoft\Advanced Archive Password Recovery\Advanced Archive Password Recovery.lnk").Close();
            File.Create(@"StartA\ElcomSoft\Advanced Archive Password Recovery\End-User License Agreement.lnk").Close();
            File.Create(@"StartA\ElcomSoft\Advanced Archive Password Recovery\How to order.lnk").Close();
            File.Create(@"StartA\ElcomSoft\Advanced Archive Password Recovery\Readme.lnk").Close();
            File.Create(@"StartA\ElcomSoft\Advanced Archive Password Recovery\Uninstall ARCHPR.lnk").Close();
            Directory.CreateDirectory(@"StartA\Flashcard Master");
            Directory.CreateDirectory(@"StartA\Flux");
            File.Create(@"StartA\Flux\Flux.lnk").Close();
            File.Create(@"StartA\Flux\Uninstall.lnk").Close();
            Directory.CreateDirectory(@"StartA\full phat products");
            Directory.CreateDirectory(@"StartA\full phat products\Snarl");
            File.Create(@"StartA\full phat products\Snarl\Experimental Stuff.lnk").Close();
            File.Create(@"StartA\full phat products\Snarl\Extras.lnk").Close();
            File.Create(@"StartA\full phat products\Snarl\Samples.lnk").Close();
            File.Create(@"StartA\full phat products\Snarl\Snarl.lnk").Close();
            File.Create(@"StartA\full phat products\Snarl\Tools.lnk").Close();
            File.Create(@"StartA\full phat products\Snarl\Uninstall.lnk").Close();
            File.Create(@"StartA\full phat products\Snarl\User Guide.lnk").Close();
            Directory.CreateDirectory(@"StartA\Haali Media Splitter");
            Directory.CreateDirectory(@"StartA\Handbrake");
            Directory.CreateDirectory(@"StartA\HP Play");
            File.Create(@"StartA\HP Play\HP Play.lnk").Close();
            File.Create(@"StartA\HP Play\Uninstall HP Play Application.lnk").Close();
            Directory.CreateDirectory(@"StartA\Image Cropper");
            Directory.CreateDirectory(@"StartA\JabRef");
            File.Create(@"StartA\JabRef\JabRef 2.7.2.lnk").Close();
            File.Create(@"StartA\JabRef\Uninstall JabRef 2.7.2.lnk").Close();
            Directory.CreateDirectory(@"StartA\Maintenance");
            File.Create(@"StartA\Maintenance\Help.lnk").Close();
            Directory.CreateDirectory(@"StartA\MakeMKV");
            File.Create(@"StartA\MakeMKV\MakeMKV Website.lnk").Close();
            File.Create(@"StartA\MakeMKV\MakeMKV.lnk").Close();
            File.Create(@"StartA\MakeMKV\Uninstall.lnk").Close();
            Directory.CreateDirectory(@"StartA\MediaCoder x64");
            File.Create(@"StartA\MediaCoder x64\MediaCoder Dropbox.lnk").Close();
            File.Create(@"StartA\MediaCoder x64\MediaCoder x64 Web Site.lnk").Close();
            File.Create(@"StartA\MediaCoder x64\MediaCoder x64.lnk").Close();
            File.Create(@"StartA\MediaCoder x64\Uninstall MediaCoder x64.lnk").Close();
            Directory.CreateDirectory(@"StartA\MessagingToolkit");
            Directory.CreateDirectory(@"StartA\MessagingToolkit\MessagingToolkit-Barcode");
            File.Create(@"StartA\MessagingToolkit\MessagingToolkit-Barcode\Binaries.lnk").Close();
            File.Create(@"StartA\MessagingToolkit\MessagingToolkit-Barcode\Demo - Barcode.lnk").Close();
            File.Create(@"StartA\MessagingToolkit\MessagingToolkit-Barcode\Sample Code.lnk").Close();
            File.Create(@"StartA\MessagingToolkit\MessagingToolkit-Barcode\Uninstall.lnk").Close();
            Directory.CreateDirectory(@"StartA\Nithin Philips");
            Directory.CreateDirectory(@"StartA\Nithin Philips\Image Viewer");
            Directory.CreateDirectory(@"StartA\Notepad++");
            Directory.CreateDirectory(@"StartA\NuGet Package Explorer");
            Directory.CreateDirectory(@"StartA\NUnit 2.5.9");
            Directory.CreateDirectory(@"StartA\NUnit 2.5.9\Samples");
            File.Create(@"StartA\NUnit 2.5.9\Samples\C#.lnk").Close();
            File.Create(@"StartA\NUnit 2.5.9\Samples\C++.lnk").Close();
            File.Create(@"StartA\NUnit 2.5.9\Samples\Extensibility.lnk").Close();
            File.Create(@"StartA\NUnit 2.5.9\Samples\J#.lnk").Close();
            File.Create(@"StartA\NUnit 2.5.9\Samples\VB.lnk").Close();
            Directory.CreateDirectory(@"StartA\NUnit 2.5.9\Select Runtime");
            File.Create(@"StartA\NUnit 2.5.9\Select Runtime\NUnit (.NET 2.0).lnk").Close();
            File.Create(@"StartA\NUnit 2.5.9\Documentation.lnk").Close();
            File.Create(@"StartA\NUnit 2.5.9\NUnit.lnk").Close();
            Directory.CreateDirectory(@"StartA\Package Maker");
            File.Create(@"StartA\Package Maker\Package Maker.lnk").Close();
            File.Create(@"StartA\Package Maker\Uninstall.lnk").Close();
            File.Create(@"StartA\Package Maker\Visit Website.lnk").Close();
            Directory.CreateDirectory(@"StartA\QSynergy");
            Directory.CreateDirectory(@"StartA\Ruby 1.9.2-p290");
            Directory.CreateDirectory(@"StartA\Ruby 1.9.2-p290\Documentation");
            File.Create(@"StartA\Ruby 1.9.2-p290\Documentation\Ruby 1.9.2-p290 API Reference.lnk").Close();
            File.Create(@"StartA\Ruby 1.9.2-p290\Documentation\The Book of Ruby.lnk").Close();
            File.Create(@"StartA\Ruby 1.9.2-p290\Interactive Ruby.lnk").Close();
            File.Create(@"StartA\Ruby 1.9.2-p290\RubyGems Documentation Server.lnk").Close();
            File.Create(@"StartA\Ruby 1.9.2-p290\Start Command Prompt with Ruby.lnk").Close();
            File.Create(@"StartA\Ruby 1.9.2-p290\Uninstall Ruby 1.9.2-p290.lnk").Close();
            Directory.CreateDirectory(@"StartA\Startup");
            File.Create(@"StartA\Startup\Dropbox.lnk").Close();
            File.Create(@"StartA\Startup\ICS Downloader.lnk").Close();
            File.Create(@"StartA\Startup\KeePass Password Safe.lnk").Close();
            File.Create(@"StartA\Startup\SyncServer.lnk").Close();
            File.Create(@"StartA\Startup\Touch2PcPrinter.lnk").Close();
            File.Create(@"StartA\Startup\Yahoo! Widgets.lnk").Close();
            Directory.CreateDirectory(@"StartA\Tlhan Ghun");
            Directory.CreateDirectory(@"StartA\Tlhan Ghun\Paw");
            File.Create(@"StartA\Tlhan Ghun\Paw\Paw.lnk").Close();
            File.Create(@"StartA\Tlhan Ghun\Paw\Uninstall.lnk").Close();
            Directory.CreateDirectory(@"StartA\TracExplorer");
            File.Create(@"StartA\TracExplorer\TracExplorer Online Documentation.lnk").Close();
            Directory.CreateDirectory(@"StartA\TV Rename");
            Directory.CreateDirectory(@"StartA\Vector Magic");
            File.Create(@"StartA\Vector Magic\Samples.lnk").Close();
            File.Create(@"StartA\Vector Magic\Uninstall.lnk").Close();
            File.Create(@"StartA\Vector Magic\Vector Magic.lnk").Close();
            Directory.CreateDirectory(@"StartA\VobSub");
            Directory.CreateDirectory(@"StartA\Windows 7 USB DVD Download Tool");
            File.Create(@"StartA\Windows 7 USB DVD Download Tool\Uninstall Windows 7 USB DVD Download Tool.lnk").Close();
            File.Create(@"StartA\Windows 7 USB DVD Download Tool\Windows 7 USB DVD Download Tool.lnk").Close();
            Directory.CreateDirectory(@"StartA\WinRAR");
            File.Create(@"StartA\WinRAR\Console RAR manual.lnk").Close();
            File.Create(@"StartA\WinRAR\WinRAR help.lnk").Close();
            File.Create(@"StartA\WinRAR\WinRAR.lnk").Close();
            Directory.CreateDirectory(@"StartA\Wireless Music Sync for BlackBerry");
            Directory.CreateDirectory(@"StartA\WMV9 VCM");
            File.Create(@"StartA\WMV9 VCM\EULA.lnk").Close();
            File.Create(@"StartA\WMV9 VCM\Help.lnk").Close();
            File.Create(@"StartA\WMV9 VCM\Read_me.lnk").Close();
            Directory.CreateDirectory(@"StartA\XBMC");
            File.Create(@"StartA\XBMC\Uninstall XBMC.lnk").Close();
            File.Create(@"StartA\XBMC\XBMC.lnk").Close();
            Directory.CreateDirectory(@"StartA\Yamb 2.1.0.0 beta 2");
            File.Create(@"StartA\Yamb 2.1.0.0 beta 2\Uninstall.lnk").Close();
            File.Create(@"StartA\Yamb 2.1.0.0 beta 2\Yamb - Website.lnk").Close();
            File.Create(@"StartA\Yamb 2.1.0.0 beta 2\Yamb.lnk").Close();
            File.Create(@"StartA\bbwifimusicsync-sf.net.lnk").Close();
            File.Create(@"StartA\Console.lnk").Close();
            File.Create(@"StartA\CreateLinks.lnk").Close();
            File.Create(@"StartA\Cygwin.lnk").Close();
            File.Create(@"StartA\Fetchmail.lnk").Close();
            File.Create(@"StartA\Internet Explorer (64-bit).lnk").Close();
            File.Create(@"StartA\Internet Explorer.lnk").Close();
            File.Create(@"StartA\MKVExtractGUI2.exe.lnk").Close();
            File.Create(@"StartA\Nir Launcher.lnk").Close();
            File.Create(@"StartA\nithin@athena.lnk").Close();
            File.Create(@"StartA\nithin@eos.lnk").Close();
            File.Create(@"StartA\nithin@kaia.lnk").Close();
            File.Create(@"StartA\nithin@leto.lnk").Close();
            File.Create(@"StartA\nithin@sdf.org.lnk").Close();
            File.Create(@"StartA\NSIS.lnk").Close();
            File.Create(@"StartA\SCP - nithin@192.168.0.110.lnk").Close();
            File.Create(@"StartA\SCP - nithin@192.168.0.111.lnk").Close();
            File.Create(@"StartA\SCP - nithin@192.168.0.112.lnk").Close();
            File.Create(@"StartA\SCP - nithin@ssh.nithinphilips.com.lnk").Close();
            File.Create(@"StartA\SCP - spikiermonkey,flashcardmaster@web.sourceforge.net.lnk").Close();
            File.Create(@"StartA\Update Checker.lnk").Close();
            File.Create(@"StartA\Windows PowerShell - CMEIAS Installer.lnk").Close();
            // End Dummy Start Menu

            _knownCategories.Add("Accessories");
            _knownCategories.Add("Administrative Tools");

            t = TemplateParser.Parse(Common.GetTestDataFullPath("TransformationTest.Setup.ini"));

            foreach (var templateCategory in t)
                _knownCategories.Add(templateCategory.Name);

            _startMenu = new StartMenu();
            _startMenu.KnownCategories = _knownCategories;
            _startMenu.AddLocation("StartA");
        }

        [TestMethod]
        public void StandardRun()
        {
            List<Command> realCommand = new List<Command>();
            var list = t.TransformStartMenu(_startMenu).ToList();
            foreach (var command in list)
            {
                command.Execute();
                realCommand.Add((command as MoveProgramItemCommand).GetRealCommand());
            }

            foreach (var command in realCommand)
            {
                command.Execute();
            }

            var cleanupCommands = t.CleanupStartMenu(_startMenu).ToList();

            foreach (var cleanupCommand in cleanupCommands)
            {
                cleanupCommand.Execute();
            }

        }
    }
}
