using Scellecs.Morpeh;
using Slimebones.ECSCore.Event;
using Slimebones.ECSCore.Storage;
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
            ref var storage = ref StorageUtils.Create<InputSpecStorage>();
            storage.specs = specs;
            storage.disabledSpecIndexes = new List<int>();
            storage.isEnabled = true;
            return ref storage;
        }

        public static void EnableSpec(string code)
        {
            ref var storage = ref StorageUtils.Get<InputSpecStorage>();

            for (int i = 0; i < storage.specs.Count; i++)
            {
                if (storage.specs[i].code == code)
                {
                    if (!storage.disabledSpecIndexes.Contains(i))
                    {
                        throw new AlreadyEventException(
                            "spec " + storage.specs[i],
                            "enabled"
                        );
                    }
                    storage.disabledSpecIndexes.Remove(i);
                    return;
                }
            }

            throw new NotFoundException("no spec found for code " + code);
        }

        public static void DisableSpec(string code)
        {
            ref var storage = ref StorageUtils.Get<InputSpecStorage>();

            for (int i = 0; i < storage.specs.Count; i++)
            {
                if (storage.specs[i].code == code)
                {
                    if (storage.disabledSpecIndexes.Contains(i))
                    {
                        throw new AlreadyEventException(
                            "spec " + storage.specs[i],
                            "disabled"
                        );
                    }
                    storage.disabledSpecIndexes.Add(i);
                    return;
                }
            }

            throw new NotFoundException("no spec found for code " + code);
        }

        public static void EnableAllSpecs()
        {
            ref var storage = ref StorageUtils.Get<InputSpecStorage>();
            storage.isEnabled = false;
        }

        public static void DisableAllSpecs()
        {
            ref var storage = ref StorageUtils.Get<InputSpecStorage>();
            storage.isEnabled = false;
        }

        public static void DecideEnableAllSpecs(bool flag)
        {
            if (flag)
            {
                EnableAllSpecs();
                return;
            }
            DisableAllSpecs();
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