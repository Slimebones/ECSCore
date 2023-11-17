using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.UI.Canvas;
using UnityEngine;

namespace Slimebones.ECSCore.UI.Panel
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
            where T : struct, ICanvasComponent
        {
            ref var request =
                ref RequestComponentUtils.Create<MoveToCanvasRequest>(
                    1,
                    world
                );
            request.targetE = e;
            request.canvasE = world.Filter.With<T>().Build().First();
        }

        /// <summary>
        /// Moves entity to either enabled or disabled canvas depending on
        /// given flag.
        /// </summary>
        /// <param name="isEnabled"></param>
        /// <param name="e"></param>
        /// <param name="world"></param>
        public static void DecideMove(bool isEnabled, Entity e, World world)
        {
            if (isEnabled)
            {
                Move<EnabledCanvas>(e, world);
                return;
            }
            Move<DisabledCanvas>(e, world);
        }
    }
}
