using Scellecs.Morpeh;
using Slimebones.ClumsyDelivery.UI.Panel;
using Slimebones.ECSCore.Collision;
using Slimebones.ECSCore.Logging;
using Slimebones.ECSCore.UI.Panel;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Slimebones.ECSCore.Condition
{
    [Serializable]
    public class TriggerCondition: ICondition
    {
        // doesn't matter which collider is host
        public UnityEngine.Collider unityCollider1;
        public UnityEngine.Collider unityCollider2;

        [Tooltip(
            "How long the host object should stay in the trigger in order"
            + " to give a positive result. On trigger=null, this would mean"
            + " just normal period counting all the time."
        )]
        public float triggerStayPeriod;
        [Tooltip(
            "Whether the first trigger should be after a constant staying"
            + " in trigger delay. If trigger=null this option is not"
            + " considered."
        )]
        public bool isFirstTriggerWithoutPeriod;

        public Filter allowedEntitiesF;

        public PanelProviderData displayPanel;

        public bool isAlwaysTrueWhileEntered = false;

        [HideInInspector]
        public float lastTriggeredTime = 0f;

        private bool isEntered = false;

        public bool Check(
            Entity e,
            World world
        )
        {
            if (unityCollider1 == null || unityCollider2 == null)
            {
                // trigger as like it has been staying
                return Trigger(world);
            }

            Filter collisionEventF =
                world.Filter.With<CollisionEvent>().Build();

            foreach (var eventE in collisionEventF)
            {
                ref var collisionEvent =
                    ref eventE.GetComponent<CollisionEvent>();

                bool isCollider1InEvent = (
                    unityCollider1 != null
                    && (
                        collisionEvent.unityHostCollider == unityCollider1
                        || collisionEvent.unityGuestCollider == unityCollider1
                    )
                );
                bool isCollider2InEvent = (
                    unityCollider2 != null
                    && (
                        collisionEvent.unityHostCollider == unityCollider2
                        || collisionEvent.unityGuestCollider == unityCollider2
                    )
                );

                // check if the same collider and the same host entity as
                // allowed
                if (isCollider1InEvent && isCollider2InEvent)
                {
                    if (collisionEvent.type == CollisionEventType.Stay)
                    {
                        return Trigger(world);
                    }

                    if (collisionEvent.type == CollisionEventType.Exit)
                    {
                        isEntered = false;
                        if (IsDisplayPanelRefDefined())
                        {
                            SetDisplayPanelState(false, world);
                        }
                    }
                }
            }

            // do not return false if the trigger is already entered to avoid
            // problems with trigger listening in FPS updates, where
            // some collision events can be missed
            if (isAlwaysTrueWhileEntered)
            {
                return isEntered;
            }

            return false;
        }

        private void SetDisplayPanelState(bool isActive, World world)
        {
            if (isActive)
            {
                PanelUtils.Enable(
                    displayPanel.key,
                    world
                );
                return;
            }
            PanelUtils.Disable(
                displayPanel.key,
                world
            );
        }

        private bool Trigger(World world)
        {
            if (!isEntered)
            {
                if (IsDisplayPanelRefDefined())
                {
                    SetDisplayPanelState(true, world);
                }

                isEntered = true;

                // set last triggered time even without an actual trigger
                // since we want to calculate first period check
                lastTriggeredTime = Time.time;
                // trigger if the first trigger is not supposed to be
                // after the first period
                return isFirstTriggerWithoutPeriod;
            }

            if (IsDisplayPanelRefDefined())
            {
                displayPanel
                    .provider
                    .GetComponentInChildren<Slider>()
                    .value =
                        Mathf.Lerp(
                            0f,
                            1f,
                            (Time.time - lastTriggeredTime)
                                / triggerStayPeriod
                        );
            }

            if (
                triggerStayPeriod == 0
                || Time.time - lastTriggeredTime > triggerStayPeriod
            )
            {
                lastTriggeredTime = Time.time;
                return true;
            }

            return false;
        }

        public bool IsDisplayPanelRefDefined()
        {
            return (
                displayPanel != null
                && displayPanel.key != ""
                && displayPanel.provider != null
            );
        }
    }
}