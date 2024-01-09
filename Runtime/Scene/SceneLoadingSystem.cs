using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.SceneManagement;
using Slimebones.ClumsyDelivery.Scene;
using Slimebones.ECSCore.Object;
using Slimebones.ECSCore.Request;
using Slimebones.ECSCore.Input;

namespace Slimebones.ECSCore.Scene
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(SceneLoadingSystem))]
    public sealed class SceneLoadingSystem : UpdateSystem {
        private Filter loadingSpinnerF;
        private Filter loadingSpeedF;
        private Filter loadingOperationF;
        private Filter loadRequestsF;
        private float progress = 0;

        public override void OnAwake() {
            loadingSpinnerF = World.Filter.With<LoadingSpinner>().Build();
            loadingSpeedF = World
                .Filter
                .With<SceneLoadingSpeed>()
                .Build();
            loadingOperationF =
                World.Filter.With<SceneLoadingOperation>().Build();
            loadRequestsF = RequestUtils.FB.With<LoadSceneRequest>().Build();

            if (SceneStorage.currentRealScene == null) {
                SceneStorage.currentRealScene =
                    SceneManager.GetActiveScene().name;
            }

            // if the system appeared to be inside loading screen - schedule
            // loading of the next scene from the storage
            if (SceneStorage.isLoadingScreen) {
                LoadNextProgressive();
            }
        }

        public override void OnUpdate(float deltaTime) {
            if (SceneStorage.isLoadingScreen) {
                RotateSpinners(deltaTime);
                UpdateProgress(deltaTime);
                return;
            }

            ProcessLoadRequests();
        }

        private void ProcessLoadRequests()
        {
            foreach (var e in loadRequestsF)
            {
                // only first load request is satisfied

                ref LoadSceneRequest loadRequest = ref e
                    .GetComponent<LoadSceneRequest>();

                SceneStorage.nextRealScene = loadRequest.sceneName;
                bool isLoadingScreenEnabled =
                    loadRequest.isLoadingScreenEnabled;

                RequestUtils.Complete(e);

                if (isLoadingScreenEnabled)
                {
                    LoadLoadingScreen();
                    return;
                }

                // in case of no loading screen, load next scene
                // immediately
                LoadNextImmediately();
                return;
            }
        }

        private void UpdateProgress(float deltaTime) {
            ref AsyncOperation loadingOperation = ref loadingOperationF.First()
                .GetComponent<SceneLoadingOperation>().value;
            ref SceneLoadingSpeed loadingSpeed = ref loadingSpeedF
                .First().GetComponent<SceneLoadingSpeed>();

            progress += deltaTime * loadingSpeed.loadingSpeed;

            if (progress >= 1) {
                SceneStorage.isLoadingScreen = false;
                loadingOperation.allowSceneActivation = true;
            }
        }

        private void RotateSpinners(float deltaTime) {
            foreach (var loadingSpinnerE in  loadingSpinnerF) {
                ref LoadingSpinner loadingSpinner =
                    ref loadingSpinnerE.GetComponent<LoadingSpinner>();

                ref GameObject loadingSpinnerGO = ref loadingSpinnerE
                    .GetComponent<Go>().value;
                loadingSpinnerGO.transform.Rotate(
                    new Vector3(0f, 0f, -loadingSpinner.animationSpeed * deltaTime)
                );
            }
        }

        private void LoadLoadingScreen() {
            // next awake of this system will result in LoadNext due to
            // isLoadingScreen flag activation
            SceneStorage.isLoadingScreen = true;
            SceneManager.LoadScene("Loading");
        }

        private void LoadNextImmediately() {
            string nextRealScene = SceneStorage.nextRealScene;
            SceneStorage.currentRealScene = nextRealScene;
            SceneStorage.nextRealScene = null;

            // unlock cursor and input in all cases to prevent nasty bugs of
            // jumping to another scene e.g. from ingame menu
            //
            // later it can be done only if according request's flag is given
            CursorUtils.UnlockCursor();

            SceneManager.LoadScene(nextRealScene);
        }

        private void LoadNextProgressive() {
            string nextRealScene = SceneStorage.nextRealScene;
            SceneStorage.currentRealScene = nextRealScene;
            SceneStorage.nextRealScene = null;

            ref SceneLoadingOperation loadingOperation = ref loadingOperationF
                .First().GetComponent<SceneLoadingOperation>();

            AsyncOperation operation = SceneManager.LoadSceneAsync(
                nextRealScene
            );
            operation.allowSceneActivation = false;
            loadingOperation.value = operation;
        }
    }
}
