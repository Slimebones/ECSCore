using Scellecs.Morpeh;
using System;

namespace Slimebones.ECSCore.Condition
{
    public interface ICondition
    {
        public bool Check(Entity e, World world);
    }

    public interface ICondition<T1>
    {
        public bool Check(Entity e, World world, T1 a1);
    }

    public interface ICondition<T1, T2>
    {
        public bool Check(Entity e, World world, T1 a1, T2 a2);
    }

    public interface ICondition<T1, T2, T3>
    {
        public bool Check(Entity e, World world, T1 a1, T2 a2, T3 a3);
    }

    public interface ICondition<T1, T2, T3, T4>
    {
        public bool Check(Entity e, World world, T1 a1, T2 a2, T3 a3, T4 a4);
    }
}