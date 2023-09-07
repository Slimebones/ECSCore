using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using JetPants.ClumsyDelivery.Core.Mouse;

namespace JetPants.ClumsyDelivery.Core.Global {
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GlobalCleanupSystem))]
    public sealed class GlobalCleanupSystem : CleanupSystem {
        private Filter globalMouseEvents;

        public override void OnAwake() {
            globalMouseEvents = World.Filter.With<MouseEvent>();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (Entity entity in globalMouseEvents) {
                World.RemoveEntity(entity);
            }
        }
    }
}