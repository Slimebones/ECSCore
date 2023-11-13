using Scellecs.Morpeh;
using UnityEngine.Events;
using UnityUI = UnityEngine.UI;

namespace Slimebones.ECSCore.UI
{
    /// <summary>
    /// Attached to button components in order to response to onClick and
    /// maybe other events.
    /// </summary>
    public interface IButtonListener
    {
        // subscriber pattern: https://stackoverflow.com/a/63490134
        public void Subscribe(
            Entity e, UnityUI.Button unityButton, World world
        );
        public void Unsubscribe();
    }
}