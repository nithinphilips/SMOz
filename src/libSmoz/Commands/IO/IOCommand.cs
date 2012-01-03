using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibSmoz.Commands.Io
{
    /// <summary>
    /// Subset of <see cref="Commands"/> that act on files and directories.
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class NamespaceDoc { }

    /// <summary>
    /// The base class for all commands that act on files or directories.
    /// </summary>
    public abstract class IoCommand: Command
    {
        /// <summary>
        /// Creates an <see cref="IoCommand"/>, which when executed, will reverse any actions performed by the <see cref="IoCommand"/> that created it.
        /// </summary>
        /// <returns>An <see cref="IoCommand"/> that is the opposite of the instance that created it.</returns>
        public abstract IoCommand GetReverseCommand();
    }
}
