using System;

namespace Slimebones.ECSCore.Scene {
    /// <summary>
    /// Stores information about scenes.
    /// </summary>
    /// <remarks>
    /// Identifies at least current and next Real scenes to load. Once a new
    /// scene is loaded, the nextRealScene should be set to null, and
    /// currentRealScene should be set to the new scene.
    /// </remarks>
    internal struct SceneStorage {
        public static string currentRealScene;
        public static string nextRealScene;
        /// <summary>
        /// Whether loading screen is loaded.
        /// </summary>
        public static bool isLoadingScreen;
    }
}
