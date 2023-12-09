using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Slimebones.ECSCore.Storage;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;

namespace Slimebones.ECSCore.Input
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct InputSpecStorage: IStorageComponent
    {
        public List<InputSpec> specs;
        public List<int> disabledSpecIndexes;
        public bool isEnabled;
    }
}
