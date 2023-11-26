using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Graphics;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils;
using Slimebones.ECSCore.Utils.Parsing;
using System;
using UnityEngine;

namespace Slimebones.ECSCore.Config.Specs
{
    public class ResolutionConfigSpec: IConfigSpec
    {
        public string Key => "resolution";
        public string DefaultValueStr => "1920x1080@auto";

        private World world;
        public World World
        {
            get => world;
            set => world = value;
        }

        public bool OnInit(string value, out string newValue)
        {
            newValue = "";
            return true;
        }

        public bool OnChange(string value, out string newValue)
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

        public Action<string> OnSettingInit(Entity e, string lastValue)
        {
            throw new NotImplementedException();
        }


        //private void Call(int index)
        //{
        //    var option = dropdownUnity.options[index];

        //    Config.Config.Set(
        //        key,
        //        option.text.Replace(" ", "").Replace("Hz", "")
        //    );
        //}

        public void OnChange1(Resolution value)
        {
        }

        private void InitOptions()
        {
            // clear demo options
            dropdownUnity.options.Clear();

            foreach (var resolution in UnityEngine.Screen.resolutions)
            {
                OptionData optionData = new OptionData(resolution.ToString());
                dropdownUnity.options.Add(optionData);
            }
        }

        private void SelectFromConfig()
        {
            string value = Config.Config.Get(key);

            bool isAuto = value.EndsWith("@auto");

            for (int i = 0; i < dropdownUnity.options.Count; i++)
            {
                if (
                    (
                        // for auto refresh rate choose first matched
                        // resolution
                        isAuto
                        && dropdownUnity
                            .options[i]
                            .text
                            .Replace(" ", "")
                            .StartsWith(value.Replace("@auto", ""))
                    )
                    || dropdownUnity
                            .options[i]
                            .text
                            .Replace(" ", "")
                            .Replace("Hz", "")
                        == value
                )
                {
                    dropdownUnity.value = i;
                    return;
                }
            }

            // for unrecognized option set first available resolution option
            // use last option available since it's most often is better in
            // terms of screen quality
            Log.Error(
                "unrecognized dropdown value " + value + " => use last one"
            );
            int lastIndex = dropdownUnity.options.Count - 1;
            dropdownUnity.value = lastIndex;
            Call(lastIndex);
        }

        private Resolution Parse(string valueStr)
        {
            Resolution resolution = new Resolution();

            string[] resolutionParts = valueStr.Split("x");
            if (resolutionParts.Length != 2)
            {
                throw new LengthExpectException<char>(
                    valueStr.ToCharArray(), 2
                );
            }

            resolution.width = int.Parse(resolutionParts[0]);

            string[] refreshRateParts = resolutionParts[1].Split("@");
            if (refreshRateParts.Length != 2)
            {
                throw new LengthExpectException<char>(
                    valueStr.ToCharArray(), 2
                );
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