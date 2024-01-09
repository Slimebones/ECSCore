using Scellecs.Morpeh;
using Slimebones.ECSCore.Event;
using UnityEngine;

namespace Slimebones.ECSCore.Collision
{
    public static class CollisionUtils {
        public static readonly FilterBuilder CollisionEventFB =
            EventUtils.FB.With<CollisionEvent>();
        public static readonly Filter CollisionEventF =
            CollisionEventFB.Build();

        public delegate void NoFlagCollisionAction(
            ref CollisionEvent evt
        );
        public delegate void CollisionAction(
            bool isComponentHost,
            ref CollisionEvent evt
        );
        public delegate void CollisionAction<T1>(
            bool isComponent1Host,
            ref CollisionEvent evt,
            ref T1 component1
        );
        public delegate void CollisionAction<T1, T2>(
            bool isComponent1Host,
            ref CollisionEvent evt,
            ref T1 component1,
            ref T2 component2
        );

        public static void ExecuteForEachCollisionEvent<
            TComponent1,
            TComponent2
        >(
            CollisionAction<
                TComponent1,
                TComponent2
            > action
        )
            where TComponent1: struct, IComponent
            where TComponent2: struct, IComponent
        {
            NoFlagCollisionAction actionWrapper =
                (
                    ref CollisionEvent collisionEvent
                ) =>
                {
                    var hostE = collisionEvent.hostEntity;
                    var guestE = collisionEvent.guestEntity;

                    if (!hostE.IsNullOrDisposed() && hostE.Has<TComponent2>())
                    {
                        ref TComponent2 c2 =
                            ref hostE.GetComponent<TComponent2>();

                        bool isOtherComponentExist;
                        ref TComponent1 c1 =
                            ref guestE.GetComponent<TComponent1>(
                                out isOtherComponentExist
                            );
                        if (!isOtherComponentExist)
                        {
                            return;
                        }

                        action(false, ref collisionEvent, ref c1, ref c2);
                        return;
                    }

                    if (
                        !guestE.IsNullOrDisposed()
                        && guestE.Has<TComponent2>()
                    )
                    {
                        ref TComponent2 c2 =
                            ref guestE.GetComponent<TComponent2>();

                        bool isOtherComponentExist;
                        ref TComponent1 c1 =
                            ref hostE.GetComponent<TComponent1>(
                                out isOtherComponentExist
                            );
                        if (!isOtherComponentExist)
                        {
                            return;
                        }

                        action(true, ref collisionEvent, ref c1, ref c2);
                        return;
                    }
                };
            ExecuteForEachCollisionEvent(
                actionWrapper
            );
        }

        public static void ExecuteForEachCollisionEvent<
            TComponent1
        >(
            CollisionAction<TComponent1> action
        )
            where TComponent1: struct, IComponent
        {
            NoFlagCollisionAction actionWrapper =
                (ref CollisionEvent collisionEvent) =>
                {
                    var hostE = collisionEvent.hostEntity;
                    var guestE = collisionEvent.guestEntity;

                    if (!hostE.IsNullOrDisposed() && hostE.Has<TComponent1>())
                    {
                        ref TComponent1 c =
                            ref hostE.GetComponent<TComponent1>();
                        action(true, ref collisionEvent, ref c);
                        return;
                    }
                    if (
                        !guestE.IsNullOrDisposed()
                        && guestE.Has<TComponent1>()
                    )
                    {
                        ref TComponent1 c =
                            ref guestE.GetComponent<TComponent1>();
                        action(false, ref collisionEvent, ref c);
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
            NoFlagCollisionAction action
        ) {
            foreach (Entity collisionEventEntity in CollisionEventF) {
                ref CollisionEvent collisionEvent =
                    ref collisionEventEntity.GetComponent<CollisionEvent>();

                action(ref collisionEvent);
            }
        }
    }
}