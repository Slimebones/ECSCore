using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.Request;
using UnityEngine;

namespace Slimebones.ECSCore.Graphics
{
    public class GraphicsSystem: ISystem
    {
        private static Resolution lastResolution;
        private static FullScreenMode lastMode;

        private Filter setResolutionReqF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            setResolutionReqF =
               RequestUtils.FB.With<SetGraphicsRequest>().Build();

            lastResolution = Screen.currentResolution;
            lastMode = Screen.fullScreenMode;
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var reqE in setResolutionReqF)
            {
                ref var reqC =
                    ref reqE.GetComponent<SetGraphicsRequest>();

                Resolution finalResolution;
                FullScreenMode finalMode;
                if (reqC.resolution != null)
                {
                    finalResolution = (Resolution)reqC.resolution;
                }
                else
                {
                    finalResolution = lastResolution;
                }

                if (reqC.fullScreenMode != null)
                {
                    finalMode = (FullScreenMode)reqC.fullScreenMode;
                }
                else
                {
                    finalMode = lastMode;
                }

                Screen.SetResolution(
                    finalResolution.width,
                    finalResolution.height,
                    finalMode,
                    finalResolution.refreshRate
                );

                if (reqC.isVsyncEnabled != null)
                {
                    QualitySettings.vSyncCount =
                        reqC.isVsyncEnabled == true
                        ? 1
                        : 0;
                }

                lastResolution = finalResolution;
                lastMode = finalMode;

                RequestUtils.Complete(reqE);
            }

        }

        public void Dispose()
        {
        }
    }
}
