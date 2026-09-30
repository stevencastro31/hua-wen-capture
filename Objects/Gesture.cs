using HuaWenCapture.Types;

namespace HuaWenCapture.Objects {
    // object that represent a sequence of key strokes
    public class Gesture : IEquatable<Gesture> {
        public Keys Key { get; private set; }
        public Mod Modifiers { get; private set; }

        public Gesture(Keys key, Mod modifiers) {
            this.Key = key;
            this.Modifiers = modifiers;
        }

        public bool Equals(Gesture? other) {
            return other != null && other.Key == this.Key && other.Modifiers == this.Modifiers;
        }

        public override bool Equals(object? obj) { return Equals(obj as Gesture); }

        public override int GetHashCode() { return ((int)Key * 397) ^ (int)Modifiers; }

        public override string ToString() {
            List<string> parts = new();
            if ((this.Modifiers & Mod.Control) != 0) parts.Add("Ctrl");
            if ((this.Modifiers & Mod.Alt) != 0) parts.Add("Alt");
            if ((this.Modifiers & Mod.Shift) != 0) parts.Add("Shift");
            if ((this.Modifiers & Mod.Win) != 0) parts.Add("Win");
            if (this.Key != Keys.None) parts.Add(Key.ToString());       // Key is None while only modifiers are held
            return string.Join("+", parts);
        }
    }
}