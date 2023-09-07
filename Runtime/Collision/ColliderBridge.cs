using Scellecs.Morpeh;
using UnityEngine;

namespace Slimebones.ECSCore.Collision {
    /// <summary>
    /// Bridge between Unity Collisions and ECS.
    /// </summary>
    public class ColliderBridge: Bridge {
        public void OnCollisionEnter(UnityEngine.Collision collision) {
            ref CollisionEvent collisionEvent = ref CreateCollisionEvent(
                collision
            );
            collisionEvent.type = CollisionEventType.Enter;
        }

        public void OnCollisionStay(UnityEngine.Collision collision) {
            ref CollisionEvent collisionEvent = ref CreateCollisionEvent(
                collision
            );
            collisionEvent.type = CollisionEventType.Stay;
        }

        public void OnCollisionExit(UnityEngine.Collision collision) {
            ref CollisionEvent collisionEvent = ref CreateCollisionEvent(
                collision
            );
            collisionEvent.type = CollisionEventType.Exit;
        }

        public void OnTriggerEnter(UnityEngine.Collider collider) {
            ref CollisionEvent collisionEvent = ref CreateTriggerCollisionEvent(
                collider
            );
            collisionEvent.type = CollisionEventType.Enter;
        }

        public void OnTriggerStay(UnityEngine.Collider collider) {
            ref CollisionEvent collisionEvent = ref CreateTriggerCollisionEvent(
                collider
            );
            collisionEvent.type = CollisionEventType.Stay;
        }

        public void OnTriggerExit(UnityEngine.Collider collider) {
            ref CollisionEvent collisionEvent = ref CreateTriggerCollisionEvent(
                collider
            );
            collisionEvent.type = CollisionEventType.Exit;
        }

        private ref CollisionEvent CreateCollisionEvent(
            UnityEngine.Collision collision
        ) {
            ref CollisionEvent collisionEvent = ref CreateRawCollisionEvent();
            collisionEvent.collision = collision;
            collisionEvent.isTrigger = false;
            
            ColliderBridge otherColliderBridge =
                collision.gameObject.GetComponent<ColliderBridge>();
            collisionEvent.guestEntity =
                otherColliderBridge != null ? otherColliderBridge.Entity : null;

            return ref collisionEvent;
        }

        private ref CollisionEvent CreateTriggerCollisionEvent(
            UnityEngine.Collider collider
        ) {
            ref CollisionEvent collisionEvent = ref CreateRawCollisionEvent();

            collisionEvent.collider = collider;
            collisionEvent.isTrigger = true;

            ColliderBridge otherColliderBridge =
                collider.gameObject.GetComponent<ColliderBridge>();
            collisionEvent.guestEntity =
                otherColliderBridge != null ? otherColliderBridge.Entity : null;

            return ref collisionEvent;
        }

        private ref CollisionEvent CreateRawCollisionEvent() {
            Entity eventEntity = world.CreateEntity();

            ref CollisionEvent collisionEvent =
                ref eventEntity.AddComponent<CollisionEvent>();
            collisionEvent.hostEntity = entity;

            return ref collisionEvent;
        }
    }
}
