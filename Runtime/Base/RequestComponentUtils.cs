using Scellecs.Morpeh;
using System;

namespace Slimebones.ECSCore.Base
{
    public static class RequestComponentUtils
    {
        public static ref T Create<T>(
            int requiredCallCountToComplete,
            World world
        ) where T : struct, IRequestComponent
        {
            var e = world.CreateEntity();
            ref T c = ref e.AddComponent<T>();
            ref var meta = ref e.AddComponent<RequestMeta>();
            meta.callCount = 0;
            meta.requiredCallCountToComplete = requiredCallCountToComplete;
            return ref c;
        }

        public static void RegisterCall(
            Entity e
        )
        {
            ref var meta = ref e.GetComponent<RequestMeta>();
            meta.callCount++;
        }

        public static bool IsCompleted(Entity e)
        {
            ref var requestMeta =
                ref e.GetComponent<RequestMeta>();
            return
                requestMeta.callCount
                >= requestMeta.requiredCallCountToComplete;
        }

    }
}