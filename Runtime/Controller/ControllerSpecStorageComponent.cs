using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;

namespace Slimebones.ECSCore.Controller
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class ControllerSpecStorageComponent: MonoProvider<ControllerSpecStorage>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ControllerSpecStorage: IComponent
    {
        public Dictionary<string, IController> spec;
    }
}
