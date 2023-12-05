using Scellecs.Morpeh;
using Slimebones.ECSCore.Collision.Bridges;
using Slimebones.ECSCore.Event;
using System.Xml;
using UnityEngine;

namespace Slimebones.ECSCore.Collision
{
    internal static class InternalCollisionUtils {
        public static ref CollisionEvent CreateCollisionEvent(
            UnityEngine.Collision collision,
            Entity e,
            Collider unityHostCollider,
            World world
        ) {
            ref CollisionEvent collisionEvent = ref CreateRawCollisionEvent(
                e,
                unityHostCollider,
                world
            );
            collisionEvent.unityCollision = collision;
            collisionEvent.isTrigger = false;
            
            BaseColliderBridge otherColliderBridge =
                collision.gameObject.GetComponent<BaseColliderBridge>();
            collisionEvent.guestEntity =
                otherColliderBridge != null ? otherColliderBridge.Entity : null;

            return ref collisionEvent;
        }

        public static ref CollisionEvent CreateTriggerCollisionEvent(
            Collider collider,
            Entity e,
            Collider unityHostCollider,
            World world
        ) {
            ref CollisionEvent collisionEvent = ref CreateRawCollisionEvent(
                e,
                unityHostCollider,
                world
            );

            collisionEvent.unityGuestCollider = collider;
            collisionEvent.isTrigger = true;

            // take first possible collider bridge with base
            // this will work exactly as expected as collider components
            // with empty array types, as they often are guests
            var otherColliderBridge =
                collider.gameObject.GetComponent<BaseColliderBridge>();
            collisionEvent.guestEntity =
                otherColliderBridge != null
                ? otherColliderBridge.Entity : null;

            return ref collisionEvent;
        }

        private static ref CollisionEvent CreateRawCollisionEvent(
            Entity hostE,
            Collider unityHostCollider,
            World world
        ) {
            Entity evte = EventUtils.CreateReturnEntity<CollisionEvent>();
            ref var evt =
                ref evte.GetComponent<CollisionEvent>();

            evt.hostEntity = hostE;
            evt.unityHostCollider = unityHostCollider;

            if (hostE.Has<ContactActions>())
            {
                evte.AddComponent<ContactActionsEventPointer>();
            }

            return ref evt;
        }
    }
}