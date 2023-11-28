using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.React;
using UnityUI = UnityEngine.UI;

namespace Slimebones.ECSCore.UI.Panel
{
    public class PanelStateChangeButtonListener: IEntityListener
    {
        public string targetPanelKey;
        public PanelStateChange targetChangeState;

        private Entity e;
        private UnityUI.Button unityButton;
        private World world;

        public void Subscribe(
            Entity e, World world
        )
        {
            this.e = e;
            this.world = world;
            unityButton = GameObjectUtils.GetUnityOrError(
                e
            ).GetComponent<UnityUI.Button>();

            unityButton.onClick.AddListener(Call);
        }

        public void Unsubscribe()
        {
            unityButton.onClick.RemoveListener(Call);
        }

        private void Call()
        {
            ref var req =
                ref ReqUtils.Create<SetPanelStateRequest>(
                    1,
                    world
                );
            req.key = targetPanelKey;
            req.state = targetChangeState;
        }
    }
}