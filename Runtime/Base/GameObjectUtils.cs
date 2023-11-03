using Scellecs.Morpeh;
using UnityEngine;

namespace Slimebones.ECSCore.Base
{
    public static class GameObjectUtils
    {
        public static ref GameObjectStorage GetOrError(Entity e)
        {
            if (!e.Has<GameObjectStorage>())
            {
                // every collider should have game object attached
                throw new MissingECSGameObjectException(e);
            }
            return ref e.GetComponent<GameObjectStorage>();
        }

        public static ref GameObject GetUnityOrError(Entity e)
        {
            ref GameObjectStorage GOECS = ref GetOrError(e);

            if (GOECS.value == null)
            {
                throw new UnsetECSGameObjectValueException(e);
            }

            return ref GOECS.value;
        }
    }
}