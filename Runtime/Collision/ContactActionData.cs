using Slimebones.ECSCore.ActionSpec;
using System;
using TriInspector;
using UnityEngine;

namespace Slimebones.ECSCore.Collision
{
    [Serializable]
    public struct ContactActionData
    {
        [PropertyTooltip(
            "Whether the action should be called on every collision of host"
            + " where guest collider is not in the list."
        )]
        public bool isExcept;
        public Collider[] colliders;
        [SerializeReference]
        public IActionSpec actionSpec;
    }
}