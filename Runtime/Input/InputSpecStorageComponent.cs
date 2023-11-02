using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;

namespace Slimebones.ECSCore.Input
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class InputSpecStorageComponent
        : MonoProvider<InputSpecStorage>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct InputSpecStorage: IComponent
    {
        public List<InputSpec> specs;
    }
}
