using Scellecs.Morpeh;
using Slimebones.ECSCore.Utils;

namespace Slimebones.ECSCore.Key
{
    public static class KeyUtils
    {
        public static Entity GetEntity(string key, World world)
        {
            foreach (var e in world.Filter.With<Key>().Build())
            {
                ref var c = ref e.GetComponent<Key>();
                if (c.key == key)
                {
                    return e;
                }
            }

            throw new NotFoundException("entity with key", key);
        }

        public static Entity GetEntity(
            string key,
            FilterBuilder fb
        )
        {
            foreach (var e in fb.With<Key>().Build())
            {
                ref var c = ref e.GetComponent<Key>();
                if (c.key == key)
                {
                    return e;
                }
            }

            throw new NotFoundException("entity with key", key);
        }
    }
}