using Scellecs.Morpeh;
using Slimebones.ECSCore;
using Slimebones.ECSCore.React;
using TMPro;

namespace Slimebones.ECSCore.UI.Settings
{
    public class SettingListener: IEntityListener
    {
        public UIInputType uiInputType;
        public TextMeshProUGUI displayText = null;

        public void Subscribe(Entity e, World world)
        {
            Config.Config.SubscribeSetting(e, uiInputType);
        }

        public void Unsubscribe()
        {
            // do nothing for now
        }
    }
}