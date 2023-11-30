using Scellecs.Morpeh;
using System;

namespace Slimebones.ECSCore.Base.Event
{
    public static class EventUtils
    {
        public static ref T Create<T>(
            World world
        ) where T : struct, IEventComponent
        {
            var e = world.CreateEntity();
            ref T c = ref e.AddComponent<T>();
            ref var meta = ref e.AddComponent<EventMeta>();
            return ref c;
        }
    }
}