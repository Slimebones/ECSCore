using Scellecs.Morpeh;
using Slimebones.ECSCore.Lock;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils;
using System;
using System.Collections.Generic;

namespace Slimebones.ECSCore.Request
{
    public static class RequestUtils
    {
        public static readonly FilterBuilder FB =
            LockUtils
            .UnlockedFB
            .With<InternalRequestMeta>();

        public static ref T Create<T>() where T : struct, IRequestComponent
        {
            var e = World.Default.CreateEntity();
            ref T c = ref e.AddComponent<T>();

            // request is created anyway in order to conform with the
            // return type, but lock it via variable
            ref var meta = ref e.AddComponent<InternalRequestMeta>();

            return ref c;
        }

        public static void Complete(Entity e)
        {
            if (!e.Has<InternalRequestMeta>())
            {
                throw new NotFoundException("request meta component");
            }
            World.Default.RemoveEntity(e);
        }
    }
}