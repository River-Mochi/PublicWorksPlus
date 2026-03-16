// File: Utils/ShellOpen.cs
// Purpose: Cross-platform-ish folder opening helper for Options UI buttons.
// Notes:
// - First attempt: Unity Application.OpenURL with a file:// URI.
// - Fallback: OS shell open (Windows/macOS/Linux).
// - Safe: catches exceptions; no crash on failure.

namespace DispatchBoss
{
    using Colossal.PSI.Environment;
    using System;
    using System.Diagnostics;
    using System.IO;
    using UnityEngine;

    internal static class ShellOpen
    {
        internal static void OpenFolderSafe(string folderPath, string logLabel)
        {
            try
            {
                if (string.IsNullOrEmpty(folderPath))
                {
                    Mod.s_Log.Info($"{Mod.ModTag} {logLabel}: folder path is empty.");
                    return;
                }

                string fullPath = Path.GetFullPath(folderPath);

                if (!Directory.Exists(fullPath))
                {
                    Mod.s_Log.Info($"{Mod.ModTag} {logLabel}: folder not found: {fullPath}");
                    return;
                }

                if (TryOpenWithUnityFileUrl(fullPath))
                {
                    return;
                }

                TryOpenWithOsShell(fullPath);
            }
            catch (Exception ex)
            {
                Mod.s_Log.Warn($"{Mod.ModTag} {logLabel}: failed opening folder: {ex.GetType().Name}: {ex.Message}");
            }
        }

        internal static string GetLogsFolder()
        {
            return Path.Combine(EnvPath.kUserDataPath, "Logs");
        }

        internal static string GetModsDataFolder()
        {
            return Path.Combine(EnvPath.kUserDataPath, "ModsData", Mod.ModId);
        }

        private static bool TryOpenWithUnityFileUrl(string fullPath)
        {
            try
            {
                string path = fullPath;

                if (!path.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal) &&
                    !path.EndsWith(Path.AltDirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                {
                    path += Path.DirectorySeparatorChar;
                }

                string uri = new Uri(path).AbsoluteUri;
                Application.OpenURL(uri);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void TryOpenWithOsShell(string fullPath)
        {
            try
            {
                RuntimePlatform platform = Application.platform;

                if (platform == RuntimePlatform.WindowsPlayer || platform == RuntimePlatform.WindowsEditor)
                {
                    ProcessStartInfo psi = new ProcessStartInfo(fullPath)
                    {
                        UseShellExecute = true,
                        ErrorDialog = false,
                        Verb = "open",
                    };

                    Process.Start(psi);
                    return;
                }

                if (platform == RuntimePlatform.OSXPlayer || platform == RuntimePlatform.OSXEditor)
                {
                    ProcessStartInfo psi = new ProcessStartInfo("open", QuoteArg(fullPath))
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    };

                    Process.Start(psi);
                    return;
                }

                if (platform == RuntimePlatform.LinuxPlayer || platform == RuntimePlatform.LinuxEditor)
                {
                    ProcessStartInfo psi = new ProcessStartInfo("xdg-open", QuoteArg(fullPath))
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    };

                    Process.Start(psi);
                    return;
                }

                ProcessStartInfo psiGeneric = new ProcessStartInfo(fullPath)
                {
                    UseShellExecute = true,
                    ErrorDialog = false,
                };

                Process.Start(psiGeneric);
            }
            catch (Exception ex)
            {
                Mod.s_Log.Warn($"{Mod.ModTag} ShellOpen: OS fallback failed: {ex.GetType().Name}: {ex.Message}");
            }
        }

        private static string QuoteArg(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "\"\"";
            }

            if (s.IndexOf(' ') >= 0 || s.IndexOf('\t') >= 0)
            {
                return "\"" + s.Replace("\"", "\\\"") + "\"";
            }

            return s;
        }
    }
}
