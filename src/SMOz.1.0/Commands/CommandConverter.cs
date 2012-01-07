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
 *  Original FileName :  CommandConverter.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using SMOz.Commands.UI;
using SMOz.Commands.IO;
using System.IO;
using SMOz.Utilities;

namespace SMOz.Commands
{
    /// <summary>
    /// Converts UI operations to IO operations
    /// </summary>
    public sealed class CommandConverter
    {
	   public static MoveFileCommand[] ConvertMove(MoveStartItemCommand uiCommand) {

		  List<MoveFileCommand> result = new List<MoveFileCommand>();
		  foreach (string root in Utility.GetAllRoots()) {
			 string source = Path.Combine(root, uiCommand.OldName);
			 string target = Path.Combine(Path.Combine(root, uiCommand.NewCategory), Path.GetFileName(uiCommand.NewName));

			 if(uiCommand.StartItem.Exists(source)){
				result.Add(new MoveFileCommand(source, target, uiCommand.StartItem, uiCommand.OldName, uiCommand.NewName, uiCommand.StartItem.Exists(target))); 
			 }
		  }
		  return result.ToArray();
	   }

	   public static RenameFileCommand[] ConvertRename(RenameStartItemCommand uiCommand) {

		  List<RenameFileCommand> result = new List<RenameFileCommand>();
		  foreach (string root in Utility.GetAllRoots()) {
			 string source = Path.Combine(root, uiCommand.OldName);
			 string target = Path.Combine(Path.Combine(root, uiCommand.NewCategory), Path.GetFileName(uiCommand.NewName));

			 if (uiCommand.StartItem.Exists(source)) {
				result.Add(new RenameFileCommand(source, target, uiCommand.StartItem, uiCommand.OldName, uiCommand.NewName, uiCommand.StartItem.Exists(target)));
			 }
		  }
		  return result.ToArray();
	   }

	   public static DeleteFileCommand[] ConvertDelete(DeleteStartItemCommand uiCommand) {

		  List<DeleteFileCommand> result = new List<DeleteFileCommand>();
		  foreach (string root in Utility.GetAllRoots()) {
			 string source = Path.Combine(root, uiCommand.StartItem.RealName);
			 string target = "";
			 // All local item are in local trash, everything else is in user trash
			 if(root == Utility.LOCAL_START_ROOT){
				target = Path.Combine(Utility.LOCAL_TRASH_ROOT, GetRandomString());
			 }else{
				target = Path.Combine(Utility.USER_TRASH_ROOT, GetRandomString());
			 }

			 if (uiCommand.StartItem.Exists(source)) {
				result.Add(new DeleteFileCommand(source, target, uiCommand.StartItem, uiCommand.StartItem.Name, ""));
			 }
		  }
		  return result.ToArray();
	   }

	   private static string GetRandomString() {
		  return Guid.NewGuid().ToString();
	   }
    }
}
