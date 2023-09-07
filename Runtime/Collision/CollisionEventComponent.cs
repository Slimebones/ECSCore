using Scellecs.Morpeh;
using UnityEngine;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace Slimebones.ECSCore.Collision {
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
    /// </remarks>
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CollisionEvent : IComponent {
        public CollisionEventType type;
        public UnityEngine.Collision collision;
        public UnityEngine.Collider collider;
        public Entity hostEntity;
        public Entity guestEntity;
        public bool isTrigger;
    }
}