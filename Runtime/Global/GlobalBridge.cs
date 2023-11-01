using Slimebones.ECSCore.Mouse;
using Scellecs.Morpeh;
using UnityEngine;
using Slimebones.ECSCore.Base;

namespace Slimebones.ECSCore.Global
{
    /// <summary>
    /// Activate global Unity logic for ECS layer.
    /// </summary>
    public class GlobalBridge : Bridge {
        public void OnMouseUp() {
            CreateMouseEvent(MouseEventType.Up); 
        }

        private void CreateMouseEvent(MouseEventType type) {
            Entity eventE = world.CreateEntity();
            ref MouseEvent e =
                ref eventE.AddComponent<MouseEvent>();

            e.type = type;
            e.targetEntity = entity;
        }
    }
}
