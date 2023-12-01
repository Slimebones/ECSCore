using Scellecs.Morpeh;
using UnityEngine;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using Slimebones.ECSCore.Base.Event;

namespace Slimebones.ECSCore.Collision
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CollisionEventComponent : MonoProvider<CollisionEvent> {
    }

    /// <summary>
    /// Event of collision of two objects.
    /// </summary>
    /// <remarks>
    /// Can be Trigger or not. `collider` field is set on Trigger events,
    /// `collision` field is set otherwise.
    ///
    /// For 2D alternative fields are used, such as `collider2D` and
    /// `collision2D`.
    /// </remarks>
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CollisionEvent : IEventComponent {
        public CollisionEventType type;
        public UnityEngine.Collider unityHostCollider;
        public UnityEngine.Collision unityCollision;
        public UnityEngine.Collider guestCollider;
        public Entity hostEntity;
        public Entity guestEntity;
        public bool isTrigger;
    }
}