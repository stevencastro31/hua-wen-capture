using HuaWenCapture.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HuaWenCapture.Objects {
    internal static class HotkeyManager{
        private static readonly Dictionary<string, HotkeyEntry> Entries = new();
        private static HotkeyWindow? _window;            // window where hotkeys are sent
        private static int _nextId = 0x1000;            // ids used for registering unique hotkeys

        public static Gesture? GetGesture(string name) {
            HotkeyEntry? e;
            return Entries.TryGetValue(name, out e) ? e.Gesture : null;
        }

        #region Hotkey Methods
        // call the method associated with the registered hotkey
        public static void OnHotkey(int id) {
            foreach (HotkeyEntry entry in Entries.Values) {
                if (entry.Id == id) {
                    entry.Handler();
                    return;
                }
            }
        }

        public static RegisterResult Register(string name, Gesture gesture, Action handler) {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("name");    // parameter validation
            if (gesture == null) throw new ArgumentNullException("gesture");
            if (handler == null) throw new ArgumentNullException("handler");

            EnsureWindow(); // get native window

            HotkeyEntry? old;
            Entries.TryGetValue(name, out old);

            // release the old registration first so re-registering the same gesture works.
            if (old != null)
                UnregisterHotKey(_window!.Handle, old.Id);

            int id = old != null ? old.Id : _nextId++;  // hotkey entry id

            if (!RegisterHotKey(_window!.Handle, id, (uint)gesture.Modifiers | MOD_NOREPEAT, (uint)gesture.Key)) {
                int err = Marshal.GetLastWin32Error();

                // roll back to the previous hotkey
                if (old != null)
                    RegisterHotKey(_window.Handle, old.Id, (uint)old.Gesture.Modifiers | MOD_NOREPEAT, (uint)old.Gesture.Key);

                return err == ERROR_HOTKEY_ALREADY_REGISTERED ? RegisterResult.AlreadyInUse : RegisterResult.Invalid;
            }

            Entries[name] = new HotkeyEntry(id, gesture, handler);
            return RegisterResult.Success;
        }

        public static bool Unregister(string name) {
            HotkeyEntry? e;
            if (!Entries.TryGetValue(name, out e)) return false;
            UnregisterHotKey(_window!.Handle, e.Id);
            Entries.Remove(name);
            return true;
        }

        public static void UnregisterAll() {
            CancelCapture();
            if (_window != null) {
                foreach (var e in Entries.Values)
                    UnregisterHotKey(_window.Handle, e.Id);
            }
            Entries.Clear();
        }
        #endregion

        #region Hotkey Capture (low-level keyboard hook)
        private static IntPtr _hook = IntPtr.Zero;
        private static HookProc? _hookProc;                     // keep a reference so the GC doesn't collect it
        private static Action<Gesture>? _onCaptured;
        private static Action? _onCancelled;
        private static Action<Gesture>? _onProgress;
        private static SynchronizationContext? _syncContext;
        private static bool _requireModifier;
        private static bool _finishing;
        private static readonly HashSet<int> HeldModifiers = new HashSet<int>();
        private static readonly HashSet<int> SwallowedDowns = new HashSet<int>();

        public static bool IsCapturing { get { return _hook != IntPtr.Zero; } }     // is capturing when there is a hook

        public static void BeginCapture(Action<Gesture> onCaptured, Action? onCancelled = null, bool requireModifier = true, Action<Gesture>? onProgress = null) {
            if (onCaptured == null) throw new ArgumentNullException("onCaptured");
            if (IsCapturing) CancelCapture();

            // capture routine callbacks
            _onCaptured = onCaptured;
            _onCancelled = onCancelled;
            _onProgress = onProgress;

            _requireModifier = requireModifier;
            _syncContext = SynchronizationContext.Current ?? new WindowsFormsSynchronizationContext();
            _finishing = false;
            HeldModifiers.Clear();
            SwallowedDowns.Clear();

            _hookProc = LowLevelKeyboardProc;       // callback for keyboard hook
            using (Process proc = Process.GetCurrentProcess())
            using (ProcessModule? mod = proc.MainModule) {
                _hook = SetWindowsHookEx(WH_KEYBOARD_LL, _hookProc, GetModuleHandle(mod!.ModuleName), 0);
            }

            if (_hook == IntPtr.Zero) throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
        }

        public static void CancelCapture() {
            if (!IsCapturing) return;
            StopHook();
            ClearCaptureState();
        }

        private static void StopHook() {
            if (_hook != IntPtr.Zero) {
                UnhookWindowsHookEx(_hook);
                _hook = IntPtr.Zero;        // remove the hook
            }
        }

        private static void ClearCaptureState() {
            _onCaptured = null;
            _onCancelled = null;
            _onProgress = null;
            _finishing = false;
            HeldModifiers.Clear();
            SwallowedDowns.Clear();
        }

        private static IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam) {
            if (nCode < 0 || _hook == IntPtr.Zero)
                return CallNextHookEx(_hook, nCode, wParam, lParam);

            //KBDLLHOOKSTRUCT data = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            KBDLLHOOKSTRUCT data = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);
            int msg = wParam.ToInt32();
            int vk = (int)data.vkCode;
            bool isDown = msg == WM_KEYDOWN || msg == WM_SYSKEYDOWN;
            bool isUp = msg == WM_KEYUP || msg == WM_SYSKEYUP;

            // ignore keys we injected/other injected input
            if ((data.flags & LLKHF_INJECTED) != 0)
                return CallNextHookEx(_hook, nCode, wParam, lParam);    // end this hook and process with the next one

            if (isDown) {
                SwallowedDowns.Add(vk);

                if (_finishing)
                    return (IntPtr)1;           // still waiting for the user to release everything

                if (IsModifierVk(vk)) {
                    HeldModifiers.Add(vk);
                    ReportProgress(Keys.None);  // report change in current hotkey configuration
                    return (IntPtr)1;
                }

                Mod mods = CurrentMods();
                Keys key = (Keys)vk;

                if (key == Keys.Escape && mods == Mod.None) {   // end capture routine when ESC or no modifiers are pressed
                    Action? cancelled = _onCancelled;
                    SynchronizationContext? ctx = _syncContext;
                    _finishing = true;
                    if (cancelled != null) ctx!.Post(_ => cancelled(), null);
                    MaybeStopAfterRelease();
                    return (IntPtr)1;
                }

                bool isFunctionKey = key >= Keys.F1 && key <= Keys.F24;
                if (_requireModifier && mods == Mod.None && !isFunctionKey) {
                    ReportProgress(key);    // show it, but keep waiting for a valid combo
                    return (IntPtr)1;
                }

                var captured = _onCaptured;
                SynchronizationContext? syncCtx = _syncContext;
                Gesture gesture = new(key, mods);
                _finishing = true;
                if (captured != null) syncCtx!.Post(_ => captured(gesture), null);
                MaybeStopAfterRelease();
                return (IntPtr)1;
            }

            if (isUp) {
                // only swallow key-ups whose key-down we swallowed; otherwise a key held from before capture started would look "stuck" to the system
                if (SwallowedDowns.Remove(vk)) {
                    HeldModifiers.Remove(vk);
                    if (!_finishing) ReportProgress(Keys.None);
                    MaybeStopAfterRelease();
                    return (IntPtr)1;
                }
            }
            return CallNextHookEx(_hook, nCode, wParam, lParam);
        }

        // posts a partial gesture (currently held modifiers + optional key) to the UI thread.
        private static void ReportProgress(Keys key) {
            var cb = _onProgress;
            if (cb == null) return;
            Gesture partial = new(key, CurrentMods());
            _syncContext!.Post(_ => cb(partial), null);
        }

        // after a result is delivered we keep swallowing until every swallowed key is released, so auto-repeat of the final key doesn't leak into the focused app
        private static void MaybeStopAfterRelease() {
            if (_finishing && SwallowedDowns.Count == 0) {
                StopHook();
                ClearCaptureState();
            }
        }

        private static bool IsModifierVk(int vk) {
            switch (vk) {
                case 0x10:
                case 0xA0:
                case 0xA1:      // Shift
                case 0x11:
                case 0xA2:
                case 0xA3:      // Ctrl
                case 0x12:
                case 0xA4:
                case 0xA5:      // Alt
                case 0x5B:
                case 0x5C:      // Win
                    return true;
                default:
                    return false;
            }
        }

        private static Mod CurrentMods() {
            Mod m = Mod.None;
            foreach (int vk in HeldModifiers) {
                switch (vk) {
                    case 0x10: case 0xA0: case 0xA1: m |= Mod.Shift; break;
                    case 0x11: case 0xA2: case 0xA3: m |= Mod.Control; break;
                    case 0x12: case 0xA4: case 0xA5: m |= Mod.Alt; break;
                    case 0x5B: case 0x5C: m |= Mod.Win; break;
                }
            }
            return m;
        }
        #endregion

        // check if there is a window for hotkeys, if not create one, also unregister all hotkeys when the application is closed.
        private static void EnsureWindow() {
            if (_window != null) return;
            _window = new HotkeyWindow();
            Application.ApplicationExit += (s, e) => UnregisterAll();
        }

        #region Win32 Stuff
        private const int WM_HOTKEY = 0x0312;
        private const int HWND_MESSAGE = -3;
        private const uint MOD_NOREPEAT = 0x4000;
        private const int ERROR_HOTKEY_ALREADY_REGISTERED = 1409;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;
        private const uint LLKHF_INJECTED = 0x10;

        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        #endregion
    }
}