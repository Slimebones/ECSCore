using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Input;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Screen;
using Slimebones.ECSCore.Utils;
using System;

namespace Slimebones.ECSCore.Config.Specs
{
    public class FullscreenConfigSpec: IConfigSpec
    {
        public string Key => "fullscreen";

        public string DefaultValue => "1";

        public void OnChange(string value, World world)
        {
            bool isFullscreen; 
            try
            {
                isFullscreen = Parse(value);
            }
            catch
            {
                Log.Error(
                    "cannot parse fullscreen {0}, use default {1}",
                    value,
                    DefaultValue
                );
                Config.Set(Key, DefaultValue);
                return;
            }


            ref var req =
                ref RequestComponentUtils.Create<SetScreenResolutionRequest>(
                    1,
                    world
                );
            req.fullScreenMode =
                isFullscreen
                ? UnityEngine.FullScreenMode.FullScreenWindow
                : UnityEngine.FullScreenMode.Windowed;
        }

        private bool Parse(string value)
        {
            if (value == "1")
            {
                return true;
            }

            if (value == "0")
            {
                return false;
            }

            throw new Exception();
        }

    }
}