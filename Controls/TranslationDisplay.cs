using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AntdUI;

namespace HuwWenCapture.Controls {
    public partial class TranslationDisplay : UserControl {
        private DataTable _data = new ();

        public TranslationDisplay() {
            InitializeComponent();

            _data.Columns.Add("id", typeof(int));
            _data.Columns.Add("zh-OCR", typeof(string));
            _data.Columns.Add("en-Translation", typeof(string));
            _data.Columns.Add("screenshot", typeof(string));

            Column col0 = new Column("id", "No.") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, Editable = false, Align = ColumnAlign.Center };
            Column col2 = new Column("zh-OCR", "Chinese/中文 (OCR/文字识别)") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, };
            Column col3 = new Column("en-Translation", "English/英文 (Translation/翻译)") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, };

            Column col1 = new Column("screenshot", "Actions") {
                Render = (value, record, rowIndex) => {
                    CellButton button = new CellButton($"{rowIndex}") { Text = "View Image" };
                    button.Fore = Color.Blue;
                    button.Back = Color.Red;
                    return button;
                },
                Align = ColumnAlign.Center,
            };

            string minWidth = "240";
            col2.MinWidth = minWidth;
            col3.MinWidth = minWidth;

            table1.Columns.Add(col0);   // id
            table1.Columns.Add(col1);   // actions
            table1.Columns.Add(col2);   // chinese
            table1.Columns.Add(col3);   // english

            table1.CellButtonClick += Table1_CellButtonClick;
        }

        private void Table1_CellButtonClick(object sender, TableButtonEventArgs e) {
            DataRow dr = e.Record as DataRow;
            string filepath = (string)dr.ItemArray[3];
            if (filepath == null) return;

            try {
                Process.Start(new ProcessStartInfo(filepath) { UseShellExecute = true });
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public void AddEntry(string zh, string en, string path) {
            _data.Rows.Add(_data.Rows.Count + 1, zh, en, path);
            table1.DataSource = _data;
            table1.ScrollToEnd();
        }

        public void ResizeColumns(int width) {
            table1.Columns[0].Width = $"80";
            table1.Columns[1].Width = $"160";
            int colWidth = (int)((width - 80 - 160) / 2);
            table1.Columns[2].Width = $"{colWidth}";
            table1.Columns[3].Width = $"{colWidth}";
        }
    }
}
