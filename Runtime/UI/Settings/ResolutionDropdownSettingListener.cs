using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.React;
using Slimebones.ECSCore.Utils;
using TMPro;
using UnityEngine;
using static TMPro.TMP_Dropdown;

namespace Slimebones.ECSCore.UI.Settings
{
    public class ResolutionDropdownSettingListener: IListener
    {
        private string key;
        private TMP_Dropdown dropdownUnity;

        public void Subscribe(Entity e, World world)
        {
            key = e.GetComponent<Key.Key>().key;
            var go = GameObjectUtils.GetUnityOrError(e);
            dropdownUnity = go.GetComponent<TMP_Dropdown>();
            dropdownUnity.onValueChanged.AddListener(Call);

            InitOptions();
            SelectFromConfig();
        }

        public void Unsubscribe()
        {
            dropdownUnity.onValueChanged.RemoveListener(Call);
        }

        private void InitOptions()
        {
            // clear demo options
            dropdownUnity.options.Clear();

            foreach (var resolution in UnityEngine.Screen.resolutions)
            {
                OptionData optionData = new OptionData(resolution.ToString());
                dropdownUnity.options.Add(optionData);
            }
        }

        private void SelectFromConfig()
        {
            string value = Config.Config.Get(key);

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
                    dropdownUnity.value = i;
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
            Call(lastIndex);
        }

        private void Call(int index)
        {
            var option = dropdownUnity.options[index];

            Config.Config.Set(
                key,
                option.text.Replace(" ", "").Replace("Hz", "")
            );
        }
    }
}