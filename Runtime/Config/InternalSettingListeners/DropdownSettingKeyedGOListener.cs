using Scellecs.Morpeh;
using Slimebones.ECSCore.React;
using Slimebones.ECSCore.Utils.Parsing;
using TMPro;
using UnityEngine;

namespace Slimebones.ECSCore.Config.InternalSettingListeners
{
    internal class DropdownSettingKeyedGOListener: IKeyedGOListener
    {
        private string key;
        private TMP_Dropdown dropdownUnity;

        public void Subscribe(string key, GameObject go, World world)
        {
            this.key = key;
            dropdownUnity = go.GetComponent<TMP_Dropdown>();
            dropdownUnity.onValueChanged.AddListener(Call);
        }

        public void Unsubscribe()
        {
            dropdownUnity.onValueChanged.RemoveListener(Call);
        }

        private void Call(int index)
        {
            IConfigSpec spec = Config.GetSpec(key);
            string finalValue = dropdownUnity.options[index].text;
            string parsed;
            if (ParsingUtils.TryParseOut(spec, finalValue, out parsed))
            {
                finalValue = parsed;
            }
            Config.Set(key, finalValue);
        }
    }
}
