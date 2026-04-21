// File: Utils/LogUtils.cs
// Shared version 0.4.0
//
// Purpose:
// Logging patterns not provided by CO wrapper to Unity logger:
// - Info/Warn/TryLog: lazy message construction inside try/catch
// - WarnOnce: prevents repeated WARN spam in hot paths
// - Popup-safe helpers: Exception objects are not attached to Info/Warn logs
// - Use instead of direct s_Log.Info/Warn during mod load, city load, and gameplay systems


namespace PublicWorksPlus
{
    using Colossal.Logging;
    using System;
    using System.Collections.Generic;

    public static class LogUtils
    {
        private static readonly object s_WarnOnceLock = new object();

        private static readonly HashSet<string> s_WarnOnceKeys =
            new HashSet<string>(StringComparer.Ordinal);

        private const int MaxWarnOnceKeys = 2048;

        public static void Info(ILog log, Func<string> messageFactory)
        {
            TryLog(log, Level.Info, messageFactory);
        }

        public static void Warn(ILog log, Func<string> messageFactory, Exception? exception = null)
        {
            TryLog(log, Level.Warn, messageFactory, exception);
        }

        public static bool WarnOnce(ILog log, string key, Func<string> messageFactory, Exception? exception = null)
        {
            if (log == null || string.IsNullOrEmpty(key) || messageFactory == null)
            {
                return false;
            }

            if (!log.isLevelEnabled(Level.Warn))
            {
                return false;
            }

            string fullKey = log.name + "|" + key;

            lock (s_WarnOnceLock)
            {
                if (s_WarnOnceKeys.Count >= MaxWarnOnceKeys)
                {
                    s_WarnOnceKeys.Clear();
                }

                if (!s_WarnOnceKeys.Add(fullKey))
                {
                    return false;
                }
            }

            TryLog(log, Level.Warn, messageFactory, exception);
            return true;
        }

        public static void TryLog(ILog log, Level level, Func<string> messageFactory, Exception? exception = null)
        {
            if (log == null || messageFactory == null)
            {
                return;
            }

            if (!log.isLevelEnabled(level))
            {
                return;
            }

            string message;
            try
            {
                message = messageFactory() ?? string.Empty;
            }
            catch (Exception ex)
            {
                SafeLogNoException(log, Level.Warn, "Log message factory threw: " + ex.GetType().Name + ": " + ex.Message);
                return;
            }

            try
            {
                // Keep warning/info logs plain text; attaching exceptions can surface UI popups.
                Exception? attach = exception != null && level == Level.Error ? exception : null;
                log.Log(level, message, attach!);
            }
            catch
            {
                // Logging must never break city loading or gameplay.
            }
        }

        private static void SafeLogNoException(ILog log, Level level, string message)
        {
            try
            {
                if (log != null && log.isLevelEnabled(level))
                {
                    log.Log(level, message, null!);
                }
            }
            catch
            {
            }
        }
    }
}
