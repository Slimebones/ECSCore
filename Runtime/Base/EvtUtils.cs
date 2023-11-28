using Scellecs.Morpeh;
using System;

namespace Slimebones.ECSCore.Base
{
    public static class EvtUtils
    {
        public static ref T Create<T>(
            World world
        ) where T : struct, IEvtComponent
        {
            var e = world.CreateEntity();
            ref T c = ref e.AddComponent<T>();
            ref var meta = ref e.AddComponent<EvtMeta>();
            return ref c;
        }
    }
}