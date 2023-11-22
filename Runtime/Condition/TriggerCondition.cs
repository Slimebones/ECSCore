using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Collision;
using Slimebones.ECSCore.UI.Canvas;
using Slimebones.ECSCore.UI.Panel;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Slimebones.ECSCore.Condition
{
    [Serializable]
    public class TriggerCondition: ICondition
    {
        public UnityEngine.Collider hostCollider;
        public UnityEngine.Collider trigger;

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

        [Tooltip(
            "Parent object of the slider where the time to the next spawn"
            + " will be streamed. The parent object itself will be enabled"
            + " only on preparation start."
        )]
        public GameObjectDataComponent stateSliderPanel;

        public bool isAlwaysTrueWhileEntered = false;

        [HideInInspector]
        public float lastTriggeredTime = 0f;

        private bool isEntered = false;

        public bool Check(
            Entity e,
            World world
        )
        {
            if (trigger == null)
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

                if (hostCollider != null)
                {
                    UnityEngine.Collider factHostCollider =
                        collisionEvent.hostCollider;
                        

                    if (factHostCollider == null)
                    {
                        // also skip if game object found, but there is no
                        // collider on it, but it is not possible due to
                        // how the collisions work
                        Debug.LogWarningFormat(
                            "cannot get collider of host entity {0}, but"
                            + "the collision is somehow happened"
                        );
                        continue;
                    }

                    if (
                        factHostCollider.GetInstanceID()
                            != hostCollider.GetInstanceID()
                    )
                    {
                        // unmatched colliders
                        continue;
                    }
                }

                // check if the same collider and the same host entity as
                // allowed
                if (
                    collisionEvent.collider != null
                    && (
                        collisionEvent.collider.GetInstanceID()
                            == trigger.GetInstanceID()
                    )
                )
                {
                    if (collisionEvent.type == CollisionEventType.Stay)
                    {
                        return Trigger(world);
                    }

                    if (collisionEvent.type == CollisionEventType.Exit)
                    {
                        isEntered = false;
                        if (stateSliderPanel != null)
                        {
                            SetStateSliderPanel(false, world);
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

        private void SetStateSliderPanel(bool isActive, World world)
        {
            if (isActive)
            {
                PanelUtils.Move<MainCanvas>(
                    stateSliderPanel.Entity, world
                );
                return;
            }
            PanelUtils.Move<DisabledCanvas>(
                stateSliderPanel.Entity, world
            );
        }

        private bool Trigger(World world)
        {
            if (!isEntered)
            {
                if (stateSliderPanel != null)
                {
                    SetStateSliderPanel(true, world);
                }

                isEntered = true;

                // set last triggered time even without an actual trigger
                // since we want to calculate first period check
                lastTriggeredTime = Time.time;
                // trigger if the first trigger is not supposed to be
                // after the first period
                return isFirstTriggerWithoutPeriod;
            }

            if (stateSliderPanel != null)
            {
                stateSliderPanel.GetComponentInChildren<Slider>().value =
                    Mathf.Lerp(
                        0f,
                        1f,
                        (Time.time - lastTriggeredTime) / triggerStayPeriod
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
    }
}