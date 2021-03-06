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
 *  Original FileName :  RenameFileCommand.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;

namespace LibSmoz.Commands.Io
{
    /// <summary>
    /// Renames an actual file or directory.
    /// </summary>
    [Serializable]
    public class RenameFileCommand : MoveFileCommand
    {
        public RenameFileCommand() { }

        public RenameFileCommand(Dictionary<string, string> renameMap)
            :base(renameMap)
        {
            foreach (var pair in renameMap)
            {
                this.Name = string.Format("Rename '{0}' to '{1}'", Path.GetFileNameWithoutExtension(pair.Key), Path.GetDirectoryName(pair.Value));
                break;
            }
	   }

	   public override CommandType Type {
		  get { return CommandType.IORename; }
	   }
    }
}
