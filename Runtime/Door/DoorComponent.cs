using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Slimebones.ECSCore.Condition;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Slimebones.ECSCore.Door
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class DoorComponent: MonoProvider<Door>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct Door: IComponent
    {
        public Vector3 openRelativePosition;
        [Tooltip("Gets converted to Quaternion at runtime.")]
        public Vector3 openRelativeRotation;
        public float speed;
        [SerializeReference]
        public ICondition[] openConditions;
        [Tooltip(
            "Whether the door closes back after negative conditioning."
        )]
        public bool isBackClosed;

        [HideInInspector]
        public DoorState state;

        [HideInInspector]
        public Vector3 initialPosition;

        [HideInInspector]
        public Quaternion initialRotation;

        [HideInInspector]
        public Vector3 openedPosition;
        [HideInInspector]
        public Quaternion openedRotation;
    }
}
