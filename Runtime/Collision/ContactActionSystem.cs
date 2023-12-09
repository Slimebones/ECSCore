using Scellecs.Morpeh;
using System;
using System.Linq;

namespace Slimebones.ECSCore.Collision
{
    public sealed class ContactActionSystem: ISystem
    {
        private Filter collisionEventF;

        public World World
        {
            get;
            set;
        }

        public void OnAwake()
        {
            collisionEventF =
                World
                .Filter
                .With<CollisionEvent>()
                .With<ContactActionsEventPointer>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var evte in collisionEventF)
            {
                ref var evtc = ref evte.GetComponent<CollisionEvent>();

                if (
                    evtc.hostEntity.IsNullOrDisposed()
                    || !evtc.hostEntity.Has<ContactActions>()
                )
                {
                    continue;
                }
                ContactActionData[] actionData =
                    evtc.hostEntity.GetComponent<ContactActions>().actions;

                foreach (var data in actionData)
                {
                    if (
                        data.colliders == null
                        || data.colliders.Length == 0
                        || data.isExcept 
                            ? !data.colliders.Contains(
                                evtc.unityGuestCollider
                            )
                            : data.colliders.Contains(
                                evtc.unityGuestCollider
                            )
                    )
                    {
                        data.actionSpec.Call(
                            evtc.hostEntity,
                            evtc.guestEntity,
                            default,
                            default
                        );
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}