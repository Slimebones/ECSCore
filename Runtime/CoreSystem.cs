using Scellecs.Morpeh;
using Slimebones.ECSCore.UI;

namespace Slimebones.ECSCore
{
    public class CoreSystem: ISystem
    {
        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            World.GetStash<Button>().AsDisposable();
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void Dispose()
        {
        }
    }
}
