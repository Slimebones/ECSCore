using System;

namespace Slimebones.ECSCore.Utils
{
    public static class NumUtils
    {
        public static float TruncateFloat(float value, int precision)
        {
            // ref: https://stackoverflow.com/a/25451689
            double res = value - (value % (1 / Math.Pow(10, precision)));
            return (float)res;
        }
    }
}