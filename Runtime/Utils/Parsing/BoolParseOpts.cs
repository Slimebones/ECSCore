#nullable enable

using System;

namespace Slimebones.ECSCore.Utils.Parsing
{
    public class BoolParseOpts: IParseOpts<bool>
    {
        private readonly bool? min;
        private readonly bool? max;
        private readonly int? precision;

        public BoolParseOpts(
            bool? min = null,
            bool? max = null,
            int? precision = null
        )
        {
            this.min = min;
            this.max = max;
            this.precision = precision;
        }

        public bool? Min
        {
            get => min;
        }

        public bool? Max
        {
            get => max;
        }

        public int? Precision
        {
            get => precision;
        }
    }
}