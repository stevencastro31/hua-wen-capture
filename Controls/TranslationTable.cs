using AntdUI;
using HuaWenCapture.Objects;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace HuaWenCapture.Controls {
    public partial class TranslationTable : UserControl {
        Column colId = new("id", "号") {
            Width = "64",
            ColAlign = ColumnAlign.Center,
            Align = ColumnAlign.Center,
            Wrap = true,
            LineBreak = true,
            ColBreak = true,
            Editable = false,
        };
        Column colScore = new("score", "分数") {
            Width = "80",
            ColAlign = ColumnAlign.Center,
            Align = ColumnAlign.Center,
            DisplayFormat = "0%",
            Wrap = true,
            LineBreak = true,
            ColBreak = true,
            Editable = false,
        };
        Column colAction = new("action", "操作") {
            Width = "256",
            ColAlign = ColumnAlign.Center,
            Align = ColumnAlign.Center,
            Wrap = true,
            LineBreak = true,
            ColBreak = true,
            Editable = false,
            Render = (value, record, rowIndex) => {
                CellButton button1 = new($"{rowIndex}") { Text = "图片", Id = "IMAGE" };      // view screenshot
                CellButton button2 = new($"{rowIndex}") { Text = "查字典", Id = "DICT" };       // look up dictionary
                CellButton button3 = new($"{rowIndex}") { Text = "谷歌翻译", Id = "GOOGLE" };     // look up google translate on ocr text
                button1.Fore = Color.Blue;
                button2.Fore = Color.Blue;
                button3.Fore = Color.Blue;
                return new CellButton[] { button1, button2, button3 };
            },
        };
        Column colZh = new("zh", "中文 (OCR/文字识别)") {
            Width = "308",
            MinWidth = "128",
            ColAlign = ColumnAlign.Center,
            Align = ColumnAlign.Left,
            Wrap = true,
            LineBreak = true,
            ColBreak = true,
            ReadOnly = true
        };
        Column colEn = new("en", "英文 (Translation/翻译)") {
            Width = "308",
            MinWidth = "128",
            ColAlign = ColumnAlign.Center,
            Align = ColumnAlign.Left,
            Wrap = true,
            LineBreak = true,
            ColBreak = true,
            ReadOnly = true
        };

        private DataTable _data = new();
        private int _count = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action<string>? OnDictionaryLookUp { get; set; }

        public TranslationTable() {
            InitializeComponent();
            SetupTableColumns();
            table1.EmptyText = "Press: Alt + Shift + Q to Capture";
        }

        public void AddEntry(float score, string zh, string en, string screenshot) {
            _data.Rows.Add(_count++, score, screenshot, zh, en);
            RefreshData();
            table1.ScrollToEnd();
        }

        public bool RemoveEntry(int id) {
            DataRow? target = _data.Select($"id = {id}").FirstOrDefault();
            if (target == null) return false;
            _data.Rows.Remove(target);
            return true;
        }

        public void RefreshData() {
            table1.DataSource = _data;
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
            int width = (FindForm()!.Width - (64 + 80 + 240)) / 2;
            colZh.Width = $"{width}";
            colEn.Width = $"{width}";
        }

        private void OnTranslationTableLoad(object sender, EventArgs e) {
            table1.DataSource = _data;
            AdjustColumnWidth();
            this.Parent!.SizeChanged += OnParentSizeChanged;
            table1.CellButtonClick += OnTableCellButtonClick;
        }

        private void OnTableCellButtonClick(object sender, TableButtonEventArgs e) {
            DataRow dataRow = (DataRow)e.Record!;
            if (e.Btn.Id == "IMAGE") {
                string filepath = (string)dataRow.ItemArray[2]!;
                Process.Start(new ProcessStartInfo() { FileName = filepath, UseShellExecute = true });
                return;
            }

            if (e.Btn.Id == "DICT") {
                string zh = (string)dataRow.ItemArray[3]!;
                if (zh == null || zh.Length < 1) {
                    Notification.error(FindForm()!, "Invalid Chinese Text", "", TAlignFrom.BR, autoClose: 3);
                } else {
                    OnDictionaryLookUp?.Invoke(zh);
                }
                return;
            }

            if (e.Btn.Id == "GOOGLE") {
                string zh = (string)dataRow.ItemArray[3]!;
                if (zh == null || zh.Length < 1) {
                    Notification.error(FindForm()!, "Invalid Chinese Text", "", TAlignFrom.BR, autoClose: 3);
                } else {
                    string url = "https://translate.google.com/?sl=zh-CN&tl=en&text=" + dataRow.ItemArray[3];
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                return;
            }
        }

        private void OnParentSizeChanged(object? sender, EventArgs e) {
            AdjustColumnWidth();
        }
    }
}
