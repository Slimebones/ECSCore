using Scellecs.Morpeh;
using Slimebones.ECSCore.Utils;

namespace Slimebones.ECSCore.UI.Settings
{
    public interface ISettingComponent: IComponent
    {
        public string Key { get; set; }
        public IListener MainListener { get; set; }
    }
}