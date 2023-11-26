using Scellecs.Morpeh;
using Slimebones.ECSCore.React;
using Slimebones.ECSCore.Utils.Parsing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Slimebones.ECSCore.Config.InternalSettingListeners
{
    internal class FloatSliderSettingKeyedGOListener: IKeyedGOListener
    {
        private string key;
        private Slider sliderUnity;

        public void Subscribe(string key, GameObject go, World world)
        {
            this.key = key;
            sliderUnity = go.GetComponent<Slider>();
            sliderUnity.onValueChanged.AddListener(Call);
        }

        public void Unsubscribe()
        {
            sliderUnity.onValueChanged.RemoveListener(Call);
        }

        private void Call(float value)
        {
            IConfigSpec spec = Config.GetSpec(key);
            string finalValue;
            if (!ParsingUtils.TryParseOut(spec, value, out finalValue))
            {
                finalValue = value.ToString();
            }
            Config.Set(key, finalValue);
        }
    }
}
