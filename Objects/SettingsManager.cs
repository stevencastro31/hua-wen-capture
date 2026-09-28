using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HuwWenCapture.Objects {
    internal static class SettingsManager {
        public static bool StartMinimized { get; set; }
        private static string? _captureHotkey;
        private static string? _captureDirectory;

        public static string CaptureHotkey { 
            get => _captureHotkey; 
            set {
                bool check = Regex.IsMatch(value.ToUpperInvariant(), @"^(?!.*\b(ALT|CTRL|SHIFT)\+\1\+)(?:(?:ALT|CTRL|SHIFT)\+){1,3}[A-Z]$");
                if (!check) throw new Exception("invalid hotkey configuration");
                _captureHotkey = value;
            }
        }
        public static string CaptureDirectory {
            get => _captureDirectory;
            set {
                try {
                    string fullPath = Path.GetFullPath(value);
                    if (!Directory.Exists(fullPath))
                        Directory.CreateDirectory(fullPath);
                    _captureDirectory = fullPath;
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        static SettingsManager() {
            StartMinimized = Properties.Settings.Default.StartMinimized;
            CaptureDirectory = ResolveCapturePath();
            CaptureHotkey = Properties.Settings.Default.CaptureHotkey;
        }

        private static string ResolveCapturePath() {
            string capturePath;
            if (Properties.Settings.Default.CaptureDirectory == "SPECIAL_PICTURES")
                capturePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "HuaWenCapture");
            else
                capturePath = Properties.Settings.Default.CaptureDirectory;
            if (!Directory.Exists(capturePath))
                Directory.CreateDirectory(capturePath);
            return capturePath;
        }

        public static void Save() {
            Properties.Settings.Default.StartMinimized = StartMinimized;
            string defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "HuaWenCapture");
            if (defaultPath == CaptureDirectory)
                Properties.Settings.Default.CaptureDirectory = "SPECIAL_PICTURES";
            else
                Properties.Settings.Default.CaptureDirectory = CaptureDirectory;
            Properties.Settings.Default.CaptureHotkey = CaptureHotkey;
            Properties.Settings.Default.Save();
        }
    }
}
