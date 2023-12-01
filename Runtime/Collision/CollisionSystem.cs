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
    public sealed class CollisionSystem: ISystem {
        private Filter collidersF;

        public World World
        {
            get;
            set;
        }

        public void OnAwake() {
        }

        public void OnUpdate(float deltaTime)
        {
            collidersF = World.Filter.With<ColliderBridgeHost>().Build();

            foreach (Entity e in collidersF) {
                ref ColliderBridgeHost bridgeHost =
                    ref e.GetComponent<ColliderBridgeHost>();

                if (bridgeHost.isInitialized)
                {
                    continue;
                }

                if (bridgeHost.bridgeTypes.Length == 0)
                {
                    // add base collider bridge for components without any
                    // types defined - in order to register them as guests
                    // correctly on collisions
                    e.AddBridge<BaseColliderBridge>(World);
                }

                foreach (var colliderType in bridgeHost.bridgeTypes)
                {
                    BaseColliderBridge bridge;

                    switch (colliderType)
                    {
                        case ColliderBridgeType.CollisionStay:
                            bridge =
                                e
                                .AddBridge<CollisionStayColliderBridge>(
                                    World
                                );
                            break;
                        case ColliderBridgeType.CollisionExit:
                            bridge =
                                e
                                .AddBridge<CollisionExitColliderBridge>(
                                    World
                                );
                            break;
                        case ColliderBridgeType.TriggerStay:
                            bridge =
                                e
                                .AddBridge<TriggerStayColliderBridge>(
                                    World
                                );
                            break;
                        case ColliderBridgeType.TriggerExit:
                            bridge =
                                e
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

                bridgeHost.isInitialized = true;
            }
        }
 
        public void Dispose()
        {
        }
    }
}