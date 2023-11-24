using Scellecs.Morpeh;
using Slimebones.ECSCore.Audio;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Input;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils.Parsing;
using System;

namespace Slimebones.ECSCore.Config.Specs
{
    public class MusicVolumeConfigSpec: IConfigSpec
    {
        public string Key => "music-volume";
        public string DefaultValueStr => "100";

        public static readonly IntParseOpts parseOpts =
            new IntParseOpts(
                min: 0,
                max: 100
            );

        public void OnChange(string value, World world)
        {
            IntParseRes parseRes;
            bool retflag = ParsingUtils.Parse(
                value,
                parseOpts,
                out parseRes
            );
            if (!retflag)
            {
                Log.Error(
                    "cannot parse music {0}, use default {1}",
                    value,
                    DefaultValueStr
                );
                Config.Set(Key, DefaultValueStr);
                return;
            }

            // reset on limit error
            if (parseRes.isOutOfAnyLimit)
            {
                Config.Set(Key, parseRes.value.ToString());
                return;
            }

            ref var reqc = ref RequestComponentUtils.Create<SetAudioReq>(
                1, world
            );
            reqc.volume = parseRes.value;
        }
    }
}