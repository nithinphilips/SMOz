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

using System;
using System.Collections.Generic;
using System.Text;
using SMOz.Utilities;
using SMOz.StartMenu;

namespace SMOz.Commands.UI
{
    public delegate void ArrayRemoveDelegate(object obj);

    /// <summary>
    /// Encapsulates a command that converts a Start Menu item to a category
    /// </summary>
    public class ConvertToCategoryCommand : Command
    {
	   StartItem startItem;
	   ArrayRemoveDelegate removeDelegate;

	   public ConvertToCategoryCommand(StartItem startItem, ArrayRemoveDelegate removeDelegate) {
		  this.startItem = startItem;
		  this.removeDelegate = removeDelegate;
	   }

	   public override string Name {
		  get { return "Make Category"; }
	   }

	   public override CommandType Type {
		  get { return CommandType.UIConvertToCategory; }
	   }

	   public override void Execute() {
		  KnownCategories.Instance.Add(startItem.Name);
		  removeDelegate(startItem);
	   }

	   public override void UnExecute() {
		  
	   }
    }
}
