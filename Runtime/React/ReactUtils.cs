using Scellecs.Morpeh;
using Slimebones.ECSCore.Utils;

namespace Slimebones.ECSCore.React
{
    public static class ReactUtils
    {
        public static T GetListenerFromReactEntity<T>(
            Entity e
        ) where T: IEntityListener
        {
            ref var reactC = ref e.GetComponent<React>();

            foreach (var listener in reactC.listeners)
            {
                if (listener is T)
                {
                    return (T)listener;
                }
            }

            throw new NotFoundException(
                "listener with type for entity",
                e.ToString()
            );
        }    
    }
}