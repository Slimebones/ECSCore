using Scellecs.Morpeh;
using Slimebones.ECSCore.Lock;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils;
using System;
using System.Collections.Generic;

namespace Slimebones.ECSCore.Lock
{
    public static class LockUtils
    {
        public static readonly FilterBuilder LockedFB =
            World
            .Default
            .Filter
            .With<Lock>();
        public static readonly FilterBuilder UnlockedFB =
            World
            .Default
            .Filter
            .Without<Lock>();

        public static void Lock(Entity e)
        {
            e.AddComponent<Lock>();
        }

        public static void Unlock(Entity e)
        {
            e.RemoveComponent<Lock>();
        }

        public static void UnlockAll()
        {
            foreach (var lockedE in LockedFB.Build())
            {
                lockedE.RemoveComponent<Lock>();
            }
        }

        public static bool IsLocked(Entity e)
        {
            return e.Has<Lock>();
        }
    }
}