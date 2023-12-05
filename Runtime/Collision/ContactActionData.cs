using Slimebones.ECSCore.Base.ActionSpec;
using System;
using UnityEngine;

namespace Slimebones.ECSCore.Collision
{
    [Serializable]
    public struct ContactActionData
    {
        public Collider collider;
        [SerializeReference]
        public IActionSpec2E actionSpec;
    }
}