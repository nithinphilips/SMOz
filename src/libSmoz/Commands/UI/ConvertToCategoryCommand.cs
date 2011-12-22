/*************************************************************************
 *  SMOz (Start Menu Organizer)
 *  Copyleft (C) 2006 Nithin Philips
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
 *  Author            :  Nithin Philips <nithin@nithinphilips.com>
 *  Original FileName :  ConvertToCategoryCommand.cs
 *  Created           :  Wed Apr 19 2006
 *  Description       :  
 *************************************************************************/

using System.Collections.Generic;
using System.IO;
using LibSmoz.Model;

namespace LibSmoz.Commands.UI
{
    /// <summary>
    /// Encapsulates a command that converts a Start Menu item to a category
    /// </summary>
    public class ConvertToCategoryCommand : Command
    {
        ProgramItem programItem;
        private ICollection<string> KnownCategories { get; set; }

        public ConvertToCategoryCommand(ProgramItem programItem, ICollection<string> knownCategories)
        {
            this.programItem = programItem;
            this.KnownCategories = knownCategories;
            this.name = string.Format("Make {0} a category", Path.GetFileNameWithoutExtension(programItem.Name));
        }

        private string name;
        public override string Name
        {
            get { return name; }
        }

        public override CommandType Type
        {
            get { return CommandType.UIConvertToCategory; }
        }

        public override void Execute()
        {
            KnownCategories.Add(programItem.Name);
            programItem.Category.Remove(programItem);
        }

        public override void UnExecute()
        {
            KnownCategories.Remove(programItem.Name);
            programItem.Category.Add(programItem);
        }
    }
}
