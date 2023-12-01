using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.GO;
using Slimebones.ECSCore.Base.Request;
using Slimebones.ECSCore.React;
using UnityEngine.Serialization;
using UnityUI = UnityEngine.UI;

namespace Slimebones.ECSCore.UI.Panel
{
    public class SetPanelStateButtonListener: IEntityListener
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
            unityButton = GOUtils.GetUnity(
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
                ref RequestUtils.Create<SetPanelStateRequest>(
                    1,
                    world
                );
            req.key = targetPanelKey;
            req.state = targetChangeState;
        }
    }
}