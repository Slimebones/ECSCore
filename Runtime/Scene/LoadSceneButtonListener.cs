using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.GO;
using Slimebones.ECSCore.React;
using System;
using System.Collections.Generic;
using UnityUI = UnityEngine.UI;

namespace Slimebones.ECSCore.Scene
{
    public class LoadSceneButtonListener: IEntityListener
    {

        public static class SceneLoadActions
        {
            public static readonly Dictionary<
                ActionType,
                Action<Args>
            > ActionByType = new Dictionary<ActionType, Action<Args>> {
                { ActionType.Load, Load },
                { ActionType.Restart, Restart },
            };

            public enum ActionType
            {
                Load = 0,
                Restart = 1,
            }

            public class Args
            {
                public string name;
                public bool isLoadingScreenEnabled = false;
            }

            private static void Load(Args args)
            {
                SceneUtils.Load(
                    args.name,
                    args.isLoadingScreenEnabled
                );
            }

            private static void Restart(Args args)
            {
                SceneUtils.Restart();
            }
        }

        public string name;
        public SceneLoadActions.ActionType actionType;
        public bool isLoadingScreenEnabled = false;
        public bool shouldAllRequestsBeUnlocked = true;

        private Entity e;
        private UnityUI.Button unityButton;
        private World world;
        private SceneLoadActions.Args sceneLoadActionsArgs;

        public void Subscribe(
            Entity e, World world
        )
        {
            this.e = e;
            this.world = world;
            unityButton = GOUtils.GetUnity(
                e
            ).GetComponent<UnityUI.Button>();

            sceneLoadActionsArgs = new SceneLoadActions.Args();
            sceneLoadActionsArgs.name = name;
            sceneLoadActionsArgs.isLoadingScreenEnabled =
                isLoadingScreenEnabled;

            unityButton.onClick.AddListener(Call);
        }

        public void Unsubscribe()
        {
            unityButton.onClick.RemoveListener(Call);
        }

        private void Call()
        {
            SceneLoadActions.ActionByType[actionType](sceneLoadActionsArgs);
        }
    }
}