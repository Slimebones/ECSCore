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
            List<bool> flags = new List<bool>();

            foreach (var condition in conditions)
            {
                flags.Add(condition.Check(
                    e,
                    world
                ));
            }

            return flags.All(x => x);
        }
    }
}