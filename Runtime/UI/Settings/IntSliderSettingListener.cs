using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Config.Specs;
using Slimebones.ECSCore.React;
using Slimebones.ECSCore.Utils;
using Slimebones.ECSCore.Utils.Parsing;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Slimebones.ECSCore.UI.Settings
{
    public class IntSliderSettingListener: IListener
    {
        public TextMeshProUGUI displayText;

        private string key;
        private Slider sliderUnity;

        public void Subscribe(Entity e, World world)
        {
            key = e.GetComponent<Key.Key>().key;
            var go = GameObjectUtils.GetUnityOrError(e);

            sliderUnity = go.GetComponent<Slider>();
            sliderUnity.onValueChanged.AddListener(Call);
            var parseOpts = new IntParseOpts(
                min: (int)sliderUnity.minValue,
                max: (int)sliderUnity.maxValue
            );

            string valueStr = Config.Config.Get(key);
            sliderUnity.value = ParsingUtils.Parse(;

            Config.Config.Set(key, precisionValue.ToString());
            SetDisplayText(precisionValue.ToString());
        }

        public void Unsubscribe()
        {
            sliderUnity.onValueChanged.RemoveListener(Call);
        }

        private void Call(float value)
        {
            float precisionValue = ApplyPrecision(value);
            Config.Config.Set(key, precisionValue.ToString());
            SetDisplayText(precisionValue.ToString());
        }

        private void SetDisplayText(string text)
        {
            if (displayText != null)
            {
                displayText.text = text;
            }
        }
    }
}