using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;

namespace Slimebones.ECSCore.Collision
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CollisionSystem))]
    public sealed class CollisionSystem: UpdateSystem {
        private Filter colliders;

        public override void OnAwake() {
            colliders = World.Filter.With<Collider>().Build();

            foreach (Entity entity in colliders) {
                ref Collider collider = ref entity.GetComponent<Collider>();

                // get collider's game object
                GameObject GOUnity = GameObjectUtils.GetUnityOrError(entity);

                // create a new MonoBehaviour as a ColliderBridge and attach it
                // to a game object and it's ECS component
                collider.bridge = GOUnity.AddComponent<ColliderBridge>();

                // also propagate ECS data to the bridge in order to emit
                // ECS events
                collider.bridge.World = World;
                collider.bridge.Entity = entity;
            }
        }

        public override void OnUpdate(float deltaTime) {
        }
    }
}