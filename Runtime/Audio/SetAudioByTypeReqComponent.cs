using Scellecs.Morpeh.Providers;
using Slimebones.ECSCore.Base;
using Unity.IL2CPP.CompilerServices;

namespace Slimebones.ECSCore.Audio
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class SetAudioByTypeReqComponent: MonoProvider<SetAudioByTypeReq>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct SetAudioByTypeReq: IReqComponent
    {
        public AudioType type;

        public float? volume;
    }
}
