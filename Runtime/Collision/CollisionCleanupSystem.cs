using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

namespace JetPants.ClumsyDelivery.Core.Collision {
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CollisionCleanupSystem))]
    public sealed class CollisionCleanupSystem : CleanupSystem {
        private Filter colliders;

        public override void OnAwake() {
            colliders = World.Filter.With<CollisionEvent>();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (Entity entity in colliders) {
                World.RemoveEntity(entity);
            }
        }
    }
}