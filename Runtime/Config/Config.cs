using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Controller;
using Slimebones.ECSCore.File;
using Slimebones.ECSCore.Key;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.UI;
using Slimebones.ECSCore.Utils;
using Slimebones.ECSCore.Utils.Parsing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Slimebones.ECSCore.Config
{
    /// <summary>
    /// Operates with game configuration file.
    /// </summary>
    public static class Config
    {
        private static string Filename = "config.ini";
        private static string DefaultSectionName = "Game";

        private static IniFile file =
            new IniFile(
                Application.persistentDataPath + "/" + Filename
            );
        private static World world;

        private static Dictionary<string, ConfigSpecMeta> specMetaByKey =
            new Dictionary<string, ConfigSpecMeta>();

        public static void Init(World world)
        {
            Config.world = world;
        }

        public static void SubscribeSetting(Entity e, UIInputType uiInputType)
        {
            var go = GameObjectUtils.GetUnityOrError(e);
            var key = e.GetComponent<Key.Key>().key;

            CheckContainsKey(key);
            var meta = specMetaByKey[key];
            if (!meta.settingGOsByType.ContainsKey(uiInputType))
            {
                meta.settingGOsByType[uiInputType] = new List<GameObject>();
            }
            meta.settingGOsByType[uiInputType].Add(go);
            meta.spec.OnInit(e, world);
        }

        /// <summary>
        /// Loads the dictionary.
        /// </summary>
        public static void Register<T>(
            IConfigSpec<T>[] specs
        )
        {
            foreach (var spec in specs)
            {
                if (specMetaByKey.ContainsKey(spec.Key))
                {
                    Log.Error(
                        "spec with key {0} already exists => skip",
                        spec.Key
                    );
                }

                ConfigSpecMeta meta = new ConfigSpecMeta();
                meta.spec = (IConfigSpec<object>)spec;
                meta.isParser = spec is IParser<T>;
                // spec cannot be parser and parseable at the same time,
                // take only parsers and ignore rest in such cases
                if (!meta.isParser)
                {
                    // allows for general (IParseable<>) generic comparison
                    // ref: https://stackoverflow.com/a/503359
                    meta.isParseable = spec.GetType().GetInterfaces().Any(
                        x =>
                            x.IsGenericType
                            && x.GetGenericTypeDefinition()
                                == typeof(IParseable<>)
                    );
                }
                specMetaByKey[spec.Key] = meta;

                
                //IParseRes<T> parseRes;
                //T value = ParsingUtils.Parse(
                //    GetValueStrForSpec(
                //        (IConfigSpec<object>)spec
                //    ),
                //    spec.ParseOpts,
                //    out parseRes
                //);

                //spec.OnChange(
                //    value,
                //    world
                //);
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
            SetNoCallback(key, value);
            specMetaByKey[key].spec.OnChange(value, world);
        }

        private static void SetNoCallback(
            string key,
            string value
        )
        {
            CheckContainsKey(key);
            file.Write(key, value, DefaultSectionName);
        }

        private static void CheckContainsKey(string key)
        {
            if (!specMetaByKey.ContainsKey(key))
            {
                throw new NotFoundException(
                    "config spec with key",
                    key
                );
            }
        }

        private static string GetValueStrForSpec(
            IConfigSpec<object> spec
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
