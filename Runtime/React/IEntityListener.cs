using Scellecs.Morpeh;

namespace Slimebones.ECSCore.React
{
    public interface IEntityListener
    {
        // subscriber pattern: https://stackoverflow.com/a/63490134
        public void Subscribe(
            Entity e, World world
        );
        public void Unsubscribe();
    }
}