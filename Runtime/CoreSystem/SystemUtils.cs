using Scellecs.Morpeh;
using Slimebones.ECSCore.Logging;
using System;
using System.Collections.Generic;

namespace Slimebones.ECSCore.CoreSystem
{
    public static class SystemUtils
    {
        public static void IterateEntities(
            Filter f,
            Action<Entity> onEntity,
            int _startIterationCount = 0
        )
        {
            // avoid nested try...catch assuming that there are the same
            // order filter returns entities

            int iterationCount = 0;
            Entity iterationCurrentE = default;

            try
            {
                foreach (var e in f)
                {
                    iterationCount++;
                    if (_startIterationCount > iterationCount)
                    {
                        continue;
                    }

                    iterationCurrentE = e;
                    onEntity(e);
                }
            }
            catch (Exception exc)
            {
                Log.Skip(iterationCurrentE, exc);
                // start with the next iteration
                IterateEntities(f, onEntity, iterationCount + 1);
            }
        }
    }
}