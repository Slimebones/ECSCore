using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Condition
{
    public interface ICondition
    {
        public void Init(World world);
        public bool Check(Entity e);
    }
}