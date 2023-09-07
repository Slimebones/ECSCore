using UnityEngine;

namespace Slimebones.ECSCore.Utils {
    /// <summary>
    /// Extension for Unity debug purposes.
    /// </summary>
    public class DebugPlus {
        public static void LogJoin<T>(T[] iterable) {
            Debug.LogFormat("[{0}]", string.Join(", ", iterable));
        }
    }
}
