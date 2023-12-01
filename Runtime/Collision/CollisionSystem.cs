using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.GO;
using Slimebones.ECSCore.Collision.Bridges;
using Slimebones.ECSCore.Base.Bridge;
using Slimebones.ECSCore.Utils;
using System.Collections.Generic;
using System;

namespace Slimebones.ECSCore.Collision
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CollisionSystem))]
    public sealed class CollisionSystem: UpdateSystem {
        private Filter colliders;

        public override void OnAwake() {
            colliders = World.Filter.With<ColliderBridgeHost>().Build();

            foreach (Entity entity in colliders) {
                ref ColliderBridgeHost collider = ref entity.GetComponent<ColliderBridgeHost>();

                foreach (var colliderType in collider.bridgeTypes)
                {
                    BaseColliderBridge bridge;

                    switch (colliderType)
                    {
                        case ColliderBridgeType.CollisionEnter:
                            bridge =
                                entity
                                .AddBridge<CollisionEnterColliderBridge>(
                                    World
                                );
                            break;
                        case ColliderBridgeType.CollisionStay:
                            bridge =
                                entity
                                .AddBridge<CollisionStayColliderBridge>(
                                    World
                                );
                            break;
                        case ColliderBridgeType.CollisionExit:
                            bridge =
                                entity
                                .AddBridge<CollisionExitColliderBridge>(
                                    World
                                );
                            break;
                        case ColliderBridgeType.TriggerEnter:
                            bridge =
                                entity
                                .AddBridge<TriggerEnterColliderBridge>(
                                    World
                                );
                            break;
                        case ColliderBridgeType.TriggerStay:
                            bridge =
                                entity
                                .AddBridge<TriggerStayColliderBridge>(
                                    World
                                );
                            break;
                        case ColliderBridgeType.TriggerExit:
                            bridge =
                                entity
                                .AddBridge<TriggerExitColliderBridge>(
                                    World
                                );
                            break;
                        default:
                            throw new UnsupportedException(
                                string.Format(
                                    "collider type {0}",
                                    colliderType
                                )
                            );
                    }
                }
            }
        }

        public override void OnUpdate(float deltaTime) {
        }
    }
}