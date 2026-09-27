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
            table1.Font = new Font("Microsoft YaHei", 17.25F);
            table1.EmptyText = "NO DATA";

            _data.Columns.Add("id", typeof(int));
            _data.Columns.Add("score", typeof(float));
            _data.Columns.Add("zh-OCR", typeof(string));
            _data.Columns.Add("en-Translation", typeof(string));
            _data.Columns.Add("screenshot", typeof(string));

            // No., Score, Actions, Chinese, English

            Column colID = new Column("id", "号") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, Editable = false, Align = ColumnAlign.Center };
            Column colMS = new Column("score", "分数") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, Editable = false, Align = ColumnAlign.Center };
            Column colZH = new Column("zh-OCR", "中文 (OCR/文字识别)") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, };
            Column colEN = new Column("en-Translation", "英文 (Translation/翻译)") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, };

            colMS.DisplayFormat = "0%";

            Column colSS = new Column("screenshot", "操作") {
                Render = (value, record, rowIndex) => {
                    CellButton button = new CellButton($"{rowIndex}") { Text = "查看" };  // view
                    button.Fore = Color.Blue;
                    return button;
                },
                Align = ColumnAlign.Center,
            };

            string langMinWidth = "240";
            colZH.MinWidth = langMinWidth;
            colEN.MinWidth = langMinWidth;

            table1.Columns.Add(colID);   // id
            table1.Columns.Add(colMS);   // confidence mean score
            table1.Columns.Add(colSS);   // actions
            table1.Columns.Add(colZH);   // chinese
            table1.Columns.Add(colEN);   // english

            table1.CellButtonClick += Table1_CellButtonClick;
        }

        private void Table1_CellButtonClick(object sender, TableButtonEventArgs e) {
            DataRow dr = e.Record as DataRow;
            string filepath = (string)dr.ItemArray[4];
            if (filepath == null) return;

            try {
                Process.Start(new ProcessStartInfo(filepath) { UseShellExecute = true });
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public void AddEntry(float score, string zh, string en, string path) {
            _data.Rows.Add(_data.Rows.Count + 1, score, zh, en, path);
            table1.DataSource = _data;
            table1.ScrollToEnd();
        }

        public void ResizeColumns(int width) {
            table1.Columns["id"].Width = $"80";
            table1.Columns["screenshot"].Width = $"112";
            table1.Columns["score"].Width = $"80";
            int colWidth = (int)((width - 272) / 2);
            table1.Columns["zh-OCR"].Width = $"{colWidth}";
            table1.Columns["en-Translation"].Width = $"{colWidth}";
        }
    }
}
