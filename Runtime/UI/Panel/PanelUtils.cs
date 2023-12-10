using Scellecs.Morpeh;
using Slimebones.ECSCore.KeyCode;
using Slimebones.ECSCore.Request;
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
            string refcode
        )
        {
            SetState(refcode, PanelStateChange.Enable);
        }

        public static void Disable(
            Entity e
        )
        {
            SetState(e, PanelStateChange.Disable);
        }

        [Obsolete("use version without passing world")]
        public static void Enable(
            string refcode,
            World world
        )
        {
            SetState(refcode, PanelStateChange.Enable);
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
            string refcode,
            World world
        )
        {
            SetState(refcode, PanelStateChange.Disable);
        }

        public static void Disable(
            string refcode
        )
        {
            SetState(refcode, PanelStateChange.Disable);
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
            string refcode,
            World world
        )
        {
            SetState(refcode, PanelStateChange.Toggle);
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
            string refcode
        )
        {
            SetState(refcode, PanelStateChange.Toggle);
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
            bool isEnabled, string refcode, World world
        )
        {
            if (isEnabled)
            {
                Enable(refcode);
                return;
            }
            Disable(refcode);
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

            request.refcode = key;
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
                e.GetComponent<Code>().key,
                state
            );
        }

        public static void DecideEnable(
            bool isEnabled, string refcode
        )
        {
            if (isEnabled)
            {
                Enable(refcode);
                return;
            }
            Disable(refcode);
        }

        public static void SetState(
            string key,
            PanelStateChange state
        )
        {
            ref var request =
                ref RequestUtils.Create<SetPanelStateRequest>();

            request.refcode = key;
            request.state = state;
        }

        public static void SetState(
            Entity e,
            PanelStateChange state
        )
        {
            SetState(
                e.GetComponent<Code>().key,
                state
            );
        }
    }
}
