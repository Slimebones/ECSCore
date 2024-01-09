using Scellecs.Morpeh;
using Slimebones.ECSCore.Utils;
using UnityEngine;

namespace Slimebones.ECSCore.Object
{
    public static class GoUtils
    {
        public static ref Go Get(Entity e)
        {
            if (!e.Has<Go>())
            {
                // every collider should have game object attached
                throw new MissingECSGameObjectException(e);
            }
            return ref e.GetComponent<Go>();
        }

        public static ref Entity GetFromUnity(GameObject unityGo)
        {
            var provider = unityGo.GetComponent<GoComponent>();
            if (provider == null)
            {
                throw new NotFoundException(
                    "go provider for game object " + unityGo.ToString()
                );
            }
        }

        public static GameObject GetUnity(Entity e)
        {
            ref Go GOECS = ref Get(e);

            if (GOECS.value == null)
            {
                throw new UnsetECSGameObjectValueException(e);
            }

            return GOECS.value;
        }
    }
}