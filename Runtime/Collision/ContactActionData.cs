using Slimebones.ECSCore.ActionSpec;
using System;
using UnityEngine;

namespace Slimebones.ECSCore.Collision
{
    [Serializable]
    public struct ContactActionData
    {
        public Collider collider;
        [SerializeReference]
        public IActionSpec actionSpec;
    }
}