using Scellecs.Morpeh;
using System;

namespace Slimebones.ECSCore.Condition
{
    public interface ICondition
    {
        public bool Check(
            Entity e,
            World world,
            Filter f1 = null,
            Filter f2 = null,
            Filter f3 = null,
            Filter f4 = null
        );
    }
}