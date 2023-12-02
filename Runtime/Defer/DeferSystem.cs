using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Defer
{
    public class DeferSystem: ISystem
    {
        private Filter deferRequestF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            deferRequestF = World.Filter.With<DeferRequest>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void Dispose()
        {
        }
    }
}
