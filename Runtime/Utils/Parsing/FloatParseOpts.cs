#nullable enable

using System;

namespace Slimebones.ECSCore.Utils.Parsing
{
    public class FloatParseOpts: IParseOpts<float>
    {
        private readonly float? min;
        private readonly float? max;
        private readonly int? precision;

        public FloatParseOpts(
            float? min = null,
            float? max = null,
            int? precision = null
        )
        {
            this.min = min;
            this.max = max;
            this.precision = precision;
        }

        public float? Min
        {
            get => min;
        }

        public float? Max
        {
            get => max;
        }

        public int? Precision
        {
            get
            {
                return precision;
            }
        }
    }
}