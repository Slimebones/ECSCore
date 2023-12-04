using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.Request;
using System;

namespace Slimebones.ECSCore.UI.Panel
{
    public static class PanelUtils
    {
        public static void Enable(
            Entity e
        )
        {
            SetState(e, PanelStateChange.Enable);
        }

        public static void Enable(
            string key
        )
        {
            SetState(key, PanelStateChange.Enable);
        }

        public static void Disable(
            Entity e
        )
        {
            SetState(e, PanelStateChange.Disable);
        }

        [Obsolete("use version without passing world")]
        public static void Enable(
            string key,
            World world
        )
        {
            SetState(key, PanelStateChange.Enable);
        }

        [Obsolete("use version without passing world")]
        public static void Disable(
            Entity e,
            World world
        )
        {
            SetState(e, PanelStateChange.Disable);
        }

        [Obsolete("use version without passing world")]
        public static void Disable(
            string key,
            World world
        )
        {
            SetState(key, PanelStateChange.Disable);
        }

        public static void Disable(
            string key
        )
        {
            SetState(key, PanelStateChange.Disable);
        }

        [Obsolete("use version without passing world")]
        public static void Toggle(
            Entity e,
            World world
        )
        {
            SetState(e, PanelStateChange.Toggle);
        }

        public static void Toggle(
            Entity e
        )
        {
            SetState(e, PanelStateChange.Toggle);
        }

        [Obsolete("use version without passing world")]
        public static void Toggle(
            string key,
            World world
        )
        {
            SetState(key, PanelStateChange.Toggle);
        }

        [Obsolete("use version without passing world")]
        public static void DecideEnable(
            bool isEnabled,
            Entity e,
            World world
        )
        {
            if (isEnabled)
            {
                Enable(e);
                return;
            }
            Disable(e);
        }

        public static void Toggle(
            string key
        )
        {
            SetState(key, PanelStateChange.Toggle);
        }

        public static void DecideEnable(
            bool isEnabled,
            Entity e
        )
        {
            if (isEnabled)
            {
                Enable(e);
                return;
            }
            Disable(e);
        }

        [Obsolete("use version without passing world")]
        public static void DecideEnable(
            bool isEnabled, string key, World world
        )
        {
            if (isEnabled)
            {
                Enable(key);
                return;
            }
            Disable(key);
        }

        [Obsolete("use version without passing world")]
        public static void SetState(
            string key,
            PanelStateChange state,
            World world
        )
        {
            ref var request =
                ref RequestUtils.Create<SetPanelStateRequest>();

            request.key = key;
            request.state = state;
        }

        [Obsolete("use version without passing world")]
        public static void SetState(
            Entity e,
            PanelStateChange state,
            World world
        )
        {
            SetState(
                e.GetComponent<Key.Key>().key,
                state
            );
        }

        public static void DecideEnable(
            bool isEnabled, string key
        )
        {
            if (isEnabled)
            {
                Enable(key);
                return;
            }
            Disable(key);
        }

        public static void SetState(
            string key,
            PanelStateChange state
        )
        {
            ref var request =
                ref RequestUtils.Create<SetPanelStateRequest>();

            request.key = key;
            request.state = state;
        }

        public static void SetState(
            Entity e,
            PanelStateChange state
        )
        {
            SetState(
                e.GetComponent<Key.Key>().key,
                state
            );
        }
    }
}
