using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;

namespace Slimebones.ECSCore.Controller
{
    public class ControllerSystem: ISystem
    {
        private Filter controlledF;
        private Filter specStorageF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            controlledF = World.Filter.With<Controlled>().Build();
            specStorageF = World.Filter.With<ControllerSpecStorage>().Build();
            
            ref var specStorage =
                ref specStorageF.First().GetComponent<ControllerSpecStorage>();

            foreach (var kvp in specStorage.spec)
            {
                kvp.Value.Init(kvp.Key, World);
            }
        }

        public void OnUpdate(float deltaTime)
        {
            ref var specStorage =
                ref specStorageF.First().GetComponent<ControllerSpecStorage>();

            foreach (var e in controlledF)
            {
                ref var c = ref e.GetComponent<Controlled>();

                foreach (var key in c.keys)
                {
                    IController controller;
                    try
                    {
                        controller = specStorage.spec[key];
                    }
                    catch (KeyNotFoundException)
                    {
                        Debug.LogWarningFormat(
                            "no controller found for key {0},"
                                + " for entity id {1}",
                            key,
                            e.ID.ToString()
                        );
                        continue;
                    }

                    controller.Update(deltaTime, e);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
