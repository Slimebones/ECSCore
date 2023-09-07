using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Slimebones.ECSCore.Global {
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class GlobalComponent : MonoProvider<Global> {
    }

    /// <summary>
    /// Able to collide with other Colliders.
    /// </summary>
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct Global : IComponent {
        [HideInInspector]
        public GlobalBridge bridge;
    }
}
