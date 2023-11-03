using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Base
{
    public static class EntityUtils
    {
        // Destroy Entity and attached GameObject (if any attached).
        public static void DestroyWithGameObject(Entity entity)
        {
            GameObjectStorage gameObject =
                entity.GetComponent<GameObjectStorage>(out bool hasGameObject);

            if (hasGameObject)
            {
                UnityEngine.Object.Destroy(gameObject.value);
            }

            entity.Dispose();
        }
    }
}