using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.React;
using Slimebones.ECSCore.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.TMP_Dropdown;

namespace Slimebones.ECSCore.UI.Settings1
{
    public class ToggleSettingListener: IEntityListener
    {
        private string key;
        private Toggle toggleUnity;

        public void Subscribe(Entity e, World world)
        {
            key = e.GetComponent<Key.Key>().key;
            var go = GameObjectUtils.GetUnityOrError(e);
            toggleUnity = go.GetComponent<Toggle>();
            toggleUnity.onValueChanged.AddListener(Call);

            // don't perform an additional validation for now
            toggleUnity.isOn = Config.Config.Get(key) == "1";
        }

        public void Unsubscribe()
        {
            toggleUnity.onValueChanged.RemoveListener(Call);
        }

        private void Call(bool flag)
        {
            Config.Config.Set(
                key,
                flag ? "1" : "0"
            );
        }
    }
}