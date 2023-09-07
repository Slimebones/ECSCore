using Scellecs.Morpeh;

namespace JetPants.ClumsyDelivery.Core {
    public class ECSComponentUtils {
        /// <summary>
        /// Adds a new component for entity, or skip, if such component exists.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="entity"></param>
        public static void AddOrSkip<TComponent>(
            Entity entity
        ) where TComponent: struct, IComponent {
            entity.GetComponent<TComponent>(out bool hasComponent);
            if (!hasComponent) {
                entity.AddComponent<TComponent>();
            }
        }
    }
}