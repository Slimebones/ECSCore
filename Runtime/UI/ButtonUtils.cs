using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using UnityUI = UnityEngine.UI;

namespace Slimebones.ECSCore.UI
{
    public static class ButtonUtils
    {
        public static void Register(Entity e, World world)
        {
            ref var c = ref e.GetComponent<Button>();
            UnityUI.Button unityButton = GameObjectUtils.GetUnityOrError(
                e
            ).GetComponent<UnityUI.Button>();

            foreach (var listener in c.listeners)
            {
                listener.Subscribe(e, unityButton, world);
            }
        }
    }
}
