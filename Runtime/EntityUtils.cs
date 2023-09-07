using Scellecs.Morpeh;

namespace Slimebones.ECSCore {
    public static class EntityUtils {
        // Destroy Entity and attached GameObject (if any attached).
        public static void DestroyWithGameObject(Entity entity) {
            ECSGameObject gameObject =
                entity.GetComponent<ECSGameObject>(out bool hasGameObject);

            if (hasGameObject) {
                UnityEngine.Object.Destroy(gameObject.value);
            }

            entity.Dispose();
        } 
    }
}