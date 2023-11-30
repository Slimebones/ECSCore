using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Base.Storage
{
    public static class StorageUtils
    {
        public static ref T Get<T>(World world)
            where T: struct, IStorageComponent
        {
            return
                ref world.Filter.With<T>().Build().First().GetComponent<T>();
        }
    }
}
