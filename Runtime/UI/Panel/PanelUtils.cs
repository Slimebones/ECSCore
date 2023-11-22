using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.UI.Canvas;

namespace Slimebones.ECSCore.UI.Panel
{
    public static class PanelUtils
    {
        public static void Enable(
            string key,
            World world
        )
        {
            SetState(key, PanelStateChange.Enable, world);
        }

        public static void Disable(
            string key,
            World world
        )
        {
            SetState(key, PanelStateChange.Disable, world);
        }

        public static void Toggle(
            string key,
            World world
        )
        {
            SetState(key, PanelStateChange.Toggle, world);
        }

        public static void DecideEnable(
            bool isEnabled, string key, World world
        )
        {
            if (isEnabled)
            {
                Enable(key, world);
                return;
            }
            Disable(key, world);
        }

        public static void SetState(
            string key, PanelStateChange state, World world
        )
        {
            ref var request =
                ref RequestComponentUtils.Create<SetPanelStateRequest>(
                    1,
                    world
                );

            request.key = key;
            request.state = state;
        }
    }
}
