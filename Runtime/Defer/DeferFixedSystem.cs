using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.CoreSystem;
using Slimebones.ECSCore.Base.Request;
using Slimebones.ECSCore.Utils;
using Unity.VisualScripting.YamlDotNet.Serialization;

namespace Slimebones.ECSCore.Defer
{
    public class DeferFixedSystem: IFixedSystem
    {
        private Filter deferRequestF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            deferRequestF = World.Filter.With<DeferRequest>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            SystemUtils.IterateEntities(deferRequestF, OnEntity);
        }

        public void Dispose()
        {
        }

        private void OnEntity(Entity e)
        {
            if (!RequestUtils.RegisterCall(e))
            {
                return;
            }
            ref var c = ref e.GetComponent<DeferRequest>();
            if (c.launchOnUpdateType == UpdateType.FixedUpdate)
            {
                c.action();
            }
        }
    }
}

