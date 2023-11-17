using UnityEngine;

namespace Slimebones.ECSCore.Logging
{
    // TODO(ryzhovalex):
    //      for now here only debug writes, but we want to add an advanced
    //      logging support
    public static class Log
    {
        public static void Info(string message, params object[] format)
        {
            Info(string.Format(message, format));
        }

        public static void Info(string message)
        {
            Debug.Log(message);
        }

        public static void Warning(string message, params object[] format)
        {
            Warning(string.Format(message, format));
        }

        public static void Warning(string message)
        {
            Debug.LogWarning(message);
        }

        public static void Error(string message, params object[] format)
        {
            Error(string.Format(message, format));
        }

        public static void Error(string message)
        {
            Debug.LogError(message);
        }
    }
}