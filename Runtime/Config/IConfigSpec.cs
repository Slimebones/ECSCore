using Scellecs.Morpeh;
using Slimebones.ECSCore.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Slimebones.ECSCore.Config
{
    public interface IConfigSpec
    {
        public string Key { get; }
        public string DefaultValueStr { get; }
        public World World { get; set; }

        /// <summary>
        /// When a new setting is subscribed.
        /// </summary>
        /// <param name="lastValue">
        /// Last value registered for the spec's key.
        /// </param>
        /// <returns>
        /// Action to update the setting when the value is changed.
        /// </returns>
        public Action<string> OnSettingInit(Entity e);

        /// <summary>
        /// Reacts for external changes.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>
        /// Whether the value has been changed.
        /// </returns>
        public bool OnChange(string value, out string newValue);
    }
}