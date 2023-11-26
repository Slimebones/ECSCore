using Palmmedia.ReportGenerator.Core.Reporting.Builders.Rendering;
using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Controller;
using Slimebones.ECSCore.File;
using Slimebones.ECSCore.Key;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.UI;
using Slimebones.ECSCore.Utils;
using Slimebones.ECSCore.Utils.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Slimebones.ECSCore.Config
{
    /// <summary>
    /// Operates with game configuration file.
    /// </summary>
    public static partial class Config
    {
        public static World World { get; set; }

        private static Dictionary<string, IConfigSpec> specByKey =
            new Dictionary<string, IConfigSpec>();
        private static Dictionary<
            string, List<Action<string>>
        > settingUpdateActionsByKey =
            new Dictionary<string, List<Action<string>>>();

        private static readonly string Filename = "config.ini";
        private static readonly string DefaultSectionName = "Game";
        private static readonly int MaxOnChangeRecursion = 5;
        private static readonly IniFile file =
            new IniFile(
                Application.persistentDataPath + "/" + Filename
            );

        public static void Register(
            IConfigSpec[] specs
        )
        {
            foreach (var spec in specs)
            {
                if (specByKey.ContainsKey(spec.Key))
                {
                    Log.Error(
                        "spec with key {0} already exists => skip",
                        spec.Key
                    );
                }

                spec.World = World;

                string newValue;
                bool isValueChanged = spec.OnInit(
                    GetInitialValueForSpec(spec),
                    out newValue
                );
                if (isValueChanged)
                {
                    SetSpecValue(newValue, spec);
                }
            }
        }

        public static void SubscribeSetting(Entity e, UIInputType uiInputType)
        {
            var key = e.GetComponent<Key.Key>().key;
            CheckContainsKey(key);

            SubscribeByInputType(
                key,
                GameObjectUtils.GetUnityOrError(e),
                uiInputType
            );

            if (!settingUpdateActionsByKey.ContainsKey(key))
            {
                settingUpdateActionsByKey[key] = new List<Action<string>>();
            }
            settingUpdateActionsByKey[key].Add(
                specByKey[key].OnSettingInit(e, Get(key))
            );
        }

        private static void SubscribeByInputType(
            string key,
            GameObject go,
            UIInputType uiInputType
        )
        {
            switch (uiInputType)
            {
                case UIInputType.Dropdown:
                    var handler = new DropdownSettingListener();
                    handler.Init(key, go);
                    dropdown
                        .onValueChanged
                        .AddListener((int index) =>
                            (new DropdownSettingListener()).OnDropdownValue(
                                key, index, dropdown
                            )
                        );
                    break;
                default:
                    throw new NotFoundException(
                        "input type", uiInputType.ToString()
                    );
            }
        }

        public static string Get(
            string key
        )
        {
            if (!file.KeyExists(key, DefaultSectionName))
            {
                throw new NotFoundException("key", key);
            }
            return file.Read(key, DefaultSectionName);
        }

        public static void Set(
            string key,
            string value
        )
        {
            SetSilent(key, value);
            SetSpecValue(value, specByKey[key]);
        }

        private static void SetSpecValue(
            string value,
            IConfigSpec spec,
            int recursionNum = 0
        )
        {
            if (recursionNum > MaxOnChangeRecursion)
            {
                throw new RecursionException(MaxOnChangeRecursion);
            }

            string newNewValue;
            bool isValueChanged = spec.OnChange(value, out newNewValue);
            if (isValueChanged)
            {
                SetSpecValue(newNewValue, spec, recursionNum + 1);
                return;
            }

            // update settings only if it is a final value
            UpdateSettingsByKey(spec.Key, value);
        }

        private static void UpdateSettingsByKey(string key, string value)
        {
            if (!settingUpdateActionsByKey.ContainsKey(key))
            {
                throw new NotFoundException(
                    "setting update action for key", key
                );
            }

            foreach (var action in settingUpdateActionsByKey[key])
            {
                action(value);
            }
        }

        private static void SetSilent(
            string key,
            string value
        )
        {
            CheckContainsKey(key);
            file.Write(key, value, DefaultSectionName);
        }

        private static void CheckContainsKey(string key)
        {
            if (!specByKey.ContainsKey(key))
            {
                throw new NotFoundException(
                    "config spec with key",
                    key
                );
            }
        }

        private static string GetInitialValueForSpec(
            IConfigSpec spec
        )
        {
            if (!file.KeyExists(
                spec.Key, DefaultSectionName
            ))
            {
                file.Write(
                    spec.Key,
                    spec.DefaultValueStr,
                    DefaultSectionName
                );
                return spec.DefaultValueStr;
            }
            else
            {
                return file.Read(spec.Key, DefaultSectionName);
            }
        }
    }
}
