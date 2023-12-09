using System;
using System.Collections.Generic;
using static Slimebones.ECSCore.Utils.Delegates;

namespace Slimebones.ECSCore.Input
{
    /// <summary>
    /// Holds information about a specific inputt binding.
    /// </summary>
    public struct InputSpec
    {
        public string code;
        /// <summary>
        /// Dict binding input event types to checking functions.
        /// </summary>
        /// <remarks>
        /// If some event type is undefined, no checks will be performed for
        /// that type.
        /// </remarks>
        public Dictionary<InputEventType, ReturnFunc<bool>> map;

        public InputSpec(
            string code,
            Dictionary<InputEventType, ReturnFunc<bool>> map
        )
        {
            this.code = code;
            this.map = map;
        }
    }
}