using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Base
{
    public class ReqCleanupSystem: ICleanupSystem
    {
        private Filter requestF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            requestF = World.Filter.With<ReqMeta>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var e in requestF)
            {
                ref var pointer = ref e.GetComponent<ReqMeta>();
                if (pointer.callCount > pointer.requiredCallCountToComplete)
                {
                    World.RemoveEntity(e);
                }
            }
        }

        public void Dispose()
        {
            foreach (var e in requestF)
            {
                World.RemoveEntity(e);
            }
        }
    }
}
