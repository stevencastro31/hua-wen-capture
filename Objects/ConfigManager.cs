using Google.Protobuf.WellKnownTypes;
using HuaWenCapture.Types;
using System.Diagnostics;
using System.Text.Json;

namespace HuaWenCapture.Objects {
    internal static class ConfigManager {
        private static readonly string CONFIG_PATH = Path.Combine(AppContext.BaseDirectory, "config.json");

        private static readonly bool DEFAULT_START_MINIMIZED = false;
        private static readonly string DEFAULT_CAPTURE_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "HuaWenCapture");
        private static readonly Gesture DEFAULT_CAPTURE_HOTKEY = new(Keys.Q, Mod.Alt | Mod.Shift);

        private static Config _config;

        static ConfigManager() {
            _config = LoadConfig();
        }

        public static bool GetIsStartMinimized() => _config.IsStartMinimized;
        public static string GetCapturePath() => _config.CapturePath;
        public static Gesture GetCaptureHotkey() => _config.CaptureHotkey;

        public static void SetIsStartMinimized(bool minimized) {
            _config.IsStartMinimized = minimized;
        }

        public static void SetCapturePath(string path) {
            string fullPath = Path.GetFullPath(path);
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            _config.CapturePath = fullPath;
        } 

        public static void SetCaptureHotkey(Gesture gesture) {
            _config.CaptureHotkey = gesture;
        }

        // methods
        private static Config LoadConfig() {
            if (!File.Exists(CONFIG_PATH)) {
                return CreateDefaultConfig();
            } else {
                string json = File.ReadAllText(CONFIG_PATH);
                return JsonSerializer.Deserialize<Config>(json)!;
            }
        }

        public static void SaveConfig(Config config) {
            string json = JsonSerializer.Serialize(config, new JsonSerializerOptions() { WriteIndented = true, IncludeFields = true });
            File.WriteAllText(CONFIG_PATH, json);
        }

        public static void SaveConfig() {
            if (_config == null) return;
            string json = JsonSerializer.Serialize(_config, new JsonSerializerOptions() { WriteIndented = true, IncludeFields = true });
            File.WriteAllText(CONFIG_PATH, json);
        }

        private static Config CreateDefaultConfig() {
            Config config = new(DEFAULT_START_MINIMIZED, DEFAULT_CAPTURE_PATH, DEFAULT_CAPTURE_HOTKEY);
            SaveConfig(config);
            return config;
        }
    }
}
