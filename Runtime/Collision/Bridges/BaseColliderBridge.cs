using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.Bridge;
using Slimebones.ECSCore.Base.Event;
using UnityEngine;

namespace Slimebones.ECSCore.Collision.Bridges
{
    /// <summary>
    /// Bridge between Unity Collisions and ECS.
    /// </summary>
    public class BaseColliderBridge: Bridge
    {
        [HideInInspector] public UnityEngine.Collider hostCollider;

        public void Start()
        {
            hostCollider = GetComponent<UnityEngine.Collider>();
        }
    }
}
