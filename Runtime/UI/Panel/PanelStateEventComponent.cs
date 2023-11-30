using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Slimebones.ECSCore.Base.Event;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Slimebones.ECSCore.UI.Panel
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PanelStateEventComponent: MonoProvider<PanelStateEvent>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct PanelStateEvent: IEventComponent
    {
        public string key;
        public bool isEnabled;
        public GameObject go;
    }
}
