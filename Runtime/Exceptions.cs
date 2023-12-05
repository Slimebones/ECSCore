using Scellecs.Morpeh;
using System;
using UnityEditor;
using UnityEngine;

namespace Slimebones.ECSCore
{
    public class MissingECSGameObjectException: Exception
    {
        public MissingECSGameObjectException(string misser)
            : base(string.Format(
                "{0} missing ECSGameObject component", misser
            ))
        {
        }

        public MissingECSGameObjectException(Entity e)
            : base(string.Format(
                "missing ECSGameObject component for entity <{0}>",
                e.ID
            ))
        {
        }
    }

    public class UnsetECSGameObjectValueException: Exception
    {
        public UnsetECSGameObjectValueException(Entity e)
            : base(string.Format(
                "value for ECSGameObject is unset for entity <{0}>",
                e.ID
            ))
        {
        }
    }

    public class MissingUnityComponentException<TComponent>: Exception
    {
        public MissingUnityComponentException(
            GameObject go
        )
            : base(string.Format(
                "missing Unity component <{0}> on Unity game object <{1}>",
                typeof(TComponent).FullName,
                go
            ))
        {
        }
    }

    public class MissingECSComponentException<TComponent>
        : Exception where TComponent : struct, IComponent
    {
        public MissingECSComponentException(
            GameObject unityGO
        )
            : base(string.Format(
                "missing ECS component <{0}> on Unity game object <{1}>",
                typeof(TComponent).FullName,
                unityGO
            ))
        {
        }
    }

    /// <summary>
    /// Max recursion reached
    /// </summary>
    public class RecursionException: Exception
    {
        public RecursionException(
            int max
        )
            : base(string.Format(
                "max recursion of {0} has been reached",
                max
            ))
        {
        }
    }
}