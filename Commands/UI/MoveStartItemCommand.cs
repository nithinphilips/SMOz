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
 *  Original FileName :  MoveStartItemCommand.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using SMOz.StartMenu;
using System.IO;

namespace SMOz.Commands.UI
{
    /// <summary>
    /// Moves a start item.
    /// </summary>
    /// <remarks>No changes are made to the actual file or directory by this command.</remarks>
    public class MoveStartItemCommand : Command
    {
	   protected MoveStartItemCommand() {}

	   public MoveStartItemCommand(StartItem startItem, string newCategory) {
		  this.startItem = startItem;

		  this.oldCategory = startItem.Category;
		  this.newCategory = newCategory;

		  this.oldName = startItem.Name;
		  this.newName = Path.Combine(newCategory, Path.GetFileName(startItem.Name));

		  this.name = "Move '" + Path.GetFileName(startItem.Name) + "'";

		  //Console.WriteLine("-***'{0}'***-", this.Name);
		  //Console.WriteLine("'{0}' to '{1}'", this.oldCategory, this.newCategory);
		  //Console.WriteLine("'{0}' to '{1}'", this.oldName, this.newName);
		  //Console.WriteLine("-******-");
	   }

	   protected string name;
	   protected StartItem startItem;

	   protected string newCategory;
	   protected string oldCategory;

	   protected string newName;
	   protected string oldName;
	   
	   public string NewName {
		  get { return newName; }
	   }

	   public string OldName {
		  get { return oldName; }
	   }

	   public override string Name {
		  get { return this.name; }
	   }

	   public override CommandType Type {
		  get { return CommandType.UIMove; }
	   }

	   public StartItem StartItem {
		  get { return startItem; }
	   }
	   
	   public string NewCategory {
		  get { return newCategory; }
	   }
	   

	   public string OldCategory {
		  get { return oldCategory; }
	   }

	   public override void Execute() {
		  startItem.Category = newCategory;
		  startItem.Name = newName;
	   }

	   public override void UnExecute() {
		  startItem.Category = oldCategory;
		  startItem.Name = oldName;
	   }
    }
}
