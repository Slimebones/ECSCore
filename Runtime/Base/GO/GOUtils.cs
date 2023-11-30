using Scellecs.Morpeh;
using UnityEngine;

namespace Slimebones.ECSCore.Base.GO
{
    public static class GOUtils
    {
        public static ref GOData GetOrError(Entity e)
        {
            if (!e.Has<GOData>())
            {
                // every collider should have game object attached
                throw new MissingECSGameObjectException(e);
            }
            return ref e.GetComponent<GOData>();
        }

        public static GameObject GetUnityOrError(Entity e)
        {
            ref GOData GOECS = ref GetOrError(e);

            if (GOECS.value == null)
            {
                throw new UnsetECSGameObjectValueException(e);
            }

            return GOECS.value;
        }
    }
}