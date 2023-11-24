namespace Slimebones.ECSCore.Utils.Parsing
{
    public static class ParsingUtils
    {
        public static bool Parse(
            string valueStr,
            IntParseOpts opts,
            out IntParseRes res
        )
        {
            res = new IntParseRes();

            try
            {
                res.value = int.Parse(valueStr);
            }
            catch
            {
                return false;
            }

            if (opts.max != null && res.value > opts.max)
            {
                res.isOutOfAnyLimit = true;
                res.isOutOfMaxLimit = true;
                res.value = (int)opts.max;
                return true;
            }
            if (opts.min != null && res.value < opts.min)
            {
                res.isOutOfAnyLimit = true;
                res.isOutOfMinLimit = true;
                res.value = (int)opts.min;
                return true;
            }

            return true;
        }
    }

}