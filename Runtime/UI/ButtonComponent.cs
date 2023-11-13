using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityUI = UnityEngine.UI;

namespace Slimebones.ECSCore.UI
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class ButtonComponent: MonoProvider<Button>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct Button: IComponent, IDisposable
    {
        [SerializeReference]
        public IButtonListener[] listeners;

        public void Dispose()
        {
            foreach (var listener in listeners)
            {
                listener.Unsubscribe();
            }
        }
    }
}
