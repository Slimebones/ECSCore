using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.Request;
using Slimebones.ECSCore.Graphics;
using Slimebones.ECSCore.Logging;
using System;

namespace Slimebones.ECSCore.Config.Specs
{
    public class VsyncConfigSpec: IConfigSpec
    {
        public string Key => "slimebones.ecscore.config-spec.vsync";

        public string DefaultValueStr => "0";

        private World world;
        public World World
        {
            get => world;
            set => world = value;
        }

        public bool OnChange(string value, out string newValue)
        {
            newValue = "";
            bool flag; 
            try
            {
                flag = Parse(value);
            }
            catch
            {
                Log.Error(
                    "cannot parse vsync {0} => use default {1}",
                    value,
                    DefaultValueStr
                );
                newValue = DefaultValueStr;
                return true;
            }

            SendStateReq(flag);
            return false;
        }

        public Action<string> OnSettingInit(Entity e)
        {
            return ConfigSpecUtils.OnBasicToggleSettingInit(e);
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

        private void SendStateReq(bool flag)
        {
            ref var req =
                ref RequestUtils.Create<SetGraphicsRequest>(
                    1
                );
            req.isVsyncEnabled = flag;
        }
    }
}