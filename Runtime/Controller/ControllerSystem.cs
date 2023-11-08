using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;

namespace Slimebones.ECSCore.Controller
{
    public class ControllerSystem: ISystem
    {
        private Filter controlledF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            controlledF = World.Filter.With<Controlled>().Build();

            foreach (var e in controlledF)
            {
                ref var c = ref e.GetComponent<Controlled>();

                foreach (var controller in c.controllers)
                {
                    controller.Init(World);
                }
            }
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var e in controlledF)
            {
                ref var c = ref e.GetComponent<Controlled>();

                foreach (var controller in c.controllers)
                {
                    controller.Update(deltaTime, e);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
