using Scellecs.Morpeh;
using Slimebones.ECSCore.React;
using Slimebones.ECSCore.Utils.Parsing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Slimebones.ECSCore.Config.InternalSettingListeners
{
    internal class IntSliderSettingKeyedGOListener: IKeyedGOListener
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
            int valueInt = (int)value;
            IConfigSpec spec = Config.GetSpec(key);
            string finalValue;
            if (!ParsingUtils.TryParseOut(spec, valueInt, out finalValue))
            {
                finalValue = valueInt.ToString();
            }
            Config.Set(key, finalValue);
        }
    }
}
