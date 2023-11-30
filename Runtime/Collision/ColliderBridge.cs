using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Base.Event;
using UnityEngine;

namespace Slimebones.ECSCore.Collision
{
    /// <summary>
    /// Bridge between Unity Collisions and ECS.
    /// </summary>
    public class ColliderBridge: Bridge {
        [HideInInspector] public UnityEngine.Collider hostCollider;

        public void Start()
        {
            hostCollider = GetComponent<UnityEngine.Collider>();
        }

        public void OnCollisionEnter2D(Collision2D collision2D) {
            ref CollisionEvent collisionEvent = ref CreateCollision2DEvent(
                collision2D
            );
            collisionEvent.type = CollisionEventType.Enter;
        }

        public void OnCollisionStay2D(Collision2D collision2D) {
            ref CollisionEvent collisionEvent = ref CreateCollision2DEvent(
                collision2D
            );
            collisionEvent.type = CollisionEventType.Stay;
        }

        public void OnCollisionExit2D(Collision2D collision2D) {
            ref CollisionEvent collisionEvent = ref CreateCollision2DEvent(
                collision2D
            );
            collisionEvent.type = CollisionEventType.Exit;
        }

        public void OnTriggerEnter2D(Collider2D collider2D) {
            ref CollisionEvent collisionEvent =
                ref CreateTriggerCollision2DEvent(
                    collider2D
                );
            collisionEvent.type = CollisionEventType.Enter;
        }

        public void OnTriggerStay2D(Collider2D collider2D) {
            ref CollisionEvent collisionEvent =
                ref CreateTriggerCollision2DEvent(
                    collider2D
                );
            collisionEvent.type = CollisionEventType.Stay;
        }

        public void OnTriggerExit2D(Collider2D collider2D) {
            ref CollisionEvent collisionEvent =
                ref CreateTriggerCollision2DEvent(
                    collider2D
                );
            collisionEvent.type = CollisionEventType.Exit;
        }

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

        private ref CollisionEvent CreateCollision2DEvent(
            Collision2D collision2D
        ) {
            ref CollisionEvent collisionEvent = ref CreateRawCollisionEvent();
            collisionEvent.collision2D = collision2D;
            collisionEvent.isTrigger = false;
            
            ColliderBridge otherColliderBridge =
                collision2D.gameObject.GetComponent<ColliderBridge>();
            collisionEvent.guestEntity =
                otherColliderBridge != null ? otherColliderBridge.Entity : null;

            return ref collisionEvent;
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

        private ref CollisionEvent CreateTriggerCollision2DEvent(
            Collider2D collider2D
        ) {
            ref CollisionEvent collisionEvent = ref CreateRawCollisionEvent();

            collisionEvent.collider2D = collider2D;
            collisionEvent.isTrigger = true;

            ColliderBridge otherColliderBridge =
                collider2D.gameObject.GetComponent<ColliderBridge>();
            collisionEvent.guestEntity =
                otherColliderBridge != null ? otherColliderBridge.Entity : null;

            return ref collisionEvent;
        }

        private ref CollisionEvent CreateRawCollisionEvent() {
            ref var collisionEvent =
                ref EventUtils.Create<CollisionEvent>(World);
            collisionEvent.hostEntity = entity;
            collisionEvent.hostCollider = hostCollider;

            return ref collisionEvent;
        }
    }
}
