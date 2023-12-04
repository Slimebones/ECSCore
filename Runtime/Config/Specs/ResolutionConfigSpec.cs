using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.GO;
using Slimebones.ECSCore.Base.Request;
using Slimebones.ECSCore.Graphics;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils;
using Slimebones.ECSCore.Utils.Parsing;
using System;
using TMPro;
using UnityEngine;
using static TMPro.TMP_Dropdown;

namespace Slimebones.ECSCore.Config.Specs
{
    public class ResolutionConfigSpec: IConfigSpec, IParser<string>
    {
        public string Key => "slimebones.ecscore.config-spec.resolution";
        public string DefaultValueStr => "1920x1080@auto";

        private World world;
        public World World
        {
            get => world;
            set => world = value;
        }

        public bool OnChange(string value, out string newValue)
        {
            newValue = "";

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
                newValue = DefaultValueStr;
                return true;
            }

            SendSetResolutionRequest(resolution);
            return false;
        }

        private void SendSetResolutionRequest(Resolution resolution)
        {
            ref var req =
                ref RequestUtils.Create<SetGraphicsRequest>();
            req.resolution = resolution;
        }

        public Action<string> OnSettingInit(Entity e)
        {
            var go = GOUtils.GetUnity(e);
            TMP_Dropdown dropdownUnity = go.GetComponent<TMP_Dropdown>();
            InitSettingOptions(dropdownUnity);
            return (string value) =>
                SelectSettingOptionFromConfig(value, dropdownUnity);
        }

        private void InitSettingOptions(TMP_Dropdown dropdownUnity)
        {
            // clear demo options
            dropdownUnity.options.Clear();

            foreach (var resolution in Screen.resolutions)
            {
                OptionData optionData = new OptionData(resolution.ToString());
                dropdownUnity.options.Add(optionData);
            }
        }

        private void SelectSettingOptionFromConfig(
            string value,
            TMP_Dropdown dropdownUnity
        )
        {
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
                    dropdownUnity.SetValueWithoutNotify(i);
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

        public string ParseIn(string valueStr)
        {
            throw new NotImplementedException();
        }

        public string ParseOut(string resolutionStr)
        {
            return resolutionStr.Replace(" ", "").Replace("Hz", "");
        }
    }
}