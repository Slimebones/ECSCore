using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Slimebones.ECSCore.Collision.Bridges;
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Slimebones.ECSCore.Collision
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class ColliderBridgeHostComponent : MonoProvider<ColliderBridgeHost> {
    }

    /// <summary>
    /// Able to collide with other Colliders.
    /// </summary>
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ColliderBridgeHost : IComponent {
        // on empty bridge types a base bridge host will be registered in
        // order to correctly register a guest entity on collisions
        public ColliderBridgeType[] bridgeTypes;

        [HideInInspector]
        public bool isInitialized;
    }
}
