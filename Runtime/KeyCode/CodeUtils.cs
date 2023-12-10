using Scellecs.Morpeh;
using Slimebones.ECSCore.Utils;

namespace Slimebones.ECSCore.KeyCode
{
    public static class CodeUtils
    {
        public static Entity GetEntity(string refcode, World world)
        {
            foreach (var e in world.Filter.With<Code>().Build())
            {
                ref var c = ref e.GetComponent<Code>();
                if (c.key == refcode)
                {
                    return e;
                }
            }

            throw new NotFoundException("entity with code " + refcode);
        }

        public static Entity GetEntity(
            string refcode,
            FilterBuilder fb
        )
        {
            foreach (var e in fb.With<Code>().Build())
            {
                ref var c = ref e.GetComponent<Code>();
                if (c.key == refcode)
                {
                    return e;
                }
            }

            throw new NotFoundException("entity with code " + refcode);
        }
    }
}