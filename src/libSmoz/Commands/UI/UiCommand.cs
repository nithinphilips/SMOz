using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibSmoz.Commands.Io;
using LibSmoz.ProgramsMenu;

namespace LibSmoz.Commands.Ui
{
    /// <summary>
    /// Subset of <see cref="Commands"/> that act on <see cref="StartMenu"/> instances.
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class NamespaceDoc { }

    ///<summary>
    /// Base class for a subset of <see cref="Commands"/> that act on <see cref="StartMenu"/> instances.
    ///</summary>
    public abstract class UiCommand : Command
    {
        /// <summary>
        /// Gets an <see cref="IoCommand"/> that applies the same action performed by a <see cref="UiCommand"/>, but on the file system.
        /// </summary>
        /// <returns>A command. If no corresponding commands exist, a <c>null</c> will be returned</returns>
        public abstract IoCommand GetIoCommand();
    }
}
