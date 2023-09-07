using System;


namespace Slimebones.ECSCore.Utils {
    public class Dt {
        public static long GetUtcTimestamp() {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
