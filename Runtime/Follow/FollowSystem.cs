using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;

namespace Slimebones.ECSCore.Follow
{
    public class FollowSystem: ISystem
    {
        private Filter f;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            f = World.Filter.With<Follow>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var e in f)
            {
                ref var c = ref e.GetComponent<Follow>();
                var cgo = GameObjectUtils.GetUnityOrError(e);

                cgo.transform.position =
                    c.targetGO.transform.position + c.offset;
            }
        }

        public void Dispose()
        {
        }
    }
}
