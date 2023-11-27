using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Slimebones.ECSCore.Base;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Slimebones.ECSCore.Audio
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class SetAudioByEntityReqComponent: MonoProvider<SetAudioByEntityReq>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct SetAudioByEntityReq: IRequestComponent
    {
        public Entity e;

        public AudioClip clip;
    }
}
