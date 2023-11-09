using Scellecs.Morpeh;
using System.Collections.Generic;
using System.Linq;

namespace Slimebones.ECSCore.Condition
{
    public static class ConditionUtils
    {
        public static bool All(
            ICondition[] conditions,
            Entity e,
            World world
        )
        {
            foreach (var condition in conditions)
            {
                if (!condition.Check(
                    e,
                    world
                ))
                {
                    return false;
                }
            }

            return true;
        }
    }
}