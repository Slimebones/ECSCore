using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Graphics;
using Slimebones.ECSCore.Input;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils;
using System;

namespace Slimebones.ECSCore.Config.Specs
{
    public class VsyncConfigSpec: IConfigSpec
    {
        public string Key => "vsync";

        public string DefaultValue => "0";

        public void OnChange(string value, World world)
        {
            bool flag; 
            try
            {
                flag = Parse(value);
            }
            catch
            {
                Log.Error(
                    "cannot parse vsync {0}, use default {1}",
                    value,
                    DefaultValue
                );
                Config.Set(Key, DefaultValue);
                return;
            }


            ref var req =
                ref RequestComponentUtils.Create<SetGraphicsRequest>(
                    1,
                    world
                );
            req.isVsyncEnabled = flag;
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