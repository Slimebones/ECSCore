using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Utils
{
    public static class EntityExtensions
    {
        public static bool InCategory<T>(this Entity e)
            where T : struct, ICategory
        {
            return default(T).Has(e);
        }
    }
}