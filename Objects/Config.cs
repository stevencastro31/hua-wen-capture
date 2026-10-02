namespace HuaWenCapture.Objects {
    internal class Config {
        public bool IsStartOnLaunch { get; set; }
        public bool IsStartMinimized { get; set; }
        public string CapturePath { get; set; }
        public Gesture CaptureHotkey { get; set; }

        public Config(bool isStartOnLaunch, bool isStartMinimized, string capturePath, Gesture captureHotkey) {
            this.IsStartOnLaunch = isStartOnLaunch;
            this.IsStartMinimized = isStartMinimized;
            this.CapturePath = capturePath;
            this.CaptureHotkey = captureHotkey;
        }
    }
}
