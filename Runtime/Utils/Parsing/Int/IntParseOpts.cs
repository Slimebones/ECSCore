#nullable enable

using System;

namespace Slimebones.ECSCore.Utils.Parsing
{
    public class IntParseOpts: IParseOpts<int>
    {
        public int? min;
        public int? max;

        public IntParseOpts(
            int? min = null,
            int? max = null
        )
        {
            this.min = min;
            this.max = max;
        }

        public int Min
        {
            get
            {
                if (min == null)
                {
                    throw new CannotBeNullException("min");
                }
                return (int)min;
            }
        }

        public int Max
        {
            get
            {
                if (min == null)
                {
                    throw new CannotBeNullException("max");
                }
                return (int)min;
            }
        }
    }
}