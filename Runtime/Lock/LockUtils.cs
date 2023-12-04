using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Lock
{
    public static class LockUtils
    {
        public static readonly FilterBuilder LockedFB =
            World
            .Default
            .Filter
            .With<InternalLock>();
        public static readonly FilterBuilder UnlockedFB =
            World
            .Default
            .Filter
            .Without<InternalLock>();
        public static readonly FilterBuilder LockedByComponentFB =
            LockedFB
            .With<InternalComponentLock>();
        // here are no UnlockedByComponentFB since unlocked by component
        // entities are deleted on unlock

        public static void Lock(Entity e)
        {
            e.AddComponent<InternalLock>();
        }

        public static bool IsAnyLockedByComponent<T>()
            where T: struct, IComponent
        {
            return
                LockedByComponentFB.With<T>().Build().FirstOrDefault()
                    != null;
        }

        /// <summary>
        /// Locks by component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Lock<T>()
            where T: struct, IComponent
        {
            var e = World.Default.CreateEntity();
            e.AddComponent<T>();
            e.AddComponent<InternalComponentLock>();
            Lock(e);
        }

        /// <summary>
        /// Unlocks by component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Unlock<T>()
            where T: struct, IComponent
        {
            foreach (var e in LockedByComponentFB.With<T>().Build())
            {
                World.Default.RemoveEntity(e);
            }
        }

        public static void Unlock(Entity e)
        {
            e.RemoveComponent<InternalLock>();
        }

        public static bool IsLocked(Entity e)
        {
            return e.Has<InternalLock>();
        }
    }
}