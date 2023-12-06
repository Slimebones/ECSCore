using Scellecs.Morpeh;
using Slimebones.ECSCore.Event;
using System;
using System.Linq;
using static Slimebones.ECSCore.Utils.Delegates;

namespace Slimebones.ECSCore.Collision
{
    public static class CollisionUtils {
        public static void ExecuteForEachCollisionEventAnyComponent<
            TComponent1,
            TComponent2
        >(
            ActionRef<CollisionEvent, TComponent1, TComponent2> action
        )
            where TComponent1: struct, IComponent
            where TComponent2: struct, IComponent
        {
            ActionRef<CollisionEvent, TComponent1> actionWrapper =
                (ref CollisionEvent collisionEvent, ref TComponent1 c1) =>
                {
                    var hostE = collisionEvent.hostEntity;
                    var guestE = collisionEvent.guestEntity;

                    if (!hostE.IsNullOrDisposed() && hostE.Has<TComponent2>())
                    {
                        ref TComponent2 c2 =
                            ref hostE.GetComponent<TComponent2>();
                        action(ref collisionEvent, ref c1, ref c2);
                        return;
                    }
                    if (
                        !guestE.IsNullOrDisposed()
                        && guestE.Has<TComponent2>()
                    )
                    {
                        ref TComponent2 c2 =
                            ref guestE.GetComponent<TComponent2>();
                        action(ref collisionEvent, ref c1, ref c2);
                        return;
                    }
                };
            ExecuteForEachCollisionEventAnyComponent(
                actionWrapper
            );
        }

        public static void ExecuteForEachCollisionEventAnyComponent<
            TComponent1
        >(
            ActionRef<CollisionEvent, TComponent1> action
        )
            where TComponent1: struct, IComponent
        {
            ActionRef<CollisionEvent> actionWrapper =
                (ref CollisionEvent collisionEvent) =>
                {
                    var hostE = collisionEvent.hostEntity;
                    var guestE = collisionEvent.guestEntity;

                    if (!hostE.IsNullOrDisposed() && hostE.Has<TComponent1>())
                    {
                        ref TComponent1 c =
                            ref hostE.GetComponent<TComponent1>();
                        action(ref collisionEvent, ref c);
                        return;
                    }
                    if (
                        !guestE.IsNullOrDisposed()
                        && guestE.Has<TComponent1>()
                    )
                    {
                        ref TComponent1 c =
                            ref guestE.GetComponent<TComponent1>();
                        action(ref collisionEvent, ref c);
                        return;
                    }
                };
            ExecuteForEachCollisionEvent(actionWrapper);
        }

        /// <summary>
        /// Executes action for each collision event.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="world"></param>
        /// <param name="allowedTypes"></param>
        public static void ExecuteForEachCollisionEvent(
            ActionRef<CollisionEvent> action,
            CollisionEventType[] allowedTypes = null
        ) {
            Filter collisionEventFilter =
                EventUtils.FB.With<CollisionEvent>().Build();

            foreach (Entity collisionEventEntity in collisionEventFilter) {
                ref CollisionEvent collisionEvent =
                    ref collisionEventEntity.GetComponent<CollisionEvent>();

                if (!isAllowedType(collisionEvent.type, allowedTypes)) {
                    return; 
                }

                action(ref collisionEvent);
            }
        }

        /// <summary>
        /// Executes action for each collision event where HostComponent is
        /// attached to host collider entity.
        /// </summary>
        /// <typeparam name="HostComponent"></typeparam>
        /// <param name="action"></param>
        /// <param name="world"></param>
        /// <param name="allowedTypes"></param>
        public static void ExecuteForEachCollisionEvent<HostComponent>(
            ActionRef<CollisionEvent, HostComponent> action,
            CollisionEventType[] allowedTypes = null
        ) where HostComponent : struct, IComponent {
            // create action wrapper for less detailed overload of this method
            ActionRef<CollisionEvent> actionWrapper =
                (ref CollisionEvent collisionEvent) => {
                    Entity hostEntity = collisionEvent.hostEntity;
                    if (hostEntity.IsNullOrDisposed())
                    {
                        return;
                    }
                    ref HostComponent hostComponent =
                        ref hostEntity.GetComponent<HostComponent>(
                            out bool isTargetHostComponent
                        );
                    if (isTargetHostComponent) {
                        action(ref collisionEvent, ref hostComponent);
                    }
                };
            ExecuteForEachCollisionEvent(actionWrapper, allowedTypes);
        }

        /// <summary>
        /// Executes action for each collision event where HostComponent and
        /// GuestComponent are attached to host/guest entities.
        /// </summary>
        /// <typeparam name="HostComponent"></typeparam>
        /// <typeparam name="GuestComponent"></typeparam>
        /// <param name="action"></param>
        /// <param name="world"></param>
        /// <param name="allowedTypes"></param>
        public static void ExecuteForEachCollisionEvent<
            HostComponent,
            GuestComponent
        >(
            ActionRef<CollisionEvent, HostComponent, GuestComponent> action,
            CollisionEventType[] allowedTypes = null
        )
            where HostComponent: struct, IComponent
            where GuestComponent: struct, IComponent
        {

            // create action wrapper for less detailed overload of this method
            ActionRef<CollisionEvent, HostComponent> actionWrapper =
                (
                    ref CollisionEvent collisionEvent,
                    ref HostComponent hostComponent
                ) => {
                    Entity guestEntity = collisionEvent.guestEntity;

                    if (guestEntity.IsNullOrDisposed()) {
                        return; 
                    }

                    ref GuestComponent guestComponent =
                        ref guestEntity.GetComponent<GuestComponent>(
                            out bool isTargetGuestComponent
                        );

                    if (isTargetGuestComponent) {
                        action(
                            ref collisionEvent,
                            ref hostComponent,
                            ref guestComponent
                        );
                    }
                };

            ExecuteForEachCollisionEvent(
                actionWrapper, allowedTypes
            );
        }

        private static bool isAllowedType(
            CollisionEventType type,
            CollisionEventType[] allowedTypes
        ) {
            if (allowedTypes == null) {
                return true;
            }
            return allowedTypes.Contains(type);
        }
    }
}