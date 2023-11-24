using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Graphics;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils;
using System;
using UnityEngine;

namespace Slimebones.ECSCore.Config.Specs
{
    public class ResolutionConfigSpec: IConfigSpec
    {
        public string Key => "resolution";

        public string DefaultValueStr => "1920x1080@auto";

        public void OnChange(string value, World world)
        {
            Resolution resolution; 
            try
            {
                resolution = Parse(value);
            }
            catch
            {
                Log.Error(
                    "cannot parse resolution {0} => use default {1}",
                    value,
                    DefaultValueStr
                );
                Config.Set(Key, DefaultValueStr);
                return;
            }

            ref var req =
                ref RequestComponentUtils.Create<SetGraphicsRequest>(
                    1,
                    world
                );
            req.resolution = resolution;
        }

        private Resolution Parse(string value)
        {
            Resolution resolution = new Resolution();

            string[] resolutionParts = value.Split("x");
            if (resolutionParts.Length != 2)
            {
                throw new LengthExpectException<char>(value.ToCharArray(), 2);
            }

            resolution.width = int.Parse(resolutionParts[0]);

            string[] refreshRateParts = resolutionParts[1].Split("@");
            if (refreshRateParts.Length != 2)
            {
                throw new LengthExpectException<char>(value.ToCharArray(), 2);
            }

            resolution.height = int.Parse(refreshRateParts[0]);
            if (refreshRateParts[1] == "auto")
            {
                resolution.refreshRate = 0;
            }
            else
            {
                resolution.refreshRate = int.Parse(refreshRateParts[1]);
            }

            return resolution;
        }
    }
}