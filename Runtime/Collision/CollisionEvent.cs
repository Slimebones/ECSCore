using Scellecs.Morpeh;
using UnityEngine;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using Slimebones.ECSCore.Event;

namespace Slimebones.ECSCore.Collision
{
    /// <summary>
    /// Event of collision of two objects.
    /// </summary>
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CollisionEvent : IEventComponent {
        public CollisionEventType type;
        public Collider unityHostCollider;
        public Collider unityGuestCollider;
        /// <summary>
        /// Set only for non-trigger events.
        /// </summary>
        public UnityEngine.Collision unityCollision;
        public Entity hostEntity;
        public Entity guestEntity;
        public bool isTrigger;
    }
}