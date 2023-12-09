using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Storage
{
    public static class StorageUtils
    {
        public static ref T Get<T>()
            where T : struct, IStorageComponent
        {
            return
                ref World
                .Default.Filter.With<T>().Build().First().GetComponent<T>();
        }

        public static ref T Create<T>()
            where T : struct, IStorageComponent
        {
            var e = World.Default.CreateEntity();
            return ref e.AddComponent<T>();
        }
    }
}
