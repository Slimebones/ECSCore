using Scellecs.Morpeh;
using Slimebones.ECSCore.ActionSpec;
using System;
using System.Linq;
using UnityEngine;

namespace Slimebones.ECSCore.Object
{
    public struct GoDestroyActionSpec: IActionSpec
    {
        public GoDestroyActionSpecEntityData[] entities;

        public void Call(Entity e1, Entity e2, Entity e3, Entity e4)
        {
            if (entities == null)
            {
                // destroy nothing if entities unspecified
                return;
            }

            foreach (EntityArg entityArg in Enum.GetValues(typeof(EntityArg)))
            {
                // TODO(ryzhovalex):
                //      perf: cache entities in dictionary on first call
                foreach (var data in entities)
                {
                    if (data.arg == entityArg)
                    {
                        UnityEngine.Object.Destroy(
                            ActionSpecUtils.ChooseEntityByArg(
                                data.arg,
                                e1,
                                e2,
                                e3,
                                e4
                            ).GetUnityGo(),
                            data.destroyDelay
                        );
                    }
                }
            }
        }
    }
}