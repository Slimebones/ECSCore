using Scellecs.Morpeh;
using Slimebones.ECSCore.Bridging;
using Slimebones.ECSCore.Condition;
using Slimebones.ECSCore.Object;
using UnityEngine;

namespace Slimebones.ECSCore.Actors.Cannon
{
    public class ObjectCannonSystem: ISystem
    {
        private Filter objectCannonF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            objectCannonF = World.Filter.With<ObjectCannon>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var e in objectCannonF)
            {
                ref var c = ref e.GetComponent<ObjectCannon>();

                if (
                    c.conditions == null
                    || ConditionUtils.All(
                        c.conditions,
                        e
                    )
                )
                {
                    var cGo = GoUtils.GetUnity(e);

                    Spawn(e, ref c, cGo);
                    continue;
                }
            }
        }

        public void Dispose()
        {
        }

        private bool Spawn(
            Entity cannonE,
            ref ObjectCannon cannon,
            GameObject cannonGO
        )
        {
            // check spawned count
            if (
                cannon.maxSimultaneousObjects >= 0
                && cannon.maxSimultaneousObjects <= cannon.livingObjectsCount
            )
            {
                return false;
            }

            GameObject spawnedGO = UnityEngine.Object.Instantiate(
                cannon.spawnedPrefab,
                cannonGO.transform.position,
                Quaternion.identity
            );
            spawnedGO.SetActive(true);
            cannon.livingObjectsCount++;
            var onDestroyBridge = spawnedGO.AddComponent<OnDestroyBridge>();
            onDestroyBridge.action = (OnDestroyBridge bridge) =>
                {
                    if (!cannonE.IsNullOrDisposed())
                    {
                        ref var cannon =
                            ref cannonE.GetComponent<ObjectCannon>();
                        cannon.livingObjectsCount--;
                    }
                };
            spawnedGO.GetComponent<Rigidbody>().AddForce(
                cannonGO.transform.forward * cannon.outForce
            );

            if (cannon.spawnedObjectLifetime >= 0)
            {
                UnityEngine.Object.Destroy(
                    spawnedGO, cannon.spawnedObjectLifetime
                );
            }

            return true;
        }
    }
}
