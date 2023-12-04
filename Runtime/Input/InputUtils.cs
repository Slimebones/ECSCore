using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.Event;
using Slimebones.ECSCore.Utils;
using System.Collections.Generic;

namespace Slimebones.ECSCore.Input
{
    public static class InputUtils
    {
        public static ref InputSpecStorage InitInputSpecStorage(
            List<InputSpec> specs
        )
        {
            var e = World.Default.CreateEntity();
            ref var c = ref e.AddComponent<InputSpecStorage>();
            c.specs = specs;
            return ref c;
        }

        public static bool Listen(
            string name
        )
        {
            Filter f = EventUtils.FB.With<InputEvent>().Build();

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

        public static bool Listen(
            string name,
            InputEventType type
        )
        {
            Filter f = EventUtils.FB.With<InputEvent>().Build();

            foreach (var e in f)
            {
                ref var c = ref e.GetComponent<InputEvent>();
                if (c.name == name && c.type == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static ref InputEvent ListenReturn(
            string name
        )
        {
            Filter f = EventUtils.FB.With<InputEvent>().Build();

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