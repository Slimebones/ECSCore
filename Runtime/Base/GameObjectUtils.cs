using Scellecs.Morpeh;
using UnityEngine;

namespace Slimebones.ECSCore.Base
{
    public static class GameObjectUtils
    {
        public static ref GameObjectData GetOrError(Entity e)
        {
            if (!e.Has<GameObjectData>())
            {
                // every collider should have game object attached
                throw new MissingECSGameObjectException(e);
            }
            return ref e.GetComponent<GameObjectData>();
        }

        public static ref GameObject GetUnityOrError(Entity e)
        {
            ref GameObjectData GOECS = ref GetOrError(e);

            if (GOECS.value == null)
            {
                throw new UnsetECSGameObjectValueException(e);
            }

            return ref GOECS.value;
        }
    }
}