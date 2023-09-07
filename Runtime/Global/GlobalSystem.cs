using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

namespace Slimebones.ECSCore.Global {
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GlobalSystem))]
    public sealed class GlobalSystem: UpdateSystem {
        private Filter globalF;

        public override void OnAwake() {
            globalF = World.Filter.With<Global>();

            Entity globalE = globalF.First();
            ref Global _global = ref globalE.GetComponent<Global>();
            ref ECSGameObject gameObject = ref ECSGameObjectUtils.GetOrError(
                globalE
            );

            _global.bridge =
                gameObject.value.AddComponent<GlobalBridge>();
            _global.bridge.World = World;
            _global.bridge.Entity = globalE;
        }

        public override void OnUpdate(float deltaTime) {
        }
    }
}