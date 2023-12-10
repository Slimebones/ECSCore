using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Slimebones.ECSCore.Request;
using Unity.IL2CPP.CompilerServices;

namespace Slimebones.ECSCore.UI.Panel
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class SetPanelStateRequestComponent: MonoProvider<SetPanelStateRequest>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct SetPanelStateRequest: IRequestComponent
    {
        public string refcode;
        public PanelStateChange state;
    }
}
