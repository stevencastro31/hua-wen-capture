namespace HuaWenCapture.Types {
    // modifier keys
    public enum Mod : uint {
        None = 0,
        Alt = 0x0001,
        Control = 0x0002,
        Shift = 0x0004,
        Win = 0x0008
    }

    // hotkey register outcomes
    public enum RegisterResult {
        Success,
        AlreadyInUse,               // another app (or this app) already owns this combo
        Invalid                     // bad key/modifier combination or other failure
    }
}
