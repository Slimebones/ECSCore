using UnityEngine;

namespace Slimebones.ECSCore.Utils
{
    public static class RigidbodyUtils
    {
        /// <summary>
        /// Gets car speed in km/h.
        /// </summary>
        /// <returns>
        /// Current car's speed in km/h.
        /// </returns>
        public static float GetSpeed(Rigidbody rigidbody) {
            // https://discussions.unity.com/t/how-to-get-car-speed/66478/2
            return rigidbody.velocity.magnitude * 3.6f;
        }
    }
}