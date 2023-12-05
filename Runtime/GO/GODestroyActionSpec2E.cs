using Scellecs.Morpeh;
using Slimebones.ECSCore.ActionSpec;
using System.Linq;
using UnityEngine;

namespace Slimebones.ECSCore.GO
{
    public struct GODestroyActionSpec2E: IActionSpec2E
    {
        public int[] entityIndexesToDestroy;
        public float[] entityDestroyDelays;

        public void Call(Entity e1, Entity e2)
        {
            if (entityIndexesToDestroy == null)
            {
                // destroy nothing if indexes unspecified
                return;
            }

            for (int i = 1; i < 3; i++)
            {
                if (entityIndexesToDestroy.Contains(i))
                {
                    Destroy(e1, i);
                }
            }
        }

        private void Destroy(Entity e, int index)
        {
            float destroyDelay =
                entityDestroyDelays == null
                ? 0f
                : entityDestroyDelays[index];
            Object.Destroy(e.GetUnityGO(), destroyDelay);
        }
    }
}