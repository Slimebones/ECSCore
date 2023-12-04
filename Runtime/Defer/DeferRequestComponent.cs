using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Slimebones.ECSCore.Base.CoreSystem;
using Slimebones.ECSCore.Base.Request;
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Slimebones.ECSCore.Defer
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class DeferRequestComponent: MonoProvider<DeferRequest>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct DeferRequest: IRequestComponent
    {
        public Action action;
        public UpdateType updateType;
        public SystemCallOrder callOrder;
        public int framesToSkip;

        internal int skippedFrames;
    }
}
