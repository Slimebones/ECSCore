using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Base
{
    public class EventCleanupSystem: ICleanupSystem
    {
        private Filter eventF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            eventF = World.Filter.With<EventMeta>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var e in eventF)
            {
                World.RemoveEntity(e);
            }
        }

        public void Dispose()
        {
            foreach (var e in eventF)
            {
                World.RemoveEntity(e);
            }
        }
    }
}
