using JetPants.ClumsyDelivery.Core.Mouse;
using Scellecs.Morpeh;
using UnityEngine;

namespace JetPants.ClumsyDelivery.Core.Global {
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
