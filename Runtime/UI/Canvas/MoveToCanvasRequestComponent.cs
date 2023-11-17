using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Slimebones.ECSCore.Base;
using Unity.IL2CPP.CompilerServices;

namespace Slimebones.ECSCore.UI.Canvas
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class MoveToCanvasRequestComponent: MonoProvider<MoveToCanvasRequest>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct MoveToCanvasRequest: IRequestComponent
    {
        public Entity targetE;
        public Entity canvasE;
    }
}
