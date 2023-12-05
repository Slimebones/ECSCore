using Scellecs.Morpeh;

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
                ContactActionData[] actionData =
                    evtc.hostEntity.GetComponent<ContactActions>().actions;
                foreach (var data in actionData)
                {
                    if (
                        // on null collider every collision an action is
                        // called
                        data.collider == null
                        || data.collider == evtc.unityGuestCollider
                    )
                    {
                        data.actionSpec.Call(
                            evtc.hostEntity,
                            evtc.guestEntity
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