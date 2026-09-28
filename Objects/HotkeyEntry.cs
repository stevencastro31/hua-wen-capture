using System;
using System.Collections.Generic;
using System.Text;

namespace HuaWenCapture.Objects {
    internal class HotkeyEntry {
        public int Id;
        public Gesture Gesture;
        public Action Handler;

        public HotkeyEntry(int id, Gesture gesture, Action handler) {
            this.Id = id;
            this.Gesture = gesture;
            this.Handler = handler;
        }
    }
}