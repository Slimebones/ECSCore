using System;
using UnityEngine;

namespace Slimebones.ECSCore.Utils
{
    [Serializable]
    public class StrToInt: IObjToObj
    {
        [SerializeField]
        private string key;
        [SerializeField]
        private int value;

        public object Key
        {
            get => key;
            set => this.key = (string) value;
        }

        public object Value
        {
            get => value;
            set => this.value = (int) value;
        }
    }
}