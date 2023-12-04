using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using System;
using Unity.IL2CPP.CompilerServices;

namespace Slimebones.ECSCore.Base.Event
{
    internal class InternalEventFreshComponent
    {
    }

    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct InternalEventFresh: IComponent
    {
    }
}
