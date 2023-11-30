using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.GO;

namespace Slimebones.ECSCore.Base
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
        public static void DestroyWithGameObject(this Entity entity)
        {
            GOData gameObject =
                entity.GetComponent<GOData>(out bool hasGameObject);

            if (hasGameObject)
            {
                UnityEngine.Object.Destroy(gameObject.value);
            }

            entity.Dispose();
        }
    }
}