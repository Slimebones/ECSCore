using Scellecs.Morpeh;
using Slimebones.ECSCore.Input;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils;
using System;

namespace Slimebones.ECSCore.Config.Specs
{
    public class MouseSensitivityConfigSpec: IConfigSpec
    {
        public static readonly int Precision = 1;

        public string Key => "mouse-sensitivity";

        public string DefaultValueStr => "1.0";

        public void OnChange(string value, World world)
        {
            float sensitivity; 
            try
            {
                sensitivity = Parse(value);
            }
            catch
            {
                Log.Error(
                    "cannot parse sensitivity {0}, use default {1}",
                    value,
                    DefaultValueStr
                );
                Config.Set(Key, DefaultValueStr);
                return;
            }

            CursorUtils.mouseSensitivityX = sensitivity;
            CursorUtils.mouseSensitivityY = sensitivity;
        }

        private float Parse(string value)
        {
            return (float)Math.Round(
                float.Parse(value),
                Precision
            );
        }

    }
}