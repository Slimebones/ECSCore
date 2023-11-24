using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.React;
using TMPro;

namespace Slimebones.ECSCore.UI.Settings
{
    public class SettingListener: IListener
    {
        public UIInputType uiInputType;

        public void Subscribe(Entity e, World world)
        {
            Config.Config.SubscribeSetting(e, uiInputType);
        }

        public void Unsubscribe()
        {
            // do nothing for now
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