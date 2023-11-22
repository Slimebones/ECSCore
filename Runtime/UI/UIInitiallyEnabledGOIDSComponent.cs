using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Slimebones.ECSCore.UI
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class UIInitiallyEnabledGOIDSComponent: MonoProvider<UIInitiallyEnabledGOIDS>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct UIInitiallyEnabledGOIDS: IComponent
    {
        [HideInInspector]
        public List<int> value;
    }
}
