using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;

namespace Slimebones.ECSCore.Condition
{
    public class ConditionService
    {
        private static Dictionary<string, ICondition> data =
            new Dictionary<string, ICondition>();

        public static void Register(
            string key, ICondition condition, World world
        )
        {
            // overwriting is allowed, since the static data is saved across
            // the scenes
            condition.Init(world);
            data[key] = condition;
        }

        /// <summary>
        /// Checks multiple conditions - all should return true.
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool Check(
            string[] keys,
            Entity e
        )
        {
            foreach (var name in keys)
            {
                try
                {
                    if (!data[name].Check(e))
                    {
                        return false;
                    }
                }
                catch (KeyNotFoundException)
                {
                    Debug.LogWarning("not found condition key " + name);
                    continue;
                }
            }

            return true;
        }
    }
}