using System;
using System.Collections.Generic;
using System.Text;

namespace HuaWenCapture.Objects {
    // a window handle for the application to receive messages
    internal class HotkeyWindow : NativeWindow {
        private const int WM_HOTKEY = 0x0312;
        private const int HWND_MESSAGE = -3;

        public HotkeyWindow() {
            CreateParams cParams = new CreateParams();
            cParams.Parent = new IntPtr(HWND_MESSAGE);
            CreateHandle(cParams);
        }

        // intercept low level windows messages sent to the form/window.
        protected override void WndProc(ref Message m) {
            if (m.Msg == WM_HOTKEY)         // if windows detect a hotkey, do the associated action/method
                HotkeyManager.OnHotkey(m.WParam.ToInt32());
            base.WndProc(ref m);
        }
    }
}
