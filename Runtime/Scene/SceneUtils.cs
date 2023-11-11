using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using UnityEngine.SceneManagement;

namespace Slimebones.ECSCore.Scene {
    public static class SceneUtils {
        /// <summary>
        /// Loads level with next integer.
        /// </summary>
        /// <param name="world"></param>
        public static void LoadNextLevel(World world) {
            string currentScene = SceneStorage.currentRealScene;

            if (!currentScene.StartsWith("Level_")) {
                throw new NotLevelException(currentScene); 
            }

            int levelNumber = GetLevelNumber(currentScene);
            levelNumber++;

            // TODO(ryzhovalex):
            //      consider max possible level, after reaching it,
            //      do something special
            Load("Level_" + levelNumber, world);
        }

        public static void Load(
            string name,
            World world,
            bool isLoadingScreenEnabled = false
        ) {
            // create a request to scene systems to load a new scene
            ref LoadSceneRequest loadRequest =
                ref RequestComponentUtils.Create<LoadSceneRequest>(0, world);
            loadRequest.sceneName = name;
            loadRequest.isLoadingScreenEnabled = isLoadingScreenEnabled;
        }

        /// <summary>
        /// Restarts current scene.
        /// </summary>
        /// <param name="world"></param>
        public static void Restart(World world, bool isLoadingScreenEnabled) {
            Load(
                SceneManager.GetActiveScene().name,
                world,
                isLoadingScreenEnabled
            );
        }

        public static int GetLevelNumber(string levelName) {
            return int.Parse(levelName.Replace("Level_", ""));
        }
    }
}