using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AntdUI;
using HuaWenCapture.Objects;

namespace HuaWenCapture.Controls {
    public partial class TranslationTable : UserControl {
        Column colId = new("id", "号") {
            Width = "64", 
            ColAlign = ColumnAlign.Center, Align = ColumnAlign.Center,
            Wrap = true, LineBreak = true, ColBreak = true,
            Editable = false,
        };
        Column colScore = new("score", "分数") {
            Width = "80",
            ColAlign = ColumnAlign.Center, Align = ColumnAlign.Center,
            DisplayFormat = "0%",
            Wrap = true, LineBreak = true, ColBreak = true,
            Editable = false,
        };
        Column colAction = new("action", "操作") {
            Width = "144",
            ColAlign = ColumnAlign.Center, Align = ColumnAlign.Center,
            Wrap = true, LineBreak = true, ColBreak = true,
            Editable = false,
            Render = (value, record, rowIndex) => {
                CellButton button1 = new($"{rowIndex}") { Text = "图片", Id = "IMAGE" };   // view screenshot
                CellButton button2 = new($"{rowIndex}") { Text = "查看", Id = "DICT" };    // look up dictionary
                button1.Fore = Color.Blue;
                button2.Fore = Color.Blue;
                return new CellButton[] { button1, button2 };
            },
        };
        Column colZh = new("zh", "中文 (OCR/文字识别)") {
            Width = "256", 
            MinWidth = "128",
            ColAlign = ColumnAlign.Center, Align = ColumnAlign.Left,
            Wrap = true, LineBreak = true, ColBreak = true,
            ReadOnly = true
        };
        Column colEn = new("en", "英文 (Translation/翻译)") {
            Width = "256", 
            MinWidth = "128",
            ColAlign = ColumnAlign.Center, Align = ColumnAlign.Left,
            Wrap = true, LineBreak = true, ColBreak = true,
            ReadOnly = true
        };

        private DataTable _data = new();
        private int _count = 0;

        public TranslationTable() {
            InitializeComponent();
            SetupTableColumns();
        }

        public void AddEntry(float score, string zh, string en, string screenshot) {
            _data.Rows.Add(_count++, score, screenshot, zh, en);
        }

        public bool RemoveEntry(int id) {
            DataRow? target = _data.Select($"id = {id}").FirstOrDefault();
            if (target == null) return false;
            _data.Rows.Remove(target);
            return true;
        }

        private void SetupTableColumns() {
            table1.ColumnFont = Static.ColumnHeaderFont;
            table1.Font = Static.ColumnFont;
            table1.Columns.AddRange([colId, colScore, colAction, colZh, colEn]);
            table1.EditMode = TEditMode.DoubleClick;

            _data.Columns.Add("id", typeof(int));
            _data.Columns.Add("score", typeof(float));
            _data.Columns.Add("screenshot", typeof(string));
            _data.Columns.Add("zh", typeof(string));
            _data.Columns.Add("en", typeof(string));
        }

        private void AdjustColumnWidth() {
            int width = (FindForm()!.Width - (64 + 80 + 144)) / 2;
            colZh.Width = $"{width}";
            colEn.Width = $"{width}";
        }

        private void OnTranslationTableLoad(object sender, EventArgs e) {
            AddEntry(0.9F, "我来了来了！", "I'm HERE!", "test");
            AddEntry(0.45F, "我来了来了！", "I'm HERE!", "test");
            AddEntry(0.9F, "我来了来了！", "I'm HERE!", "test");
            AddEntry(0.9F, "我来了来了！", "I'm HERE!", "test");
            AddEntry(0.9F, "我来了来了！", "I'm HERasdE!", "test");
            RemoveEntry(1);

            table1.DataSource = _data;
            AdjustColumnWidth();
            this.Parent!.SizeChanged += OnParentSizeChanged;
            table1.CellButtonClick += OnTableCellButtonClick;
        }

        private void OnTableCellButtonClick(object sender, TableButtonEventArgs e) {
            // do some stuff
        }

        private void OnParentSizeChanged(object? sender, EventArgs e) {
            AdjustColumnWidth();
        }

    }
}
