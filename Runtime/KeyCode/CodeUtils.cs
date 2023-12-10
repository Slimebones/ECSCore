using Scellecs.Morpeh;
using Slimebones.ECSCore.Utils;

namespace Slimebones.ECSCore.KeyCode
{
    public static class CodeUtils
    {
        public static Entity GetEntity(string code, World world)
        {
            foreach (var e in world.Filter.With<Code>().Build())
            {
                ref var c = ref e.GetComponent<Code>();
                if (c.code == code)
                {
                    return e;
                }
            }

            throw new NotFoundException("entity with code " + code);
        }

        public static Entity GetEntity(
            string code,
            FilterBuilder fb
        )
        {
            foreach (var e in fb.With<Code>().Build())
            {
                ref var c = ref e.GetComponent<Code>();
                if (c.code == code)
                {
                    return e;
                }
            }

            throw new NotFoundException("entity with code " + code);
        }
    }
}