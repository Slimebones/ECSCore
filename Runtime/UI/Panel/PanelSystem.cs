using Scellecs.Morpeh;
using Slimebones.CSKit.Logging;
using Slimebones.ECSCore.Event;
using Slimebones.ECSCore.GO;
using Slimebones.ECSCore.KeyCode;
using Slimebones.ECSCore.Request;
using System.Collections.Generic;
using UnityEngine;

namespace Slimebones.ECSCore.UI.Panel
{
    public class PanelSystem: ISystem
    {
        private Filter panelF;
        private Filter reqF;

        private Dictionary<string, GameObject> panelGOByKey =
            new Dictionary<string, GameObject>();

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            reqF = RequestUtils.FB.With<SetPanelStateRequest>().Build();
            panelF = World.Filter.With<Panel>().Build();

            InitStorage();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var reqE in reqF)
            {
                ref var reqC = ref reqE.GetComponent<SetPanelStateRequest>();

                if (!panelGOByKey.ContainsKey(reqC.code))
                {
                    Log.Error(
                        "unregistered request's key "
                        + reqC.code
                        + " => skip"
                    );
                    RequestUtils.Complete(reqE);
                    continue;
                }

                bool finalState = GetFinalState(
                    ref reqC,
                    panelGOByKey[reqC.code]
                );
                panelGOByKey[reqC.code].SetActive(finalState);

                ref var evt =
                    ref EventUtils.Create<PanelStateEvent>();
                evt.key = reqC.code;
                evt.isEnabled = finalState;
                evt.go = panelGOByKey[reqC.code];

                RequestUtils.Complete(reqE);
            }
        }

        public void Dispose()
        {
        }

        private bool GetFinalState(
            ref SetPanelStateRequest reqC,
            GameObject reffedGO
        )
        {
            switch (reqC.state)
            {
                case PanelStateChange.Enable:
                    return true;
                case PanelStateChange.Disable:
                    return false;
                case PanelStateChange.Toggle:
                    // use exactly active self since we do want to
                    // know only the object's state, his parents don't
                    // matter here
                    return !reffedGO.activeSelf;
                default:
                    Log.Error(
                        "[Panel] undefined panel request's state "
                        + reqC.state
                        + " => set to false"
                    );
                    return false;
            }
        }

        private void InitStorage()
        {
            foreach (var panelE in panelF)
            {
                var key = panelE.GetComponent<Code>().code;
                var panelGO = GOUtils.GetUnity(
                    panelE
                );

                if (key == "")
                {
                    Log.Error(
                        "[Panel] game object "
                        + panelGO
                        + " has an empty key => skip"
                    );
                    continue;
                }

                if (panelGOByKey.ContainsKey(key))
                {
                    Log.Error(
                        "[Panel] game object "
                        + panelGO
                        + "has a duplicate key "
                        + key
                        + " => skip"
                    );
                    continue;
                }

                panelGOByKey[key] = panelGO;
            }
        }
    }
}
