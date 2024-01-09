using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Slimebones.ECSCore.Condition;
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Slimebones.ECSCore.Actors.Cannon
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class ObjectCannonComponent: MonoProvider<ObjectCannon>
    {
    }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ObjectCannon: IComponent
    {
        public float outForce;

        [SerializeReference]
        public ICondition[] conditions;

        public GameObject spawnedPrefab;
        public float spawnedObjectLifetime;
        public int maxSimultaneousObjects;


        [HideInInspector]
        public bool isEnabled;

        [HideInInspector]
        public float lastTriggeredTime;

        [HideInInspector]
        public int livingObjectsCount;
    }
}
