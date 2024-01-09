using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using Slimebones.ECSCore.Object;

namespace Slimebones.ECSCore.Mouse
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(MouseSystem))]
    public sealed class MouseSystem: UpdateSystem {
        private Filter interactables;

        public override void OnAwake() {
            interactables = World.Filter.With<MouseInteractable>().Build();

            foreach (var e in interactables) {
                ref MouseInteractable interactable =
                    ref e.GetComponent<MouseInteractable>();

                ref Object.Go goECS = ref GoUtils.Get(e);

                interactable.bridge =
                    goECS.value.AddComponent<MouseBridge>();

                interactable.bridge.World = World;
                interactable.bridge.Entity = e;
            }
        }

        public override void OnUpdate(float deltaTime) {
        }
    }
}