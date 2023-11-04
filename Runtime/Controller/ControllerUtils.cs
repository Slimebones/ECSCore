using Scellecs.Morpeh;
using Slimebones.ECSCore.Utils;
using System.Collections.Generic;

namespace Slimebones.ECSCore.Controller
{
    public static class ControllerUtils
    {
        public static ref ControllerSpecStorage InitStorage(
            Dictionary<string, IController> spec,
            World world
        )
        {
            var e = world.CreateEntity();
            ref var c = ref e.AddComponent<ControllerSpecStorage>();
            c.spec = spec;
            return ref c;
        }
    }
}