using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Base
{
    public static class RequestComponentUtils
    {
        public static ref T Create<T>(
            int requiredCallCountToComplete,
            World world
        ) where T : struct, IRequestComponent
        {
            var e = world.CreateEntity();
            ref T c = ref e.AddComponent<T>();
            ref var meta = ref e.AddComponent<RequestMeta>();
            meta.callCount = 0;
            meta.requiredCallCountToComplete = requiredCallCountToComplete;
            return ref c;
        }

        /// <summary>
        /// Registers a call for a request entity.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>
        /// False if the request had been already completed before
        /// this register. True otherwise. Useful to avoid double request
        /// usage. Note that call is registered despite the returned result
        /// for compatibility reasons.
        /// </returns>
        public static bool RegisterCall(
            Entity e
        )
        {
            bool result = true;
            ref var meta = ref e.GetComponent<RequestMeta>();

            if (IsCompletedMeta(ref meta))
            {
                result = false;
            }

            meta.callCount++;

            return result;
        }

        public static bool IsCompleted(Entity e)
        {
            ref var requestMeta =
                ref e.GetComponent<RequestMeta>();
            return IsCompletedMeta(ref requestMeta);
        }

        private static bool IsCompletedMeta(ref RequestMeta meta)
        {
            return
                meta.callCount
                >= meta.requiredCallCountToComplete;
        }

    }
}