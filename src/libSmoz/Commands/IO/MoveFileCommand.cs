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

using System.Collections.Generic;
using System.IO;
using System.Text;
using LibSmoz.Model;

namespace LibSmoz.Commands.IO
{
    /// <summary>
    /// Moves an actual file or directory.
    /// </summary>
    public class MoveFileCommand : Command
    {
        protected MoveFileCommand() { }

        public MoveFileCommand(Dictionary<string, string> moveMap)
        {
            MoveMap = moveMap;
            MoveFileMode = MoveFileMode.Overwrite;

            foreach (var pair in moveMap)
            {
                if (File.Exists(pair.Key)) isFile = true;
                this.name = string.Format("Move '{0}' to '{1}'", Path.GetFileNameWithoutExtension(pair.Key), Path.GetDirectoryName(pair.Value));
                break;
            }
        }

        private bool isFile = false;
        public ProgramItem ProgramItem { get; private set; }
        public Dictionary<string, string> MoveMap { get; private set; }
        public MoveFileMode MoveFileMode { get; set; }

        protected string name;
        public override string Name
        {
            get { return this.name; }
        }

        public override CommandType Type
        {
            get { return CommandType.IOMove; }
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

        private void MoveFile(string source, string target)
        {
            // create target dir
            Directory.CreateDirectory(Path.GetDirectoryName(target));
            MoveFile(source, target, MoveFileMode);
        }

        private void MoveDirectory(string source, string target)
        {
            RecursiveMoveDirectory(source, target, MoveFileMode);
        }

        // ...Because .NET is piece of .SHIT
        // Watch for stack overflow, ye be warned
        // Note: This method can be rewritten to 'merge' two directories together (like 'move' in explorer does)
        public static void RecursiveMoveDirectory(string source, string target, MoveFileMode mode)
        {
            Directory.CreateDirectory(target);

            string[] subdirs = Directory.GetDirectories(source);

            for (int i = 0; i < subdirs.Length; i++)
            {
                RecursiveMoveDirectory(subdirs[i], Path.Combine(target, Path.GetFileName(subdirs[i])), mode);
            }

            string[] files = Directory.GetFiles(source);

            for (int i = 0; i < files.Length; i++)
            {
                string targetFile = Path.Combine(target, Path.GetFileName(files[i]));
                MoveFile(files[i], targetFile, mode);
            }

            Directory.Delete(source, false);

        }

        public static void MoveFile(string source, string target, MoveFileMode mode)
        {
            if (File.Exists(target))
            {
                switch (mode)
                {
                    case MoveFileMode.Overwrite:
                        File.Delete(target);
                        File.Move(source, target);
                        break;
                    case MoveFileMode.OverwriteIfNewer:
                        if (File.GetCreationTimeUtc(source) >= File.GetCreationTimeUtc(target))
                        {
                            File.Delete(target);
                            File.Move(source, target);
                        }
                        else
                        {
                            File.Delete(source);
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                File.Move(source, target);
            }
        }
    }
}
