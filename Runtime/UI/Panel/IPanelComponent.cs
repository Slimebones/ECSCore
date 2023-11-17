using Scellecs.Morpeh;

namespace Slimebones.ECSCore.UI.Panel
{
    public interface IPanelComponent: IComponent
    {
        public bool IsEnabled
        {
            get; set;
        }
    }
}