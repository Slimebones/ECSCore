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
            UnityEngine.GameObject go
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
            UnityEngine.GameObject unityGO
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

    /// <summary>
    /// The instance is not supported.
    /// </summary>
    public class UnsupportedException: Exception
    {
        public UnsupportedException(string value)
            : base(string.Format("{0} is unsupported", value)) { }
    }

    /// <summary>
    /// Some feature is unavailable in current condition.
    /// </summary>
    public class UnavailableFeatureException: Exception
    {
        public UnavailableFeatureException(
            string feature,
            string explanation
        ) : base(string.Format(
            "{0} is unavailable: {1}", feature, explanation
        ))
        {
        }
    }

    /// <summary>
    /// An object is not found.
    /// </summary>
    public class NotFoundException: Exception
    {
        public NotFoundException(
            string value
        ) : base(
            string.Format(
                "{0} is not found", value
            )
        )
        {
        }
    }

    /// <summary>
    /// An object cannot be none.
    /// </summary>
    public class CannotBeNullException: Exception
    {
        public CannotBeNullException(
            string value
        ) : base(
            value + " cannot be none"
        )
        {
        }
    }

    public class AlreadyEventException: Exception
    {
        public AlreadyEventException(
            string value,
            string evt
        ) : base(
            string.Format(
                "{0} already {1}",
                value,
                evt
            )
        )
        {
        }
    }

    public abstract class ExpectException: Exception
    {
        public ExpectException(
            string message
        )
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Some length of an enumerable is expected.
    /// </summary>
    public class LengthExpectException<T>: ExpectException
    {
        public LengthExpectException(
            T[] array,
            int expectedLength
        ) : base(
            string.Format(
                "array {0} is expected to be of length {1}, got length {2}",
                string.Join(",", array),
                expectedLength,
                array.Length
            )
        )
        {
        }
    }
}