using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Controller
{
    public static class ControllerUtils
    {
        public static void SetAll(
            bool isEnabled,
            World world
        )
        {
            foreach (var e in world.Filter.With<Controlled>().Build())
            {
                ref var c = ref e.GetComponent<Controlled>();

                c.isEnabled = isEnabled;
            }
        }

        public static void SetAllWhereAnyController<
            TController1
        >(
            bool isEnabled,
            World world
        )
            where TController1: IController
        {
            foreach (var e in world.Filter.With<Controlled>().Build())
            {
                ref var c = ref e.GetComponent<Controlled>();

                foreach (var controller in c.controllers)
                {
                    if (
                        controller is TController1
                    )
                    {
                        c.isEnabled = isEnabled;
                        break;
                    }
                }
            }
        }

        public static void SetAllWhereAnyController<
            TController1,
            TController2
        >(
            bool isEnabled,
            World world
        )
            where TController1: IController
            where TController2: IController
        {
            foreach (var e in world.Filter.With<Controlled>().Build())
            {
                ref var c = ref e.GetComponent<Controlled>();

                foreach (var controller in c.controllers)
                {
                    if (
                        controller is TController1
                        || controller is TController2
                    )
                    {
                        c.isEnabled = isEnabled;
                        break;
                    }
                }
            }
        }

        public static void SetAllWhereAnyController<
            TController1,
            TController2,
            TController3
        >(
            bool isEnabled
        )
            where TController1: IController
            where TController2: IController
            where TController3: IController
        {
            foreach (var e in World.Default.Filter.With<Controlled>().Build())
            {
                ref var c = ref e.GetComponent<Controlled>();

                foreach (var controller in c.controllers)
                {
                    if (
                        controller is TController1
                        || controller is TController2
                        || controller is TController3
                    )
                    {
                        c.isEnabled = isEnabled;
                        break;
                    }
                }
            }
        }

        public static void SetAllWhereAnyController<
            TController1,
            TController2,
            TController3,
            TController4
        >(
            bool isEnabled,
            World world
        )
            where TController1: IController
            where TController2: IController
            where TController3: IController
            where TController4: IController
        {
            foreach (var e in world.Filter.With<Controlled>().Build())
            {
                ref var c = ref e.GetComponent<Controlled>();

                foreach (var controller in c.controllers)
                {
                    if (
                        controller is TController1
                        || controller is TController2
                        || controller is TController3
                        || controller is TController4
                    )
                    {
                        c.isEnabled = isEnabled;
                        break;
                    }
                }
            }
        }
    }
}