using Scellecs.Morpeh;
using Slimebones.ECSCore.Event;
using Slimebones.ECSCore.Storage;
using System.Collections.Generic;
using static Slimebones.ECSCore.Utils.Delegates;

namespace Slimebones.ECSCore.Input
{
    /// <summary>
    /// Generates input events listening to retrieved InputActions.
    /// </summary>
    public class InputSystem: ISystem
    {
        public Filter inputSpecStorageF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            inputSpecStorageF = World.Filter.With<InputSpecStorage>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            ref var specStorage =
                ref StorageUtils.Get<InputSpecStorage>();

            if (!specStorage.isEnabled)
            {
                return;
            }

            // TODO(ryzhovalex):
            //      event checking can be concurred, but for each type
            //      separately

            for (int i = 0; i < specStorage.specs.Count; i++)
            {
                if (specStorage.disabledSpecIndexes.Contains(i))
                {
                    continue;
                }

                var spec = specStorage.specs[i];
                foreach (var kvp in spec.map)
                {
                    if (kvp.Value())
                    {
                        ref var inputEvt =
                            ref EventUtils.Create<InputEvent>();
                        inputEvt.type = kvp.Key;
                        inputEvt.name = spec.code;
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
