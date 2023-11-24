using static Slimebones.ECSCore.Utils.Delegates;

namespace Slimebones.ECSCore.Utils
{
    public static class StructUtils
    {
        public static T? GetOrNull<T>(ReturnFunc<T> getter)
            where T: struct
        {
            try
            {
                return getter();
            }
            catch
            {
                return null;
            }
        }
    }
}