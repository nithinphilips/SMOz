namespace LibSmoz.Transformation
{
    /// <summary>
    /// Enumerated list of the possible types of a <see cref="CategoryItem"/>.
    /// </summary>
    public enum CategoryItemType
    {
        /// <summary>
        /// Specifies that the <see cref="CategoryItem"/> must be matched as an exact string.
        /// A String value of "value" will result in regex "^value$".
        /// </summary>
        String, 
        /// <summary>
        /// Specifies that the <see cref="CategoryItem"/> must be matched as a wildcard.
        /// A WildCard value of "value" will result in regex ".*value.*".
        /// </summary>
        WildCard,
        /// <summary>
        /// Specifies that the <see cref="CategoryItem"/> must be matched as a regular expression.
        /// </summary>
        Regex
    };
}