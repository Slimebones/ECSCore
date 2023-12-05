using UnityEngine;

namespace Slimebones.ECSCore.Collision.Bridges
{
    /// <summary>
    /// Bridge between Unity Collisions and ECS.
    /// </summary>
    public class BaseColliderBridge: Bridging.Bridge
    {
        [HideInInspector] public UnityEngine.Collider hostCollider;

        public void Start()
        {
            hostCollider = GetComponent<Collider>();
        }
    }
}
