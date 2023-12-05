using Scellecs.Morpeh;
using Slimebones.ECSCore.Condition;
using Slimebones.ECSCore.GO;
using Slimebones.ECSCore.Utils;
using UnityEngine;

namespace Slimebones.ECSCore.Actors.Door
{
    public class DoorSystem: ISystem
    {
        public Filter f;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            f = World.Filter.With<Door>().Build();

            // set initial positions and rotation of the doors
            foreach (var e in f)
            {
                ref var c = ref e.GetComponent<Door>();
                var go = GOUtils.GetUnity(e);

                c.initialPosition = go.transform.position;
                c.initialRotation = go.transform.rotation;
                c.openedPosition =
                    go.transform.position + c.openRelativePosition;
                // quaternions are multiplied instead of addition
                c.openedRotation =
                    go.transform.rotation
                    * Quaternion.Euler(
                        c.openRelativeRotation.x,
                        c.openRelativeRotation.y,
                        c.openRelativeRotation.z
                    );
            }
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var e in f)
            {
                ref var c = ref e.GetComponent<Door>();

                // perf: don't check doors which no more will open
                if (c.state == DoorState.Opened && !c.isBackClosed)
                {
                    continue;
                }

                bool areConditionsPositive =
                    c.openConditions == null
                    || ConditionUtils.All(
                        c.openConditions,
                        e,
                        World
                    );

                // change a door state if external conditions have been
                // changed
                switch (c.state)
                {
                    case DoorState.Closed:
                        if (areConditionsPositive)
                        {
                            c.state = DoorState.Opening;
                        }
                        break;
                    case DoorState.Opened:
                        if (c.isBackClosed && !areConditionsPositive)
                        {
                            c.state = DoorState.Closing;
                        }
                        break;
                    case DoorState.Opening:
                        // revert opening if an according flag is given and
                        // the conditions are no more positive
                        if (
                            c.isBackClosed
                            && !areConditionsPositive
                        )
                        {
                            c.state = DoorState.Closing;
                        }
                        break;
                    case DoorState.Closing:
                        if (areConditionsPositive)
                        {
                            c.state = DoorState.Opening;
                        }
                        break;
                }


                // in a separate instruction set move door in order to achieve
                // the same frame action, even after a state change
                switch (c.state)
                {
                    case DoorState.Opening:
                        ProcessOpening(e, ref c, deltaTime);
                        break;
                    case DoorState.Closing:
                        ProcessClosing(e, ref c, deltaTime);
                        break;
                }
            }
        }

        public void Dispose()
        {
        }

        private void ProcessOpening(
            Entity e,
            ref Door c,
            float deltaTime
        )
        {
            ProcessLerp(
                e,
                ref c,
                c.openedPosition,
                c.openedRotation,
                DoorState.Opened,
                deltaTime
            );
        }

        private void ProcessClosing(
            Entity e,
            ref Door c,
            float deltaTime
        )
        {
            ProcessLerp(
                e,
                ref c,
                c.initialPosition,
                c.initialRotation,
                DoorState.Closed,
                deltaTime
            );
        }

        private void ProcessLerp(
            Entity e,
            ref Door c,
            Vector3 toPosition,
            Quaternion toRotation,
            DoorState finalState,
            float deltaTime
        )
        {
            var go = GOUtils.GetUnity(e);
            go.transform.position = Vector3.Lerp(
                go.transform.position,
                toPosition,
                c.speed * deltaTime
            );
            go.transform.rotation = Quaternion.Lerp(
                go.transform.rotation,
                toRotation,
                c.speed * deltaTime
            );

            Quaternion q1 = go.transform.rotation;
            Quaternion q2 = toRotation;
            if (
                Vector3.Distance(
                    go.transform.position, toPosition
                ) < VectorConstants.VectorDistancePrecision
                &&
                    Mathf.Abs(q2.w - q1.w)
                        < VectorConstants.QuaternionDiffPrecision
                    && Mathf.Abs(q2.x - q1.x)
                        < VectorConstants.QuaternionDiffPrecision
                    && Mathf.Abs(q2.y - q1.y)
                        < VectorConstants.QuaternionDiffPrecision
                    && Mathf.Abs(q2.z - q1.z)
                        < VectorConstants.QuaternionDiffPrecision

            )
            {
                c.state = finalState;
            }
        }
    }
}
