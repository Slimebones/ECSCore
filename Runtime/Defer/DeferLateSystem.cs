using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.CoreSystem;
using Slimebones.ECSCore.Base.Request;
using Slimebones.ECSCore.Utils;
using Unity.VisualScripting.YamlDotNet.Serialization;

namespace Slimebones.ECSCore.Defer
{
    public class DeferLateSystem: ILateSystem
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
            ref var c = ref e.GetComponent<DeferRequest>();
            if (c.launchOnUpdateType != UpdateType.LateUpdate)
            {
                return;
            }
            if (c.skippedFrames < c.framesToSkip)
            {
                c.skippedFrames++;
                return;
            }
            if (!RequestUtils.RegisterCall(e))
            {
                return;
            }
            c.action();
        }
    }
}

