using System;
using UnityEngine;

namespace Slimebones.ECSCore.Utils
{
    [Serializable]
    public class FloatToInt: IObjToObj
    {
        [SerializeField]
        private float key;
        [SerializeField]
        private int value;

        public object Key
        {
            get => key;
            set => key = (float) value;
        }

        public object Value
        {
            get => value;
            set => this.value = (int) value;
        }
    }
}