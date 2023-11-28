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
            Filter f = world.Filter.With<InputEvt>().Build();

            foreach (var e in f)
            {
                ref var c = ref e.GetComponent<InputEvt>();
                if (c.name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Listen(
            string name,
            InputEvtType type,
            World world
        )
        {
            Filter f = world.Filter.With<InputEvt>().Build();

            foreach (var e in f)
            {
                ref var c = ref e.GetComponent<InputEvt>();
                if (c.name == name && c.type == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static ref InputEvt ListenReturn(
            string name,
            World world
        )
        {
            Filter f = world.Filter.With<InputEvt>().Build();

            foreach (var e in f)
            {
                ref var c = ref e.GetComponent<InputEvt>();
                if (c.name == name)
                {
                    return ref c;
                }
            }

            throw new NotFoundException("any input event for given specs");
        }
    }
}