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
		  string localSource = Path.Combine(Utility.LOCAL_START_ROOT, uiCommand.OldName);
		  string userSource = Path.Combine(Utility.USER_START_ROOT, uiCommand.OldName);

		  string localTarget = Path.Combine(Path.Combine(Utility.LOCAL_START_ROOT, uiCommand.NewCategory), Path.GetFileName(uiCommand.NewName));
		  string userTarget = Path.Combine(Path.Combine(Utility.USER_START_ROOT, uiCommand.NewCategory), Path.GetFileName(uiCommand.NewName));

		  List<MoveFileCommand> result = new List<MoveFileCommand>();
		  if (uiCommand.StartItem.HasLocal) { result.Add(new MoveFileCommand(localSource, localTarget, uiCommand.StartItem, uiCommand.OldName, uiCommand.NewName)); }
		  if (uiCommand.StartItem.HasUser) { result.Add(new MoveFileCommand(userSource, userTarget, uiCommand.StartItem, uiCommand.OldName, uiCommand.NewName)); }
		  return result.ToArray();
	   }

	   public static RenameFileCommand[] ConvertRename(RenameStartItemCommand uiCommand) {
		  string localSource = Path.Combine(Utility.LOCAL_START_ROOT, uiCommand.OldName);
		  string userSource = Path.Combine(Utility.USER_START_ROOT, uiCommand.OldName);

		  string localTarget = Path.Combine(Path.Combine(Utility.LOCAL_START_ROOT, uiCommand.NewCategory), Path.GetFileName(uiCommand.NewName));
		  string userTarget = Path.Combine(Path.Combine(Utility.USER_START_ROOT, uiCommand.NewCategory), Path.GetFileName(uiCommand.NewName));

		  List<RenameFileCommand> result = new List<RenameFileCommand>();	  
		  if (uiCommand.StartItem.HasLocal) { result.Add(new RenameFileCommand(localSource, localTarget, uiCommand.StartItem, uiCommand.OldName, uiCommand.NewName)); }
		  if (uiCommand.StartItem.HasUser) { result.Add(new RenameFileCommand(userSource, userTarget, uiCommand.StartItem, uiCommand.OldName, uiCommand.NewName)); }
		  return result.ToArray();
	   }

	   public static DeleteFileCommand[] ConvertDelete(DeleteStartItemCommand uiCommand) {
		  string localSource = uiCommand.StartItem.LocalPath;
		  string userSource = uiCommand.StartItem.UserPath;

		  string localTarget = Path.Combine(Utility.LOCAL_TRASH_ROOT, GetRandomString());
		  string userTarget = Path.Combine(Utility.USER_TRASH_ROOT, GetRandomString());

		  List<DeleteFileCommand> result = new List<DeleteFileCommand>();
		  if (uiCommand.StartItem.HasLocal) { result.Add(new DeleteFileCommand(localSource, localTarget, uiCommand.StartItem, uiCommand.StartItem.Name, "")); }
		  if (uiCommand.StartItem.HasUser) { result.Add(new DeleteFileCommand(userSource, userTarget, uiCommand.StartItem, uiCommand.StartItem.Name, "")); }
		  return result.ToArray();
	   }

	   private static string GetRandomString() {
		  return Guid.NewGuid().ToString();
	   }
    }
}
