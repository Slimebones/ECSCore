using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using TriInspector;
using Unity.IL2CPP.CompilerServices;

namespace Slimebones.ECSCore.Collision
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class ContactActionsComponent: MonoProvider<ContactActions>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ContactActions: IComponent
    {
        [InfoBox(
            "Action spec receives two entities: first is host,"
            + " second is guest."
        )]
        public ContactActionData[] actions;
    }
}
