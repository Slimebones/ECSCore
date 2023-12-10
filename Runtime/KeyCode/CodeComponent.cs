using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using TriInspector;
using Unity.IL2CPP.CompilerServices;

namespace Slimebones.ECSCore.KeyCode
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class CodeComponent: MonoProvider<Code>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct Code: IComponent
    {
        [HideLabel]
        public string key;
    }
}
