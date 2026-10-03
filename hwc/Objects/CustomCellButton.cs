using AntdUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HuaWenCapture.Objects {
    internal class CustomCellButton : AntdUI.CellButton {
        public CustomCellButton(string id) : base(id) { }

        public CustomCellButton(string id, string? text) : base(id, text) { }

        public CustomCellButton(string id, TTypeMini _type) : base(id, _type) { }

        public CustomCellButton(string id, string text, TTypeMini _type) : base(id, text, _type) { }

        public override void Paint(Canvas g, Font font, bool enable, SolidBrush fore) {
            using Font modifiedFont = new Font(font, font.Style | FontStyle.Bold);
            base.Paint(g, modifiedFont, enable, fore);
        }
    }
}
