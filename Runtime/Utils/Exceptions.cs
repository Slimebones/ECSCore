using System;
using System.Collections;
using System.Linq;

namespace Slimebones.ECSCore.Utils {
    /// <summary>
    /// The instance is not supported.
    /// </summary>
    public class UnsupportedException : Exception {
        public UnsupportedException(string title, string value)
            : base(String.Format("{0} <{1}> is unsupported", title, value)) {}
    }

    /// <summary>
    /// Some feature is unavailable in current condition.
    /// </summary>
    public class UnavailableFeatureException : Exception {
        public UnavailableFeatureException(
            string feature,
            string explanation
        ) : base(String.Format(
            "{0} is unavailable: {1}", feature, explanation
        )) {}
    }

    /// <summary>
    /// An object is not found.
    /// </summary>
    public class NotFoundException : Exception {
        public NotFoundException(
            string title,
            string value
        ) : base (
            String.Format(
                "{0} <{1}> is not found", title, value
            )
        ) {}

        public NotFoundException(
            string title
        ) : base (
            String.Format(
                "{0} is not found", title
            )
        ) {}
    }

    public abstract class ExpectException: Exception
    {
        public ExpectException(
            string message
        ): base(message) {}
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
        ) {}
    }
}
