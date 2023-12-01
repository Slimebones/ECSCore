using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.Bridge;
using Slimebones.ECSCore.Base.Event;
using UnityEngine;

namespace Slimebones.ECSCore.Collision.Bridges
{
    /// <summary>
    /// Bridge between Unity Collisions and ECS.
    /// </summary>
    public class CollisionStayColliderBridge: BaseColliderBridge
    {
        public void OnCollisionStay(UnityEngine.Collision collision)
        {
            ref CollisionEvent collisionEvent =
                ref InternalCollisionUtils.CreateCollisionEvent(
                    collision,
                    Entity,
                    hostCollider,
                    world
                );
            collisionEvent.type = CollisionEventType.Stay;
        }
    }
}
