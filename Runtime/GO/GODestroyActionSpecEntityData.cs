using System;

namespace Slimebones.ECSCore.ActionSpec
{
    [Serializable]
    public struct GODestroyActionSpecEntityData: IActionSpecEntityData
    {
        public EntityArg arg;
        public float destroyDelay;

        public EntityArg Arg => arg;
    }
}