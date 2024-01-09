using Scellecs.Morpeh;
using UnityEngine;

namespace Slimebones.ECSCore.React
{
    public interface IKeyedGOListener
    {
        public void Subscribe(
            string key, UnityEngine.GameObject go, World world
        );
        public void Unsubscribe();
    }
}