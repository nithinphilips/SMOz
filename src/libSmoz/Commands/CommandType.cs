namespace LibSmoz.Commands
{
    /// <summary>
    /// Specifies the type of a command.
    /// </summary>
    public enum CommandType
    {
        None = 0,
        /// <summary>
        /// A command that can hold a group of other commands and execute them in order.
        /// </summary>
        Group,
        UIConvertToCategory,
        UIMove,
        UIDelete,
        UIRename,
        IOMove,
        IODelete,
        IORename
    }
}