using Scellecs.Morpeh;
using Slimebones.ECSCore.React;
using TMPro;
using UnityEngine;

namespace Slimebones.ECSCore.Config.InternalSettingListeners
{
    internal class DropdownSettingListener: IKeyedGOListener
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
            Config.Set(key, dropdownUnity.options[index].text);
        }
    }
}
