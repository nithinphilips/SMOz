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
 *  Original FileName :  Command.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace SMOz.Commands
{
    public enum CommandType
    {
	   None = 0,
	   Group,
	   UIConvertToCategory,
	   UIMove,
	   UIDelete,
	   UIRename,
	   IOMove,
	   IODelete,
	   IORename
    }

    /// <summary>
    /// Base class for all Undo/Redo commands.
    /// </summary>
    /// <remarks>This class is abstract</remarks>
    public abstract class Command{
	   /// <summary>
	   /// Display name of this command.
	   /// </summary>
	   public abstract string Name { get; }

	   /// <summary>
	   /// Type of this command. If, an unknown command <code>CommandType.None</code> is returned
	   /// </summary>
	   public abstract CommandType Type { get; }

	   public abstract void Execute();
	   public abstract void UnExecute();

	   public override string ToString() {
		  return this.Name;
	   }

	   public static string TypeToString(CommandType type) {
		  switch (type) {
			 case CommandType.None:
				return "None";
			 case CommandType.Group:
				return "Group";
			 case CommandType.UIMove:
				return "Move";
			 case CommandType.UIDelete:
				return "Delete";
			 case CommandType.UIRename:
				return "Rename";
			 case CommandType.IOMove:
				return "Move";
			 case CommandType.IODelete:
				return "Delete";
			 case CommandType.IORename:
				return "Rename";
			 default:
				return type.ToString();
		  }
	   }
    }
}
