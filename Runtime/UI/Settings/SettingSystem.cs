using Scellecs.Morpeh;
using Slimebones.ECSCore.Base;
using UnityEngine.UI;
using UnityUI = UnityEngine.UI;

namespace Slimebones.ECSCore.UI.Settings
{
    /// <summary>
    /// Adds listeners to buttons.
    /// </summary>
    public class SettingSystem: ISystem
    {
        private Filter dropdownSettingF;

        public World World
        {
            get; set;
        }

        public void OnAwake()
        {
            InitDropdown();
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void Dispose()
        {
        }

        private void InitDropdown()
        {
            dropdownSettingF = World.Filter.With<DropdownSetting>().Build();

            foreach (var e in dropdownSettingF)
            {
                ref var c = ref e.GetComponent<DropdownSetting>();
                c.mainListener = new DropdownSettingListener();
                c.mainListener.Subscribe(e, World);
            }
        }
    }
}
