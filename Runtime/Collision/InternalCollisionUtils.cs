using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.Event;
using Slimebones.ECSCore.Collision.Bridges;
using System.Xml;
using UnityEngine;

namespace Slimebones.ECSCore.Collision
{
    internal static class InternalCollisionUtils {
        public static ref CollisionEvent CreateCollisionEvent(
            UnityEngine.Collision collision,
            Entity e,
            UnityEngine.Collider unityHostCollider,
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
            UnityEngine.Collider collider,
            Entity e,
            UnityEngine.Collider unityHostCollider,
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
            Entity e,
            UnityEngine.Collider unityHostCollider,
            World world
        ) {
            ref var collisionEvent =
                ref EventUtils.Create<CollisionEvent>();
            collisionEvent.hostEntity = e;
            collisionEvent.unityHostCollider = unityHostCollider;

            return ref collisionEvent;
        }
    }
}