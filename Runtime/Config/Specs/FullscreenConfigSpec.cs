using Scellecs.Morpeh;
using Slimebones.ECSCore.Graphics;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Request;
using System;
using UnityEngine;

namespace Slimebones.ECSCore.Config.Specs
{
    public class FullscreenConfigSpec: IConfigSpec
    {
        public string Key =>
            "slimebones.ecscore.config-spec.fullscreen";
        public string DefaultValueStr => "1";

        private World world;
        public World World
        {
            get => world;
            set => world = value;
        }

        public bool OnChange(string value, out string newValue)
        {
            newValue = "";
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
                    DefaultValueStr
                );
                newValue = DefaultValueStr;
                return true;
            }

            SendFullscreenModeReq(
                isFullscreen
                    ? FullScreenMode.FullScreenWindow
                    : FullScreenMode.Windowed
            );
            return false;
        }

        public Action<string> OnSettingInit(Entity e)
        {
            return ConfigSpecUtils.OnBasicToggleSettingInit(e);
        }

        private void SendFullscreenModeReq(FullScreenMode mode)
        {
            ref var req =
                ref RequestUtils.Create<SetGraphicsRequest>();
            req.fullScreenMode = mode;
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