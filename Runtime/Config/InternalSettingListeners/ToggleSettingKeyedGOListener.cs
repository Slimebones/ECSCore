using Scellecs.Morpeh;
using Slimebones.ECSCore.React;
using Slimebones.ECSCore.Utils.Parsing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Slimebones.ECSCore.Config.InternalSettingListeners
{
    internal class ToggleSettingKeyedGOListener: IKeyedGOListener
    {
        private string key;
        private Toggle toggleUnity;

        public void Subscribe(string key, GameObject go, World world)
        {
            this.key = key;
            toggleUnity = go.GetComponent<Toggle>();
            toggleUnity.onValueChanged.AddListener(Call);
        }

        public void Unsubscribe()
        {
            toggleUnity.onValueChanged.RemoveListener(Call);
        }

        private void Call(bool flag)
        {
            IConfigSpec spec = Config.GetSpec(key);
            string finalValue;
            if (!ParsingUtils.TryParseOut(spec, flag, out finalValue))
            {
                finalValue = flag ? "1" : "0";
            }
            Config.Set(key, finalValue);
        }
    }
}
