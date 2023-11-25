using Slimebones.ECSCore.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Slimebones.ECSCore.Config
{
    internal class ConfigSpecMeta
    {
        public IConfigSpec<object> spec;
        public Dictionary<UIInputType, List<GameObject>> settingGOsByType =
            new Dictionary<UIInputType, List<GameObject>>();
    }
}