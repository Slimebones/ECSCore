using System;

namespace Slimebones.ECSCore.ActionSpec
{
    [Serializable]
    public struct GoDestroyActionSpecEntityData: IActionSpecEntityData
    {
        public EntityArg arg;
        public float destroyDelay;

        public EntityArg Arg => arg;
    }
}