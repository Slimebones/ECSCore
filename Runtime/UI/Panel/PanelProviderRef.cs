using Slimebones.ECSCore.UI.Panel;
using System;

namespace Slimebones.ClumsyDelivery.UI.Panel
{
    [Serializable]
    public class PanelProviderRef
    {
        public string key;
        public PanelComponent provider;
    }
}