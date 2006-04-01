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
 *  Original FileName :  MoveFileCommand.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using SMOz.StartMenu;
using System.IO;

namespace SMOz.Commands.IO
{
    public class MoveFileCommand : Command
    {
	   protected MoveFileCommand() { }

	   public MoveFileCommand(string source, string target, StartItem startItem, string oldName, string newName) {
		  this.source = source;
		  this.target = target;
		  this.startItem = startItem;
		  this.oldName = oldName;
		  this.newName = newName;
		  this.name = "Move '" + this.source + "' to '" + this.target + "'";
	   }

	   protected string oldName;
	   protected string newName;
	   protected string source;
	   protected string target;
	   protected StartItem startItem;
	   protected string name;

	   public StartItem StartItem {
		  get { return startItem; }
	   }

	   public string NewName {
		  get { return newName; }
	   }

	   public string OldName {
		  get { return oldName; }
	   }


	   public string Target {
		  get { return target; }
	   }

	   public string Source {
		  get { return source; }
	   }

	   public override string Name {
		  get { return this.name; }
	   }

	   public override CommandType Type {
		  get { return CommandType.IOMove; }
	   }

	   public override void Execute() {
		  startItem.RealName = newName;
		  if (startItem.Type == StartItemType.File) {
			 MoveFile(source, target);
		  } else {
			 MoveDirectory(source, target);
		  }
	   }

	   public override void UnExecute() {
		  startItem.RealName = oldName;
		  if (startItem.Type == StartItemType.File) {
			 MoveFile(target, source);
		  } else {
			 MoveDirectory(target, source);
		  }
	   }

	   private void MoveFile(string source, string target) {
		  // create target dir
		  Directory.CreateDirectory(Path.GetDirectoryName(target));
		  File.Move(source, target);
	   }

	   private void MoveDirectory(string source, string target) {
		  Directory.CreateDirectory(Path.GetDirectoryName(target));
		  Directory.Move(source, target);
	   }
    }
}
