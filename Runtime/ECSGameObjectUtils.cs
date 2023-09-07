using Scellecs.Morpeh;
using UnityEngine;

namespace JetPants.ClumsyDelivery.Core {
    public static class ECSGameObjectUtils {
        public static ref ECSGameObject GetOrError(Entity e) {
            if (!e.Has<ECSGameObject>()) {
                // every collider should have game object attached
                throw new MissingECSGameObjectException(e);
            }
            return ref e.GetComponent<ECSGameObject>();
        }

        public static ref GameObject GetUnityOrError(Entity e) {
            ref ECSGameObject GOECS = ref GetOrError(e);

            if (GOECS.value == null) {
                throw new UnsetECSGameObjectValueException(e);
            }

            return ref GOECS.value;
        }
    }
}