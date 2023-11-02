using Scellecs.Morpeh;
using Slimebones.ECSCore.Utils;
using System.Collections.Generic;

namespace Slimebones.ECSCore.Input
{
    public static class InputUtils
    {
        public static ref InputSpecStorage InitInputSpecStorage(
            List<InputSpec> specs,
            World world
        )
        {
            var e = world.CreateEntity();
            ref var c = ref e.AddComponent<InputSpecStorage>();
            c.specs = specs;
            return ref c;
        }

        public static bool Listen(
            string name,
            World world
        )
        {
            Filter f = world.Filter.With<InputEvent>().Build();

            foreach (var e in f)
            {
                ref var c = ref e.GetComponent<InputEvent>();
                if (c.name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public static ref InputEvent ListenReturn(
            string name,
            World world
        )
        {
            Filter f = world.Filter.With<InputEvent>().Build();

            foreach (var e in f)
            {
                ref var c = ref e.GetComponent<InputEvent>();
                if (c.name == name)
                {
                    return ref c;
                }
            }

            throw new NotFoundException("any input event for given specs");
        }
    }
}