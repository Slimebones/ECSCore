using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using UnityUI = UnityEngine.UI;

namespace Slimebones.ECSCore.UI.Button
{
    public static class ButtonUtils
    {
        public static void Register(Entity e, World world)
        {
            ref var c = ref e.GetComponent<Button>();

            foreach (var listener in c.listeners)
            {
                listener.Subscribe(e, world);
            }
        }
    }
}
