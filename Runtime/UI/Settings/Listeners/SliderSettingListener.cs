using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Slimebones.ECSCore.UI.Settings
{
    public class SliderSettingListener: IListener
    {
        private string key;
        private Slider sliderUnity;

        public void Subscribe(Entity e, World world)
        {
            key = e.GetComponent<DropdownSetting>().key;
            var go = GameObjectUtils.GetUnityOrError(e);
            sliderUnity = go.GetComponent<Slider>();
            sliderUnity.onValueChanged.AddListener(Call);
            sliderUnity.value = float.Parse(Config.Config.Get(key));
        }

        public void Unsubscribe()
        {
            sliderUnity.onValueChanged.RemoveListener(Call);
        }

        private void Call(float value)
        {
            Config.Config.Set(key, value.ToString());
        }
    }
}