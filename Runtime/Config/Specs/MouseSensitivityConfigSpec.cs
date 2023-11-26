using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Input;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.React;
using Slimebones.ECSCore.UI.Settings;
using Slimebones.ECSCore.Utils.Parsing;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Slimebones.ECSCore.Config.Specs
{
    public class MouseSensitivityConfigSpec: IConfigSpec, IParser<float>
    {
        public string Key => "mouse-sensitivity";
        public string DefaultValueStr => "1.0";
        public static readonly float MinValue = 0f;
        public static readonly float MaxValue = 5f;

        private World world;
        public World World
        {
            get => world;
            set => world = value;
        }

        private static readonly int Precision = 1;

        public bool OnChange(string value, out string newValue)
        {
            newValue = "";
            float sensitivity; 
            try
            {
                sensitivity = Parse(value);
            }
            catch
            {
                Log.Error(
                    "cannot parse sensitivity {0} => use default {1}",
                    value,
                    DefaultValueStr
                );
                newValue = DefaultValueStr;
                return true;
            }

            CursorUtils.mouseSensitivityX = sensitivity;
            CursorUtils.mouseSensitivityY = sensitivity;
            return false;
        }

        private float Parse(string value)
        {
            float res = ApplyPrecision(float.Parse(value));

            if (res < MinValue || res > MaxValue)
            {
                throw new Exception();
            }

            return res;
        }

        private float ApplyPrecision(float value)
        {
            return (float)Math.Round(
                value,
                Precision
            );
        }

        public float ParseIn(string valueStr)
        {
            throw new NotImplementedException();
        }

        public string ParseOut(float value)
        {
            return ApplyPrecision(value).ToString();
        }

        public Action<string> OnSettingInit(Entity e)
        {
            var go = GameObjectUtils.GetUnityOrError(e);
            Slider sliderUnity = go.GetComponent<Slider>();

            TextMeshProUGUI displayText =
                ReactUtils.GetListenerFromReactEntity<SettingListener>(e)
                    .displayText;

            sliderUnity.minValue = MinValue;
            sliderUnity.maxValue = MaxValue;

            return (string value) =>
            {
                float parsed = Parse(value);
                sliderUnity.SetValueWithoutNotify(parsed);
                SetDisplayText(parsed.ToString(), displayText);
            };
        }
        
        private void SetDisplayText(string text, TextMeshProUGUI displayText)
        {
            if (displayText != null)
            {
                displayText.text = text;
            }
        }
    }
}