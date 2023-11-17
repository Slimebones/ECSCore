using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using UnityUI = UnityEngine.UI;

namespace Slimebones.ECSCore.UI.Button
{
    /// <summary>
    /// Adds listeners to buttons.
    /// </summary>
    public class ButtonSystem: ISystem
    {
        private Filter buttonF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            buttonF = World.Filter.With<Button>().Build();

            foreach (var e in buttonF)
            {
                ButtonUtils.Register(e, World);
            }
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void Dispose()
        {
        }
    }
}
