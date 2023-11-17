using Scellecs.Morpeh;
using UnityUI = UnityEngine.UI;

namespace Slimebones.ECSCore.Utils
{
    public interface IListener
    {
        // subscriber pattern: https://stackoverflow.com/a/63490134
        public void Subscribe(
            Entity e, World world
        );
        public void Unsubscribe();
    }
}