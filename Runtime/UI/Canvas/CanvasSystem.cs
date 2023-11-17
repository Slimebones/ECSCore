using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using UnityEngine;

namespace Slimebones.ECSCore.UI.Canvas
{
    public class CanvasSystem: ISystem
    {
        private Filter requestF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            requestF = World.Filter.With<MoveToCanvasRequest>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var requestE in requestF)
            {
                if (!RequestComponentUtils.RegisterCall(requestE))
                {
                    continue;
                }

                ref var requestC =
                    ref requestE.GetComponent<MoveToCanvasRequest>();

                var targetGO = GameObjectUtils.GetUnityOrError(
                    requestC.targetE
                );
                var canvasGO = GameObjectUtils.GetUnityOrError(
                    requestC.canvasE
                );

                targetGO.transform.SetParent(canvasGO.transform, false);
            }
        }

        public void Dispose()
        {
        }
    }
}
