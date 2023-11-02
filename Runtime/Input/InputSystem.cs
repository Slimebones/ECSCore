using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Input
{
    /// <summary>
    /// Generates input events listening to retrieved InputActions.
    /// </summary>
    public class InputSystem: ISystem
    {
        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void Dispose()
        {
        }
    }
}
