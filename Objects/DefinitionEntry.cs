using System;
using System.Collections.Generic;
using System.Text;

namespace HuaWenCapture.Objects {
    internal class DefinitionEntry(string simplified, string traditional, string pinyin, string definition) {
        public string Simplified { get; set; } = simplified;
        public string Traditional { get; set; } = traditional;
        public string PinYin { get; set; } = pinyin;
        public string Definition { get; set; } = definition;
    }
}
