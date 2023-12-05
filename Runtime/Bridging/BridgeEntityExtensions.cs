using Scellecs.Morpeh;
using Slimebones.ECSCore.GO;
using Slimebones.ECSCore.Utils;

namespace Slimebones.ECSCore.Bridging
{
    public static class BridgeEntityExtensions
    {
        public static T AddBridge<T>(
            this Entity e,
            World world
        ) where T : Bridge
        {
            var go = e.GetUnityGO();

            if (go.GetComponent<T>() != null)
            {
                throw new AlreadyEventException(
                    string.Format("game object {0}", go),
                    string.Format(
                        "has bridge {0} attached",
                        go.GetComponent<T>().GetType().ToString()
                    )
                );
            }

            var bridge = go.AddComponent<T>();
            bridge.Entity = e;
            bridge.World = world;
            return bridge;
        }

        public static bool HasBridge<T>(
            this Entity e
        ) where T : Bridge
        {
            return e.GetUnityGO().GetComponent<T>() != null;
        }
    }
}