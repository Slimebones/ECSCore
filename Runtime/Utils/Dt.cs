using System;


namespace Slimebones.ECSCore.Utils {
    public class DT {
        public static long GetUTCTimestamp() {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
