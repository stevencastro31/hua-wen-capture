using AntdUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DictionaryEntry = HuwWenCapture.Objects.DictionaryEntry;

namespace HuwWenCapture.Controls {
    public partial class DictionaryDisplay : UserControl {
        private readonly DataTable _data = new();

        public DictionaryDisplay() {
            InitializeComponent();

            table1.Font = new Font("Microsoft YaHei", 14.25F);
            table1.EmptyText = "NO DATA";
            table1.EditMode = TEditMode.DoubleClick;
            table1.FixedHeader = true;

            _data.Columns.Add("zh", typeof(string));
            _data.Columns.Add("py", typeof(string));
            _data.Columns.Add("definition", typeof(string));

            Column colZH = new("zh", "字") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, Align = ColumnAlign.Center };
            Column colPY = new("py", "拼音") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, Align = ColumnAlign.Center };
            Column colDF = new("definition", "定义 (Definition)") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, };

            colZH.Width = "160";
            colPY.Width = "160";
            colDF.Width = "660";
            colDF.MinWidth = "320";

            table1.Columns.Add(colZH);
            table1.Columns.Add(colPY);
            table1.Columns.Add(colDF);
        }

        public void AddRow(string word, string pinyin, string definition) {
            _data.Rows.Add(word, pinyin, definition);
        }

        public void UpdateData() {
            table1.DataSource = _data;
        }

        public void ClearData() {
            _data.Rows.Clear();
        }

        public void ResizeColumns(int width) {
            int colWidth = (int)(width - 320);
            table1.Columns["definition"]!.Width = $"{colWidth}";
        }
    }
}
