using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Mouse {
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MouseCleanupSystem))]
    public sealed class MouseCleanupSystem : CleanupSystem {
        private Filter toCleanF;

        public override void OnAwake() {
            toCleanF = World.Filter.With<MouseEvent>();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (Entity entity in toCleanF) {
                World.RemoveEntity(entity);
            }
        }
    }
}