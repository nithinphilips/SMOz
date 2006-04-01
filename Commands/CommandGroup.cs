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
 *  Original FileName :  CommandGroup.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace SMOz.Commands
{
    public class CommandGroup : Command	
    {
	   public CommandGroup(string name)
		  : this(name, new List<Command>()) { }

	   public CommandGroup(string name, List<Command> commands) {
		  this.name = name;
		  this.commands = commands;
	   }

	   public CommandGroup(string name, IEnumerable<Command> commands) {
		  this.name = name;
		  this.commands = new List<Command>(commands);
	   }

	   List<Command> commands;

	   public List<Command> Commands {
		  get { return commands; }
		  set { commands = value; }
	   }

	   string name;

	   public override string Name {
		  get { return this.name; }
	   }

	   public override CommandType Type {
		  get { return CommandType.Group; }
	   }

	   public override void Execute() {
		  foreach (Command cmd in this.commands) {
			 cmd.Execute();
		  }
	   }

	   public override void UnExecute() {
		  for (int i = this.commands.Count - 1; i >= 0; i--) {
			 this.commands[i].UnExecute();
		  }
	   }
    }
}
