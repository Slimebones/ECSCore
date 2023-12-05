using System;

namespace Slimebones.ECSCore.Base.Bridge
{
    /// <summary>
    /// Calls an action on destroy.
    /// </summary>
    public class OnDestroyBridge: Bridge
    {
        public Action<OnDestroyBridge> action;

        public void OnDestroy()
        {
            if (action != null)
            {
                action(this); 
            }
        }
    }
}