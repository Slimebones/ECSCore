using Scellecs.Morpeh;
using Slimebones.CSKit.Logging;
using Slimebones.ECSCore.Config.Specs;
using Slimebones.ECSCore.GO;
using Slimebones.ECSCore.Key;
using Slimebones.ECSCore.Request;
using Slimebones.ECSCore.Utils;
using System;
using UnityEngine;

namespace Slimebones.ECSCore.Audio
{
    public class AudioSystem: ISystem
    {
        private Entity dataE;
        private Filter typeReqF;
        private Filter keyReqF;

        public Filter musicAudioF;
        public Filter environmentAudioF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            typeReqF = RequestUtils.FB.With<SetAudioByTypeReq>().Build();
            keyReqF = RequestUtils.FB.With<SetAudioByEntityReq>().Build();
            musicAudioF =
                World.Filter.With<Audio>().With<MusicAudio>().Build();
            environmentAudioF =
                World.Filter.With<Audio>().With<EnvironmentAudio>().Build();

            dataE = World.CreateEntity();
            dataE.AddComponent<InternalAudioData>();
        }

        public void OnUpdate(float deltaTime)
        {
            ProcessReqByType();
            ProcessReqByKey();
        }

        private void ProcessReqByKey()
        {
            foreach (var reqe in keyReqF)
            {
                try
                {
                    ref var reqc = ref reqe.GetComponent<SetAudioByEntityReq>();

                    var audioSource = GOUtils.GetUnity(
                        reqc.e
                    ).GetComponent<AudioSource>();

                    if (reqc.clip != null)
                    {
                        audioSource.clip = reqc.clip;
                        audioSource.Play();
                    }
                    RequestUtils.Complete(reqe);
                }
                catch (Exception exc)
                {
                    Log.Skip(reqe, exc);
                    continue;
                }
            }
        }

        private void ProcessReqByType()
        {
            ref var dataC = ref dataE.GetComponent<InternalAudioData>();

            foreach (var reqe in typeReqF)
            {
                try
                {
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

                    RequestUtils.Complete(reqe);
                }
                catch (Exception exc)
                {
                    Log.Skip(reqe, exc);
                    continue;
                }
            }
        }

        private void SetAudioSourceVolume(Filter f, float volume)
        {
            foreach (var e in f)
            {
                var audioSourceUnity = GOUtils.GetUnity(
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

