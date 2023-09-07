using Scellecs.Morpeh;
using System;
using System.Linq;
using static Slimebones.ECSCore.Utils.Delegates;

namespace Slimebones.ECSCore.Mouse {
    public static class MouseUtils {
        /// <summary>
        /// Executes action for each MouseEvent.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="world"></param>
        /// <param name="allowedTypes"></param>
        public static void ExecuteForEachEvent(
            ActionRef<MouseEvent> action,
            World world,
            MouseEventType[] allowedTypes = null
        ) {
            Filter eventF = world.Filter.With<MouseEvent>();

            foreach (Entity eventE in eventF) {
                ref MouseEvent mouseEvent =
                    ref eventE.GetComponent<MouseEvent>();

                if (!isAllowedType(mouseEvent.type, allowedTypes)) {
                    return;
                }

                action(ref mouseEvent);
            }
        }

        /// <summary>
        /// Executes action for each mouse event where target entity has
        /// TComponent.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="action"></param>
        /// <param name="world"></param>
        /// <param name="allowedTypes"></param>
        public static void ExecuteForEachEvent<TComponent>(
            ActionRef<MouseEvent, TComponent> action,
            World world,
            MouseEventType[] allowedTypes = null
        ) where TComponent : struct, IComponent {
            // create action wrapper for less detailed overload of this method
            ActionRef<MouseEvent> actionWrapper =
                (ref MouseEvent mouseEvent) => {
                    Entity targetE = mouseEvent.targetEntity;
                    ref TComponent targetComponent =
                        ref targetE.GetComponent<TComponent>(
                            out bool isTargetHostComponent
                        );
                    if (isTargetHostComponent) {
                        action(ref mouseEvent, ref targetComponent);
                    }
                };
            ExecuteForEachEvent(actionWrapper, world, allowedTypes);
        }

        private static bool isAllowedType(
            MouseEventType type,
            MouseEventType[] allowedTypes
        ) {
            if (allowedTypes == null) {
                return true;
            }
            return allowedTypes.Contains(type);
        }
    }
}