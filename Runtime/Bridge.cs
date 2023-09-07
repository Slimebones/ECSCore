using Scellecs.Morpeh;
using UnityEngine;

namespace JetPants.ClumsyDelivery.Core {
    /// <summary>
    /// Connects Unity logic with ECS layer.
    /// </summary>
    public class Bridge: MonoBehaviour {
        protected Entity entity;
        protected World world;

        public Entity Entity {
            get {
                return entity;
            }
            set {
                entity = value;
            }
        }

        public World World {
            get {
                return world;
            }
            set {
                world = value;
            }
        }
    }
}
