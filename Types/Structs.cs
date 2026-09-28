using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

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
