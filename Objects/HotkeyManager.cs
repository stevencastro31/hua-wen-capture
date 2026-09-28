using AntdUI;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace HuwWenCapture.Objects {
    internal static class HotkeyManager {
        private const int HOTKEY_ID = 1;    // currently there is only one hotkey

        private const uint MOD_ALT = 0x0001;
        private const uint MOD_CONTROL = 0x0002;
        private const uint MOD_SHIFT = 0x0004;
        
        public static Control Form { get; private set; }

        public static void SetForm(Control form) {
            Form = form;
            UpdateHotkey();
        }

        public static void UpdateHotkey() {
            UnregisterAppHotkeys();
            RegisterAppHotkeys();
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // add/remove application hotkeys
        private static bool RegisterAppHotkeys() {
            if (Form == null) throw new Exception("no form is set to register hotkeys with.");

            string[] keys = SettingsManager.CaptureHotkey.Split("+");
            (uint modifier, Keys hotkey) = PrepareHotkey(keys);

            return RegisterHotKey(Form.Handle, HOTKEY_ID, modifier, (uint)hotkey);
        }

        private static void UnregisterAppHotkeys() {
            if (Form == null) return;
            UnregisterHotKey(Form.Handle, HOTKEY_ID);
        }

        private static (uint, Keys) PrepareHotkey(string[] keys) {
            uint modifier = 0;
            Keys hotkey = Keys.None;

            foreach (string key in keys) {
                switch (key.Trim().ToUpperInvariant()) {
                    case "CTRL": modifier |= MOD_CONTROL;
                        break;
                    case "ALT":
                        modifier |= MOD_ALT;
                        break;
                    case "SHIFT":
                        modifier |= MOD_SHIFT;
                        break;
                    default:
                        if (Enum.TryParse(key, true, out Keys parsedKey))
                            hotkey = parsedKey;
                        break;
                }
            }
            return (modifier, hotkey);
        }
    }
}
