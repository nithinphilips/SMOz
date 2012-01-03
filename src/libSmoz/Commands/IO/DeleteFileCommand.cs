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
 *  Original FileName :  DeleteFileCommand.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace LibSmoz.Commands.Io
{
    /// <summary>
    /// Deletes an actual file or directory.
    /// </summary>
    /// <remarks>Deleted files or directories are moved to .\Application Data\Smoz\Trash</remarks>
    [Serializable]
    public class DeleteFileCommand : MoveFileCommand
    {
        /// <summary>
        /// Specifies the folder to move the files to be deleted
        /// </summary>
        public static string TrashFolder { get; set; }

        static DeleteFileCommand()
        {
            TrashFolder = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SMOz"), "Trash");
        }

        public DeleteFileCommand() { }

        public DeleteFileCommand(IEnumerable<string> filesToDelete)
            : base(filesToDelete.ToDictionary(file => file, file => Path.Combine(TrashFolder, Path.GetRandomFileName())))
        {
            this.Name = string.Format("Delete '{0}'", Path.GetFileNameWithoutExtension(filesToDelete.First()));
        }

        public override CommandType Type
        {
            get { return CommandType.IODelete; }
        }
    }
}
