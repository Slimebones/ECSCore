using Scellecs.Morpeh;
using UnityEngine;

namespace Slimebones.ECSCore.Base.GO
{
    public static class GOEntityExtensions
    {
        public static GameObject GetUnityGO(this Entity e)
        {
            ref GOData GOECS = ref GOUtils.Get(e);

            if (GOECS.value == null)
            {
                throw new UnsetECSGameObjectValueException(e);
            }

            return GOECS.value;
        }
    }
}