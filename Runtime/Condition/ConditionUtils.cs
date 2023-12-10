using Scellecs.Morpeh;
using Slimebones.ECSCore.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Slimebones.ECSCore.Condition
{
    public static class ConditionUtils
    {
        public static T Get<T>(ICondition[] conditions)
            where T: ICondition
        {
            foreach (var condition in conditions)
            {
                if (condition is T)
                {
                    return (T)condition;
                }
            }

            throw new NotFoundException("condition");
        }

        public static bool All(
            ICondition[] conditions,
            Entity e
        )
        {
            foreach (var condition in conditions)
            {
                if (!condition.Check(e))
                {
                    return false;
                }
            }

            return true;
        }
    }
}