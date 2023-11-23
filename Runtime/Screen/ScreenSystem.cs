using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using UnityEngine;

namespace Slimebones.ECSCore.Screen
{
    public class ScreenSystem: ISystem
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
                World.Filter.With<SetScreenResolutionRequest>().Build();

            lastResolution = UnityEngine.Screen.currentResolution;
            lastMode = UnityEngine.Screen.fullScreenMode;
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var reqE in setResolutionReqF)
            {
                if (!RequestComponentUtils.RegisterCall(reqE))
                {
                    continue;
                }

                ref var reqC =
                    ref reqE.GetComponent<SetScreenResolutionRequest>();

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

                UnityEngine.Screen.SetResolution(
                    finalResolution.width,
                    finalResolution.height,
                    finalMode,
                    finalResolution.refreshRate
                );
            }

            // update data only after all requests handled
            UpdateLastData();
        }

        public void Dispose()
        {
        }

        private void UpdateLastData()
        {
            lastResolution = UnityEngine.Screen.currentResolution;
            lastMode = UnityEngine.Screen.fullScreenMode;
        }
    }
}
