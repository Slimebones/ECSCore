using Scellecs.Morpeh;
using Slimebones.ECSCore.Collision;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Slimebones.ECSCore.Condition
{
    [Serializable]
    public class TriggerCondition: ICondition
    {
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
        public GameObject stateSliderParent;

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
                return Trigger();
            }

            Filter collisionEventF =
                world.Filter.With<CollisionEvent>().Build();

            foreach (var eventE in collisionEventF)
            {
                ref var collisionEvent =
                    ref eventE.GetComponent<CollisionEvent>();

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
                        return Trigger();
                    }

                    if (collisionEvent.type == CollisionEventType.Exit)
                    {
                        isEntered = false;
                        if (stateSliderParent != null)
                        {
                            stateSliderParent.SetActive(false);
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

        private bool Trigger()
        {
            if (!isEntered)
            {
                if (stateSliderParent != null)
                {
                    stateSliderParent.SetActive(true);
                    stateSliderParent
                        .GetComponentInChildren<Slider>()
                        .value = 0;
                }

                isEntered = true;

                // set last triggered time even without an actual trigger
                // since we want to calculate first period check
                lastTriggeredTime = Time.time;
                // trigger if the first trigger is not supposed to be
                // after the first period
                return isFirstTriggerWithoutPeriod;
            }

            if (stateSliderParent != null)
            {
                stateSliderParent.GetComponentInChildren<Slider>().value =
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