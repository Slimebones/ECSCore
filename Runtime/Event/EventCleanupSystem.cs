using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Event
{
    public class EventCleanupSystem: ICleanupSystem
    {
        private Filter allEventsF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            allEventsF = World.Filter.With<InternalEventMeta>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var e in allEventsF)
            {
                if (e.Has<InternalEventFresh>())
                {
                    e.RemoveComponent<InternalEventFresh>();
                    continue;
                }
                World.RemoveEntity(e);
            }
        }

        public void Dispose()
        {
        }
    }
}
