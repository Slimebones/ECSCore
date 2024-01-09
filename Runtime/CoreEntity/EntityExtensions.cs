using Scellecs.Morpeh;
using Slimebones.ECSCore.Object;

namespace Slimebones.ECSCore.CoreEntity
{
    public static class EntityExtensions
    {
        /// <summary>
        /// Adds a new component for entity, or skip, if such component exists.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="entity"></param>
        public static void AddComponentOrSkip<TComponent>(
            this Entity entity
        ) where TComponent : struct, IComponent
        {
            entity.GetComponent<TComponent>(out bool hasComponent);
            if (!hasComponent)
            {
                entity.AddComponent<TComponent>();
            }
        }
        // Destroy Entity and attached GameObject (if any attached).
        public static void DestroyWithGameObject(
            this Entity entity
        )
        {
            Go gameObject =
                entity.GetComponent<Go>(out bool hasGameObject);

            if (hasGameObject)
            {
                UnityEngine.Object.Destroy(gameObject.value);
            }

            entity.Dispose();
        }
    }
}