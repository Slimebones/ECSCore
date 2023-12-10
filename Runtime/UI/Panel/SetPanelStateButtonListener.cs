using Scellecs.Morpeh;
using Slimebones.ECSCore.GO;
using Slimebones.ECSCore.React;
using Slimebones.ECSCore.Request;
using UnityEngine.Serialization;
using UnityUI = UnityEngine.UI;

namespace Slimebones.ECSCore.UI.Panel
{
    public class SetPanelStateButtonListener: IEntityListener
    {
        public string targetPanelKey;
        public PanelStateChange targetChangeState;

        private UnityUI.Button unityButton;

        public void Subscribe(
            Entity e, World world
        )
        {
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
            ref var req = ref RequestUtils.Create<SetPanelStateRequest>();
            req.code = targetPanelKey;
            req.state = targetChangeState;
        }
    }
}