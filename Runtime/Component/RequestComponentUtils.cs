using Scellecs.Morpeh;
using System;

namespace Slimebones.ECSCore.Component
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
            ref var pointer = ref e.AddComponent<RequestPointer>();
            pointer.callCount = 0;
            pointer.requiredCallCountToComplete = requiredCallCountToComplete;
            return ref c;
        }

        public static void RegisterCall(
            Entity e
        )
        {
            ref var pointer = ref e.GetComponent<RequestPointer>();
            pointer.callCount++;
        }
    }
}