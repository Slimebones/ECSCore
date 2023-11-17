using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Slimebones.ECSCore.UI.Button;
using Slimebones.ECSCore.Utils;
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Slimebones.ECSCore.UI.Settings
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class DropdownSettingComponent: MonoProvider<DropdownSetting>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct DropdownSetting: ISettingComponent, IDisposable
    {
        public string key;

        [HideInInspector]
        public IListener mainListener;

        public string Key
        {
            get => key;
            set => key = value;
        }

        public IListener MainListener
        {
            get => mainListener;
            set => mainListener = value;
        }

        public void Dispose()
        {
            mainListener.Unsubscribe();
        }
    }
}
