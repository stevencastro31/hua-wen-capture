using AntdUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DictionaryEntry = HuwWenCapture.Objects.DictionaryEntry;

namespace HuwWenCapture.Forms {
    public partial class DictionaryDialog : BaseForm {
        public DictionaryDialog() {
            InitializeComponent();
            this.SizeChanged += DictionaryDialog_SizeChanged;
        }

        private void DictionaryDialog_SizeChanged(object? sender, EventArgs e) {
            dictionaryDisplay1.ResizeColumns(this.Width);
        }

        public void AddItems(List<DictionaryEntry> entries) {
            foreach (DictionaryEntry entry in entries)
                dictionaryDisplay1.AddRow($"{entry.Simplified} / {entry.Traditional}", entry.Pinyin, entry.Definition);
            dictionaryDisplay1.UpdateData();
        }
    }
}
