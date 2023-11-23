using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.React;
using Slimebones.ECSCore.Utils;
using TMPro;
using UnityEngine;

namespace Slimebones.ECSCore.UI.Settings
{
    public class DropdownSettingListener: IListener
    {
        private string key;
        private TMP_Dropdown dropdownUnity;

        public void Subscribe(Entity e, World world)
        {
            key = e.GetComponent<Key.Key>().key;
            var go = GameObjectUtils.GetUnityOrError(e);
            dropdownUnity = go.GetComponent<TMP_Dropdown>();
            dropdownUnity.onValueChanged.AddListener(Call);

            SelectFromConfig();
        }

        public void Unsubscribe()
        {
            dropdownUnity.onValueChanged.RemoveListener(Call);
        }

        private void SelectFromConfig()
        {
            string value = Config.Config.Get(key);

            for (int i = 0; i < dropdownUnity.options.Count; i++)
            {
                if (dropdownUnity.options[i].text == value)
                {
                    dropdownUnity.value = i;
                    return;
                }
            }

            throw new NotFoundException(
                string.Format(
                    "corresponding to config option with key {0} and value",
                    key
                ),
                value
            );
        }

        private void Call(int index)
        {
            var option = dropdownUnity.options[index];

            Config.Config.Set(key, option.text);
        }
    }
}