using Scellecs.Morpeh;
using Slimebones.ECSCore.Utils;

namespace Slimebones.ECSCore.ActionSpec
{
    public static class ActionSpecUtils
    {
        public static Entity ChooseEntityByArg(
            EntityArg arg,
            Entity e1,
            Entity e2,
            Entity e3,
            Entity e4
        )
        {
            switch (arg)
            {
                case EntityArg.Entity1:
                    return e1;
                case EntityArg.Entity2:
                    return e2;
                case EntityArg.Entity3:
                    return e3;
                case EntityArg.Entity4:
                    return e4;
                default:
                    throw new UnsupportedException(string.Format(
                        "arg {0}",
                        arg
                    ));
            }
        }
    }
}
