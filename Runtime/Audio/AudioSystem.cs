using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Config.Specs;
using Slimebones.ECSCore.Key;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils;
using UnityEngine;

namespace Slimebones.ECSCore.Audio
{
    public class AudioSystem: ISystem
    {
        private Entity dataE;
        private Filter reqf;

        public Filter musicAudioF;
        public Filter environmentAudioF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            reqf = World.Filter.With<SetAudioByTypeReq>().Build();
            musicAudioF =
                World.Filter.With<Audio>().With<MusicAudio>().Build();
            environmentAudioF =
                World.Filter.With<Audio>().With<EnvironmentAudio>().Build();

            dataE = World.CreateEntity();
            dataE.AddComponent<InternalAudioData>();
        }

        public void OnUpdate(float deltaTime)
        {
            ref var dataC = ref dataE.GetComponent<InternalAudioData>();

            foreach (var reqe in reqf)
            {
                if (!RequestComponentUtils.RegisterCall(reqe))
                {
                    continue;
                }

                ref var reqc = ref reqe.GetComponent<SetAudioByTypeReq>();

                switch (reqc.type)
                {
                    case AudioType.General:
                        if (reqc.volume != null)
                        {
                            RecalculateAllVolumes(ref reqc, ref dataC);
                        }
                        break;
                    case AudioType.Music:
                        if (reqc.volume != null)
                        {
                            SetMusicVolume(
                                (float)reqc.volume,
                                dataC.generalVolume,
                                ref dataC
                            );
                        }
                        break;
                    case AudioType.Environment:
                        if (reqc.volume != null)
                        {
                            SetEnvironmentVolume(
                                (float)reqc.volume,
                                dataC.generalVolume,
                                ref dataC
                            );
                        }
                        break;
                    default:
                        Log.Error(
                            "unrecognized audio type {0} => skip",
                            reqc.type
                        );
                        break;
                }
            }
        }

        private void SetAudioSourceVolume(Filter f, float volume)
        {
            foreach (var e in f)
            {
                var audioSourceUnity = GameObjectUtils.GetUnityOrError(
                    e
                ).GetComponent<AudioSource>();
                ref var audioC = ref e.GetComponent<Audio>();

                audioSourceUnity.volume = volume * audioC.volumeModifier;
            }
        }

        private void RecalculateAllVolumes(
            ref SetAudioByTypeReq reqc,
            ref InternalAudioData dataC
        )
        {
            dataC.generalVolume = (float)reqc.volume;

            SetMusicVolume(
                dataC.musicVolume,
                dataC.generalVolume,
                ref dataC
            );
            SetEnvironmentVolume(
                dataC.environmentVolume,
                dataC.generalVolume,
                ref dataC
            );
        }

        private void SetMusicVolume(
            float volume,
            float generalVolume,
            ref InternalAudioData dataC
        )
        {
            dataC.musicVolume = volume;
            SetAudioSourceVolume(musicAudioF, volume * generalVolume);
        }

        private void SetEnvironmentVolume(
            float volume,
            float generalVolume,
            ref InternalAudioData dataC
        )
        {
            dataC.environmentVolume = volume;
            SetAudioSourceVolume(environmentAudioF, volume * generalVolume);
        }

        public void Dispose()
        {
        }
    }
}

