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
 *  Original FileName :  MoveProgramItemCommand.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibSmoz.Commands.IO;
using LibSmoz.Model;

namespace LibSmoz.Commands.UI
{
    /// <summary>
    /// Moves a start item.
    /// </summary>
    /// <remarks>No changes are made to the actual file or directory by this command.</remarks>
    public class MoveProgramItemCommand : Command
    {
        protected MoveProgramItemCommand() { }


        public MoveProgramItemCommand(ProgramItem programItem, ProgramCategory newCategory, string matchedSelector)
        {
            this.ProgramItem = programItem;

            this.OldCategory = programItem.Category;
            this.NewCategory = newCategory;
            this._name = string.Format("Move '{0}' to '{1}' because of '{2}'", Path.GetFileName(programItem.Name), newCategory.Name, matchedSelector);

            oldPaths = new List<string>(ProgramItem.Locations);
        }

        private List<string> oldPaths, newPaths;

        public MoveFileCommand GetRealCommand()
        {
            if (newPaths == null)
            {
                // Temporarily switch categories to generate the new list
                ProgramItem.Category = NewCategory;
                newPaths = new List<string>(ProgramItem.Locations);
                ProgramItem.Category = OldCategory;
            }

            var dict = new Dictionary<string, string>();
            for (int i = 0; i < oldPaths.Count; i++)
            {
                if ((ProgramItem.IsDirectory && Directory.Exists(oldPaths[i])) ||  File.Exists(oldPaths[i]) )
                {
                    dict.Add(oldPaths[i], newPaths[i]);
                }
            }
            return new MoveFileCommand(dict);
        }

        private string _name;
        public override string Name
        {
            get { return _name; }
        }

        public override CommandType Type
        {
            get { return CommandType.UIMove; }
        }

        public ProgramItem ProgramItem { get; protected set; }

        public ProgramCategory NewCategory { get; protected set; }
        public ProgramCategory OldCategory { get; protected set; }

        public override void Execute()
        {
            OldCategory.Remove(ProgramItem);
            ProgramItem.Category = NewCategory;
            NewCategory.Add(ProgramItem);
            newPaths = new List<string>(ProgramItem.Locations);
        }

        public override void UnExecute()
        {
            NewCategory.Remove(ProgramItem);
            ProgramItem.Category = OldCategory;
            OldCategory.Add(ProgramItem);
        }
    }
}
