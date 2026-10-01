using System;
using System.Collections.Generic;
using System.Text;

namespace HuaWenCapture.Objects {
    internal class Config {
        public bool IsStartMinimized { get; set; }
        public string CapturePath { get; set; }
        public Gesture CaptureHotkey { get; set; }

        public Config(bool isStartMinimized, string capturePath, Gesture captureHotkey) {
            this.IsStartMinimized = isStartMinimized;
            this.CapturePath = capturePath;
            this.CaptureHotkey = captureHotkey;
        }
    }
}
