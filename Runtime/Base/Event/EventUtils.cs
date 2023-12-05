using Scellecs.Morpeh;
using System;

namespace Slimebones.ECSCore.Base.Event
{
    public static class EventUtils
    {
        public static readonly FilterBuilder FB = 
            World
            .Default
            .Filter
            .With<InternalEventMeta>()
            .Without<InternalEventFresh>();

        public static ref T Create<T>() where T: struct, IEventComponent
        {
            return ref CreateReturnEntity<T>().GetComponent<T>();
        }

        public static Entity CreateReturnEntity<T>()
            where T: struct, IEventComponent
        {
            var e = World.Default.CreateEntity();
            e.AddComponent<InternalEventFresh>();
            e.AddComponent<InternalEventMeta>();
            e.AddComponent<T>();
            return e;
        }
    }
}