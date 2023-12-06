using Scellecs.Morpeh;
using System;

namespace Slimebones.ECSCore.ActionSpec
{
    public interface IActionSpec
    {
        public void Call(Entity e1, Entity e2, Entity e3, Entity e4);
    }
}