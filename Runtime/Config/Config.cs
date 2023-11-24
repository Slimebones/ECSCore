using Scellecs.Morpeh;
using Slimebones.ECSCore.Controller;
using Slimebones.ECSCore.File;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils;
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

        private static IniFile file;
        private static World world;

        // key strings are not capitalized for this dictionary (litle
        // performance)
        private static Dictionary<string, IConfigSpec<object>> specByKey =
            new Dictionary<string, IConfigSpec<object>>();

        /// <summary>
        /// Loads the dictionary.
        /// </summary>
        public static void Init(
            IConfigSpec<object>[] specs,
            World world
        )
        {
            Config.world = world;
            file = new IniFile(
                Application.persistentDataPath + "/" + Filename
            );

            foreach (var spec in specs)
            {
                if (specByKey.ContainsKey(spec.Key))
                {
                    Log.Warning(
                        "spec with key {0} already exists, skip",
                        spec.Key
                    );
                }
                specByKey[spec.Key] = spec;

                spec.OnChange(GetValueForSpec(spec), world);
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
            specByKey[key].OnChange(value, world);
        }

        private static void SetNoCallback(
            string key,
            string value
        )
        {
            if (!specByKey.ContainsKey(key))
            {
                throw new NotFoundException(
                    "config spec with key",
                    key
                );
            }
            file.Write(key, value, DefaultSectionName);
        }

        private static string GetValueForSpec(IConfigSpec spec)
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
