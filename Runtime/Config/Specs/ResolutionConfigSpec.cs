using Scellecs.Morpeh;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils;
using System;
using UnityEngine;

namespace Slimebones.ECSCore.Config.Specs
{
    public class ResolutionConfigSpec: IConfigSpec
    {
        public string Key => "resolution";

        public string DefaultValue => "1920x1080";

        public void OnChange(string value, World world)
        {
            int[] resolution; 
            try
            {
                resolution = ParseResolution(value);
            }
            catch
            {
                Log.Error(
                    "cannot parse resolution {0}, use default {1}",
                    value,
                    DefaultValue
                );
                Config.SetNoCallback(Key, DefaultValue);
                resolution = ParseResolution(DefaultValue);
            }

            // TODO(ryzhovalex):
            //      for now it is always a full screen, in the future make
            //      it via system accepting two (resolution and fullscreen and
            //      hz) settings
            Screen.SetResolution(resolution[0], resolution[1], true);
        }

        private int[] ParseResolution(string value)
        {
            string[] parts = value.Split("x");

            if (parts.Length == 2)
            {
                return new int[]
                {
                    int.Parse(parts[0]),
                    int.Parse(parts[1])
                };
            }

            throw new LengthExpectException<char>(value.ToCharArray(), 2);
        }
    }
}