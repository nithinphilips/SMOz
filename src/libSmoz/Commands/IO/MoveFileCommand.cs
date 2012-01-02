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
 *  Original FileName :  MoveFileCommand.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using LibSmoz.Model;

namespace LibSmoz.Commands.IO
{
    /// <summary>
    /// Moves an actual file or directory.
    /// </summary>
    [Serializable]
    public class MoveFileCommand : Command
    {
        public MoveFileCommand()
        {
            
        }

        public MoveFileCommand(Dictionary<string, string> moveMap)
        {
            MoveMap = moveMap;
            MoveFileMode = MoveFileMode.Overwrite;

            foreach (var pair in moveMap)
            {
                if (File.Exists(pair.Key)) isFile = true;
                this.Name = string.Format("Move '{0}' to '{1}'", pair.Key, pair.Value);
                break;
            }
        }

        private bool isFile = false;

        public Dictionary<string, string> MoveMap { get; set; }
        public MoveFileMode MoveFileMode { get; set; }

        
        public override CommandType Type
        {
            get { return CommandType.IOMove; }
        }

        public MoveFileCommand GetReverseCommand()
        {
            Dictionary<string, string> reverseMap = MoveMap.ToDictionary(x => x.Value, x => x.Key);
            return new MoveFileCommand(reverseMap);
        }

        public override void Execute()
        {
            foreach (var pair in MoveMap)
            {
                if(isFile)
                    MoveFile(pair.Key, pair.Value);
                else
                    MoveDirectory(pair.Key, pair.Value);
            }
        }

        public override void UnExecute()
        {
            foreach (var pair in MoveMap)
            {
                if(isFile)
                    MoveFile(pair.Value, pair.Key);
                else
                    MoveDirectory(pair.Value, pair.Key);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pair in MoveMap)
            {
                sb.AppendFormat("Move '{0}' to\n     '{1}'\n", pair.Key, pair.Value);
            }
            return sb.ToString();
        }

        private void MoveFile(string source, string destination)
        {
            // create destination dir
            Directory.CreateDirectory(Path.GetDirectoryName(destination));
            MoveFile(source, destination, MoveFileMode);
        }

        private void MoveDirectory(string source, string target)
        {
            MoveDirectory(source, target, MoveFileMode);
        }

        /// <summary>
        /// Moves a directory from one location to another.
        /// </summary>
        /// <param name="source">The source directory.</param>
        /// <param name="destination">The destination directory</param>
        /// <param name="mode">Controls the action taken when the source files exists at destination.</param>
        public static void MoveDirectory(string source, string destination, MoveFileMode mode)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(Path.Combine(destination, dirPath.Substring(source.Length)));

            //Copy all the files
            foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
                MoveFile(newPath, Path.Combine(destination, newPath.Substring(source.Length)), mode);

        }

        /// <summary>
        /// Moves a file from source to destination
        /// </summary>
        /// <param name="source">The source file.</param>
        /// <param name="destination">The destination to move the file.</param>
        /// <param name="mode">Controls the action taken when the source files exists at destination.</param>
        public static void MoveFile(string source, string destination, MoveFileMode mode)
        {
            if (!File.Exists(destination))
            {
                File.Move(source, destination);
            }
            else
            {
                switch (mode)
                {
                    case MoveFileMode.Overwrite:
                        File.Delete(destination);
                        File.Move(source, destination);
                        break;
                    case MoveFileMode.OverwriteIfNewer:
                        if (File.GetCreationTimeUtc(source) >= File.GetCreationTimeUtc(destination))
                        {
                            File.Delete(destination);
                            File.Move(source, destination);
                        }
                        else
                        {
                            File.Delete(source);
                        }
                        break;
                    default:
                        throw new ArgumentException("Unknown enum value");
                }
            }
        }
    }
}
