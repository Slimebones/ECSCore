using Scellecs.Morpeh;
using System;

namespace Slimebones.ECSCore.Condition
{
    public interface ICondition
    {
        public bool Check(Entity e);
    }
}