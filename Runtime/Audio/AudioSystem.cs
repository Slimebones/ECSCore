using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Key;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.Utils;
using UnityEngine;

namespace Slimebones.ECSCore.Audio
{
    public class AudioSystem: ISystem
    {
        public Filter reqf;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            reqf = World.Filter.With<SetAudioReq>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var reqe in reqf)
            {
                ref var reqc = ref reqe.GetComponent<SetAudioReq>();
                Entity audioE;
                try
                {
                    audioE = KeyUtils.GetEntity(
                        reqc.key, World.Filter.With<Audio>()
                    );
                }
                catch (NotFoundException)
                {
                    Log.Error(
                        "cannot found entity with key {0} for an audio"
                        + " request => skip",
                        reqc.key
                    );
                    continue; 
                }

                var audioSourceUnity = GameObjectUtils.GetUnityOrError(
                    audioE
                ).GetComponent<AudioSource>();

                if (reqc.volume != null)
                {
                    audioSourceUnity.volume = (float)reqc.volume;
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
