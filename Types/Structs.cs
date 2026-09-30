using System.Runtime.InteropServices;

namespace HuaWenCapture.Types {
    [StructLayout(LayoutKind.Sequential)]
    internal struct KBDLLHOOKSTRUCT {
        public uint vkCode;
        public uint scanCode;
        public uint flags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }
}
