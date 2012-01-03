using LibSmoz.ProgramsMenu;
using LibSmoz.Transformation;

namespace LibSmoz.Comparators
{
    internal static class EqualityComparers
    {
        public static EqualityComparer<Category> CategoryComparer { get; private set; }
        public static EqualityComparer<CategoryItem> CategoryItemComparer { get; private set; }
        public static EqualityComparer<ProgramCategory> ProgramCategoryComparer { get; private set; }
        public static EqualityComparer<ProgramItem> ProgramItemComparer { get; private set; }

        static EqualityComparers()
        {
            CategoryComparer = new EqualityComparer<Category>();
            CategoryItemComparer = new EqualityComparer<CategoryItem>();
            ProgramCategoryComparer = new EqualityComparer<ProgramCategory>();
            ProgramItemComparer = new EqualityComparer<ProgramItem>();
        }
    }
}