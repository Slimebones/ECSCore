using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;

namespace Slimebones.ECSCore.UI.Panel
{
    public static class PanelUtils
    {
        public static void Enable(
            Entity e,
            World world
        )
        {
            SetState(e, PanelStateChange.Enable, world);
        }

        public static void Enable(
            string key,
            World world
        )
        {
            SetState(key, PanelStateChange.Enable, world);
        }

        public static void Disable(
            Entity e,
            World world
        )
        {
            SetState(e, PanelStateChange.Disable, world);
        }

        public static void Disable(
            string key,
            World world
        )
        {
            SetState(key, PanelStateChange.Disable, world);
        }

        public static void Toggle(
            Entity e,
            World world
        )
        {
            SetState(e, PanelStateChange.Toggle, world);
        }

        public static void Toggle(
            string key,
            World world
        )
        {
            SetState(key, PanelStateChange.Toggle, world);
        }

        public static void DecideEnable(
            bool isEnabled, Entity e, World world
        )
        {
            if (isEnabled)
            {
                Enable(e, world);
                return;
            }
            Disable(e, world);
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
            string key,
            PanelStateChange state,
            World world
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

        public static void SetState(
            Entity e,
            PanelStateChange state,
            World world
        )
        {
            SetState(
                e.GetComponent<Key.Key>().key,
                state,
                world
            );
        }
    }
}
