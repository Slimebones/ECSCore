using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Component
{
    public class RequestCleanupSystem: ICleanupSystem
    {
        private Filter requestF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            requestF = World.Filter.With<RequestPointer>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var e in requestF)
            {
                ref var pointer = ref e.GetComponent<RequestPointer>();
                if (pointer.callCount > pointer.requiredCallCountToComplete)
                {
                    World.RemoveEntity(e);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
