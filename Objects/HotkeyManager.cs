using AntdUI;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;

namespace HuwWenCapture.Objects {
    internal static class HotkeyManager {
        private const int HOTKEY_ID = 1;    // currently the only hotkey

        private const uint MOD_CONTROL = 0x0002;
        private const uint MOD_ALT = 0x0001;
        private const uint MOD_SHIFT = 0x0004;

        public static Control Form { get; private set; }

        public static void SetForm(Control form) {
            Unregister();
            Form = form;
            Register();
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private static bool Register() {
            if (Form == null) throw new Exception("no form is set to register hotkeys with.");
            return RegisterHotKey(Form.Handle, HOTKEY_ID, MOD_ALT | MOD_SHIFT, (uint)Keys.Q);
        }

        private static void Unregister() {
            if (Form == null) return;
            UnregisterHotKey(Form.Handle, HOTKEY_ID);
        }
    }
}
