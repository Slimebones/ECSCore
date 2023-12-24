#nullable enable

using System;

namespace Slimebones.ECSCore.Utils.Parsing
{
    public class IntParseOpts: IParseOpts<int>
    {
        private readonly int? min;
        private readonly int? max;
        private readonly int? precision;

        public IntParseOpts(
            int? min = null,
            int? max = null,
            int? precision = null
        )
        {
            this.min = min;
            this.max = max;
            this.precision = precision;
        }

        public int? Min
        {
            get => min;
        }

        public int? Max
        {
            get => max;
        }

        public int? Precision
        {
            get => precision;
        }
    }
}