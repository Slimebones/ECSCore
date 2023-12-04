using Scellecs.Morpeh;
using Slimebones.ECSCore.Logging;
using System;
using System.Collections.Generic;

namespace Slimebones.ECSCore.Base.Request
{
    public static class RequestUtils
    {
        private static List<Type> lockedTypes = new List<Type>();

        public static ref T Create<T>(
            int requiredCallCountToComplete
        ) where T : struct, IRequestComponent
        {
            if (requiredCallCountToComplete == 0)
            {
                throw new Exception(
                    "cannot create request with 0 required call count"
                );
            }

            var e = World.Default.CreateEntity();
            ref T c = ref e.AddComponent<T>();

            // request is created anyway in order to conform with the
            // return type, but lock it via variable
            ref var meta = ref e.AddComponent<RequestMeta>();
            meta.callCount = 0;
            meta.requiredCallCountToComplete = requiredCallCountToComplete;
            meta.isLocked = lockedTypes.Contains(typeof(T));

            return ref c;
        }

        /// <summary>
        /// Registers a call for a request entity. Should be called before
        /// the request's processing.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>
        /// False if the request had been already completed before
        /// this register. True otherwise. Useful to avoid double request
        /// usage. Note that call is registered despite the returned result
        /// for compatibility reasons. False typically means that the
        /// request shouldn't be processed (e.g. is locked).
        /// </returns>
        public static bool RegisterCall(
            Entity e
        )
        {
            bool result = true;
            ref var meta = ref e.GetComponent<RequestMeta>();

            if (
                meta.isLocked
                || IsCompletedMeta(ref meta)
            )
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

        public static void Lock<T>()
            where T : struct, IRequestComponent
        {
            if (!lockedTypes.Contains(typeof(T)))
            {
                lockedTypes.Add(typeof(T));
            }
        }

        public static void Unlock<T>()
            where T : struct, IRequestComponent
        {
            if (lockedTypes.Contains(typeof(T)))
            {
                lockedTypes.Remove(typeof(T));
            }
        }

        public static void UnlockAll()
        {
            lockedTypes.Clear();
        }

        private static bool IsCompletedMeta(ref RequestMeta meta)
        {
            return
                meta.callCount
                >= meta.requiredCallCountToComplete;
        }

    }
}