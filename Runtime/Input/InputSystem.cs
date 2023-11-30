using Scellecs.Morpeh;
using Slimebones.ECSCore.Base.Event;
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
            ref var inputSpecStorage =
                ref inputSpecStorageF
                .First()
                .GetComponent<InputSpecStorage>();

            // TODO(ryzhovalex):
            //      event checking can be concurred, but for each type
            //      separately

            foreach (var spec in inputSpecStorage.specs)
            {
                foreach (var kvp in spec.map)
                {
                    if (kvp.Value())
                    {
                        ref var inputEvt =
                            ref EventUtils.Create<InputEvt>(World);
                        inputEvt.type = kvp.Key;
                        inputEvt.name = spec.name;
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
