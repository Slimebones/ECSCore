using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using Slimebones.ECSCore.Collision;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Slimebones.ECSCore.Condition
{
    [System.Serializable]
    public class TriggerCondition: ICondition<Filter>
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

        [HideInInspector]
        public float lastTriggeredTime = 0f;

        private bool isEntered = false;

        public bool Check(
            Entity e,
            World world,
            Filter hostEntitiesF = null
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
                    && (
                        hostEntitiesF != null
                        && IsHostEntityAllowed(
                            collisionEvent.hostEntity,
                            hostEntitiesF
                        )
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

            return false;
        }

        private bool IsHostEntityAllowed(Entity e, Filter allowedEntities)
        {
            foreach (var aE in allowedEntities)
            {
                if (e == aE)
                {
                    return true;
                }
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

            if (Time.time - lastTriggeredTime > triggerStayPeriod)
            {
                lastTriggeredTime = Time.time;
                return true;
            }

            return false;
        }
    }
}