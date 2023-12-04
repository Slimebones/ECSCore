using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Base.Category
{
    public static class CategoryEntityExtensions
    {
        public static bool InCategory<T>(this Entity e)
            where T : struct, ICategory
        {
            return default(T).Has(e);
        }
    }
}