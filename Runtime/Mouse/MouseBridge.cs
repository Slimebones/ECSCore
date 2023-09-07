using Scellecs.Morpeh;
using UnityEngine;

namespace Slimebones.ECSCore.Mouse {
    public class MouseBridge: Bridge {
        public void OnMouseEnter() {
            CreateMouseEvent(MouseEventType.Enter);
        }

        public void OnMouseOver() {
            CreateMouseEvent(MouseEventType.Over);
        }

        public void OnMouseExit() {
            CreateMouseEvent(MouseEventType.Exit);
        }

        public void OnMouseDown() {
            CreateMouseEvent(MouseEventType.Down);
        }

        public void OnMouseUpAsButton() {
            CreateMouseEvent(MouseEventType.UpAsButton);
        }

        public void OnMouseDrag() {
            CreateMouseEvent(MouseEventType.Drag);
        }

        // NOTE(ryzhovalex):
        //      OnMouseUp is called in GlobalBridge.OnMouseUp since it is
        //      common.

        private void CreateMouseEvent(MouseEventType type) {
            Entity eventE = world.CreateEntity();
            ref MouseEvent e =
                ref eventE.AddComponent<MouseEvent>();

            e.type = type;
            e.targetEntity = entity;
        }
    }
}
