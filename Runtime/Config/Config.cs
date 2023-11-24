using Scellecs.Morpeh;
using Slimebones.ECSCore.Controller;
using Slimebones.ECSCore.File;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils;
using Slimebones.ECSCore.Utils.Parsing;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        private static List<string> keys = new List<string>();
        private static List<IConfigSpec<object>> _specs =
            new List<IConfigSpec<object>>();

        public static void Init(World world)
        {
            Config.world = world;
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
                if (keys.Contains(spec.Key))
                {
                    Log.Error(
                        "spec with key {0} already exists => skip",
                        spec.Key
                    );
                }
                keys.Add(spec.Key);
                _specs.Add((IConfigSpec<object>)spec);

                IParseRes<T> parseRes;
                T value = ParsingUtils.Parse(
                    GetValueStrForSpec(
                        (IConfigSpec<object>)spec
                    ),
                    spec.ParseOpts,
                    out parseRes
                );

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
            intSpecByKey[key].OnChange(value, world);
        }

        private static void SetNoCallback(
            string key,
            string value
        )
        {
            if (!intSpecByKey.ContainsKey(key))
            {
                throw new NotFoundException(
                    "config spec with key",
                    key
                );
            }
            file.Write(key, value, DefaultSectionName);
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
