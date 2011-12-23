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
 *  Original FileName :  RenameStartItemCommand.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using LibSmoz.Model;
using System.IO;

namespace LibSmoz.Commands.UI
{
    /// <summary>
    /// Renames a start item.
    /// </summary>
    /// <remarks>No changes are made to the actual file or directory by this command.</remarks>
    public class RenameProgramItemCommand : Command
    {
        public RenameProgramItemCommand(ProgramItem programItem, string newName)
        {
            this.ProgramItem = programItem;

            this.OldName = programItem.Name;
            this.NewName = newName;

            this.name = "Rename '" + programItem.Name;
        }

        public ProgramItem ProgramItem { get; set; }
        public string OldName { get; set; }
        public string NewName { get; set; }

        private string name;
        public override string Name
        {
            get
            {
                return name;
            }
        }

        public override CommandType Type
        {
            get { return CommandType.UIRename; }
        }

        public override void Execute()
        {
            ProgramItem.Name = NewName;
        }

        public override void UnExecute()
        {
            ProgramItem.Name = OldName;
        }
    }
}