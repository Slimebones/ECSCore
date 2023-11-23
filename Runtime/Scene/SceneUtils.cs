using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using UnityEngine.SceneManagement;

namespace Slimebones.ECSCore.Scene {
    public static class SceneUtils {
        /// <summary>
        /// Loads level with next integer.
        /// </summary>
        /// <param name="world"></param>
        public static void LoadNextLevelOrBackup(
            World world,
            string prefix,
            string backup
        )
        {
            string currentScene = SceneStorage.currentRealScene;

            if (!currentScene.StartsWith(prefix)) {
                throw new NotLevelException(currentScene); 
            }

            int levelNumber = GetLevelNumber(currentScene);
            levelNumber++;

            string finalName = prefix + levelNumber;
            if (IsExists(finalName))
            {
                Load(finalName, world);
                return;
            }
            Load(backup, world);
        }

        public static bool IsExists(string name)
        {
            return SceneUtility.GetBuildIndexByScenePath(name) >= 0;
        }

        public static void Load(
            string name,
            World world,
            bool isLoadingScreenEnabled = false,
            bool shouldAllRequestsBeUnlocked = true
        ) {
            // create a request to scene systems to load a new scene
            ref LoadSceneRequest loadRequest =
                ref RequestComponentUtils.Create<LoadSceneRequest>(1, world);
            loadRequest.sceneName = name;
            loadRequest.isLoadingScreenEnabled = isLoadingScreenEnabled;

            if (shouldAllRequestsBeUnlocked)
            {
                RequestComponentUtils.UnlockAll();
            }
        }

        /// <summary>
        /// Restarts current scene.
        /// </summary>
        /// <param name="world"></param>
        public static void Restart(
            World world,
            bool isLoadingScreenEnabled = false
        )
        {
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