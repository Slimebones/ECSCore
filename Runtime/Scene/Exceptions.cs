using System;

namespace Slimebones.ECSCore.Scene {
    public class NotLevelException : Exception {
        public NotLevelException(
            string sceneName
        ) : base (
            string.Format(
                "scene <{0}> is not a level", sceneName
            )
        ) {}
    }
}