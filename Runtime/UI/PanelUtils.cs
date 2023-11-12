using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using UnityEngine;

namespace Slimebones.ECSCore.UI
{
    public static class PanelUtils
    {
        /// <summary>
        /// Moves a panel entity to another canvas.
        /// </summary>
        /// <remarks>
        /// Entity should contain GameObjectData.
        /// </remarks>
        public static void Move<T>(Entity e, World world)
            where T: struct, ICanvasComponent
        {
            ref var request =
                ref RequestComponentUtils.Create<MoveToCanvasRequest>(
                    1, 
                    world
                );
            request.targetE = e;
            request.canvasE = world.Filter.With<T>().Build().First();
        }
    }
}
