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
using System.Xml.Serialization;
using LibSmoz.Commands.Io;

namespace LibSmoz.Commands
{
    /// <summary>
    /// <para>Commands implement a basic <a href="https://en.wikipedia.org/wiki/Command_pattern">Command design pattern</a>.</para>
    /// 
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class NamespaceDoc { }

    /// <summary>
    /// <para>Base class for all Undo/Redo commands.</para>
    /// </summary>
    [XmlInclude(typeof(MoveFileCommand)), XmlInclude(typeof(DeleteFileCommand)), XmlInclude(typeof(RenameFileCommand))]
    [Serializable]
    public abstract class Command
    {
        /// <summary>
        /// Display name of this command.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of this command. If, an unknown command <code>CommandType.None</code> is returned
        /// </summary>
        public abstract CommandType Type { get; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Reverses the changes made by a call to the <see cref="Command.Execute"/> method.
        /// </summary>
        public abstract void UnExecute();

        public override string ToString()
        {
            return this.Name;
        }

        public static string TypeToString(CommandType type)
        {
            switch (type)
            {
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
