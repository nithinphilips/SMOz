using System;

namespace LibSmoz
{
    public class IniParseException : Exception
    {
        /// <summary>
        /// The line number, if any, where the error occured.
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// The character in the line, if any, where the error occured
        /// </summary>
        public int CharacterNumber { get; set; }

        public IniParseException(string message, int lineNumber = -1, int characterNumber = -1)
            :base(FormatMessage(message, lineNumber, characterNumber))
        {
            LineNumber = lineNumber;
            CharacterNumber = characterNumber;
        }

        static string FormatMessage(string message, int lineNumber, int characterNumber)
        {
            if(lineNumber == -1) return message;
            return characterNumber == -1 ? 
                string.Format("At line {0}: {1}", lineNumber, message) : 
                string.Format("At line {0}, position {1}: {2}", lineNumber, characterNumber, message);
        }
    }
}