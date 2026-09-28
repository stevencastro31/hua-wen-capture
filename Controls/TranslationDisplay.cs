using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AntdUI;
using HuwWenCapture.Forms;
using HuwWenCapture.Objects;
using JiebaNet.Segmenter;
using DictionaryEntry = HuwWenCapture.Objects.DictionaryEntry;

namespace HuwWenCapture.Controls {
    public partial class TranslationDisplay : UserControl {
        private readonly DataTable _data = new ();

        public TranslationDisplay() {
            InitializeComponent();
            table1.Font = new Font("Microsoft YaHei", 17.25F);
            table1.EmptyText = "NO DATA";
            table1.FixedHeader = true;

            _data.Columns.Add("id", typeof(int));
            _data.Columns.Add("score", typeof(float));
            _data.Columns.Add("zh-OCR", typeof(string));
            _data.Columns.Add("en-Translation", typeof(string));
            _data.Columns.Add("screenshot", typeof(string));

            // No., Score, Actions, Chinese, English

            Column colID = new("id", "号") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, Editable = false, Align = ColumnAlign.Center };
            Column colMS = new("score", "分数") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, Editable = false, Align = ColumnAlign.Center };
            Column colZH = new("zh-OCR", "中文 (OCR/文字识别)") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, };
            Column colEN = new("en-Translation", "英文 (Translation/翻译) [ML]") { Wrap = true, LineBreak = true, ColBreak = true, ReadOnly = true, };

            colMS.DisplayFormat = "0%";

            Column colSS = new("screenshot", "操作") {
                Render = (value, record, rowIndex) => {
                    CellButton button1 = new($"{rowIndex}") { Text = "图片", Id = "IMAGE" };   // view screenshot
                    CellButton button2 = new($"{rowIndex}") { Text = "查看", Id = "DICT" };    // look up dictionary
                    button1.Fore = Color.Blue;
                    button2.Fore = Color.Blue;
                    return new CellButton[] { button1, button2 };
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

            table1.CellButtonClick += OnTableCellButtonClick;
        }

        private void OnTableCellButtonClick(object sender, TableButtonEventArgs e) {
            DataRow dr = (DataRow)e.Record!;
            if (e.Btn.Id == "IMAGE") {
                string filepath = (string)dr.ItemArray[4]!;
                if (filepath == null) return;

                try {
                    Process.Start(new ProcessStartInfo(filepath) { UseShellExecute = true });
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
                return;
            }

            // Dictionary
            string text = (string)dr.ItemArray[2]!;

            if (text == null) return;
            string[] segments = [.. ChineseDictionary.Segmenter.Cut(text, cutAll: true)];

            List<DictionaryEntry> data = [];
            foreach (string segment in segments) {
                List<DictionaryEntry> entries = ChineseDictionary.Lookup(segment);
                data.AddRange(entries);
            }

            DictionaryDialog dialog = new();
            dialog.AddItems(data);
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.Show(FindForm());
        }

        public void AddEntry(float score, string zh, string en, string path) {
            _data.Rows.Add(_data.Rows.Count + 1, score, zh, en, path);
            table1.DataSource = _data;
            table1.ScrollToEnd();
        }

        public void ResizeColumns(int width) {
            table1.Columns["id"]!.Width = $"80";
            table1.Columns["screenshot"]!.Width = $"152";
            table1.Columns["score"]!.Width = $"80";
            int colWidth = (int)((width - 312) / 2);
            table1.Columns["zh-OCR"]!.Width = $"{colWidth}";
            table1.Columns["en-Translation"]!.Width = $"{colWidth}";
        }
    }
}
