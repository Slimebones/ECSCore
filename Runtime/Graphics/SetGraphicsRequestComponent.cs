using Scellecs.Morpeh.Providers;
using Slimebones.ECSCore.Base.Request;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Slimebones.ECSCore.Graphics
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class SetGraphicsRequestComponent: MonoProvider<SetGraphicsRequest>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct SetGraphicsRequest: IRequestComponent
    {
        public Resolution? resolution;
        public FullScreenMode? fullScreenMode;
        public bool? isVsyncEnabled;
    }
}
