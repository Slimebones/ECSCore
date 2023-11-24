using System;

namespace Slimebones.ECSCore.Utils.Parsing
{
    public static class ParsingUtils
    {
        public static bool Parse<T>(
            string valueStr,
            IParseOpts<T> opts,
            out IParseRes<T> res
        ) where T: struct
        {
            if (
                typeof(T) != typeof(int)
                || typeof(T) != typeof(float)
                || typeof(T) != typeof(bool)
            )
            {
                throw new NotFoundException(
                    "parser for type",
                    typeof(T).ToString()
                );
            }

            return Parse(valueStr, opts, out res);
        }

        public static bool Parse(
            string valueStr,
            IntParseOpts opts,
            out IntParseRes res
        )
        {
            res = new IntParseRes();

            try
            {
                res.Value = int.Parse(valueStr);
            }
            catch
            {
                return false;
            }

            if (
                opts.Max != null
                && res.Value > opts.Max
            )
            {
                res.IsOutAnyLimit = true;
                res.IsOutMaxLimit = true;
                res.Value = (int)opts.Max;
                return true;
            }
            if (
                opts.Min != null
                && res.Value < opts.Min
            )
            {
                res.IsOutAnyLimit = true;
                res.IsOutMinLimit = true;
                res.Value = (int)opts.Min;
                return true;
            }

            return true;
        }

        public static bool Parse(
            string valueStr,
            FloatParseOpts opts,
            out FloatParseRes res
        )
        {
            res = new FloatParseRes();

            try
            {
                res.Value = float.Parse(valueStr);
                if (opts.Precision != null)
                {
                    res.Value = (float)Math.Round(
                        res.Value,
                        (int)opts.Precision
                    );
                }
            }
            catch
            {
                return false;
            }

            if (
                opts.Max != null
                && res.Value > opts.Max
            )
            {
                res.IsOutAnyLimit = true;
                res.IsOutMaxLimit = true;
                res.Value = (int)opts.Max;
                return true;
            }
            if (
                opts.Min != null
                && res.Value < opts.Min
            )
            {
                res.IsOutAnyLimit = true;
                res.IsOutMinLimit = true;
                res.Value = (int)opts.Min;
                return true;
            }

            return true;
        }

        public static bool Parse(
            string valueStr,
            BoolParseOpts opts,
            out BoolParseRes res
        )
        {
            res = new BoolParseRes();

            if (valueStr != "0" && valueStr != "1")
            {
                return false;
            }

            res.Value = valueStr == "1";
            return true;
        }

   }

}