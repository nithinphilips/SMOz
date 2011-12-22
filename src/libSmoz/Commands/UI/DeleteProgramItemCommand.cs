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
 *  Original FileName :  DeleteProgramItemCommand.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using LibSmoz.Commands.IO;
using LibSmoz.Model;

namespace LibSmoz.Commands.UI
{
    /// <summary>
    /// Deletes a start item.
    /// </summary>
    /// <remarks>No changes are made to the actual file or directory by this command.</remarks>
    public class DeleteProgramItemCommand : Command
    {
        public DeleteProgramItemCommand(ProgramItem programItem)
        {
            this.programItem = programItem;
            this.programItemName = programItem.Name;
            this.name = "Delete '" + programItem.Name + "'";
        }

        ProgramItem programItem;
        string name;
        string programItemName;

        public DeleteFileCommand GetRealCommand()
        {
            return new DeleteFileCommand(programItem.RealLocations);
        }

        public override string Name
        {
            get { return this.name; }
        }

        public override CommandType Type
        {
            get { return CommandType.UIDelete; }
        }

        public string ProgramItemName
        {
            get { return programItemName; }
        }

        public ProgramItem ProgramItem
        {
            get { return programItem; }
        }

        public override void Execute()
        {
            programItem.Category.Remove(programItem);
        }

        public override void UnExecute()
        {
            programItem.Category.Add(programItem);
        }
    }
}
