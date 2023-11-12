using Scellecs.Morpeh;

namespace Slimebones.ECSCore.UI
{
    public interface IPanelComponent: IComponent
    {
        public bool IsEnabled { get; set; }
    }
}