using Scellecs.Morpeh;
using UnityEngine;

namespace Slimebones.ECSCore.Object
{
    public static class GoEntityExtensions
    {
        public static GameObject GetUnityGo(this Entity e)
        {
            ref Go GOECS = ref GoUtils.Get(e);

            if (GOECS.value == null)
            {
                throw new UnsetECSGameObjectValueException(e);
            }

            return GOECS.value;
        }
    }
}