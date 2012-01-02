using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LibSmoz
{
    internal class Common
    {
        public const StringComparison DefaultStringComparison = StringComparison.OrdinalIgnoreCase;
        public const RegexOptions DefaultRegexOptions = RegexOptions.IgnoreCase | RegexOptions.Singleline;
        public const string RestrictedCategorySelector = "->";
    }
}
