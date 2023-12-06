using Scellecs.Morpeh;
using Slimebones.ECSCore.ActionSpec;
using System;
using System.Linq;
using UnityEngine;

namespace Slimebones.ECSCore.GO
{
    public struct GODestroyActionSpec2E: IActionSpec2E
    {
        public EntityArg[] entitiesToDestroy;
        public float destroyDelayEntity1;
        public float destroyDelayEntity2;

        public void Call(Entity e1, Entity e2)
        {
            if (entitiesToDestroy == null)
            {
                // destroy nothing if indexes unspecified
                return;
            }

            foreach (EntityArg entityArg in Enum.GetValues(typeof(EntityArg)))
            {
                if (entitiesToDestroy.Contains(entityArg))
                {
                    DestroyByEntityArg(e1, entityArg);
                }
            }
        }

        private void DestroyByEntityArg(Entity e, EntityArg entityArg)
        {
            float destroyDelay =
                entityArg == EntityArg.Entity1
                ? destroyDelayEntity1
                : entityArg == EntityArg.Entity2
                    ? destroyDelayEntity2
                    : 0f;
            UnityEngine.Object.Destroy(e.GetUnityGO(), destroyDelay);
        }
    }
}