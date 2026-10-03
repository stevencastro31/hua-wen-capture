using AntdUI;
using HuaWenCapture.Objects;
using System.Data;

namespace HuaWenCapture.Controls {
    public partial class DictionaryTable : UserControl {
        Column colZh = new("zh", "字") {
            Width = "160",
            Wrap = true,
            LineBreak = true,
            ColBreak = true,
            ReadOnly = true,
            Align = ColumnAlign.Center
        };
        Column colPy = new("py", "拼音") {
            Width = "160",
            Wrap = true,
            LineBreak = true,
            ColBreak = true,
            ReadOnly = true,
            Align = ColumnAlign.Center
        };
        Column colDefinition = new("definition", "定义 (Definition)") {
            Wrap = true,
            LineBreak = true,
            ColBreak = true,
            ReadOnly = true,
        };

        private DataTable _data = new();

        public DictionaryTable() {
            InitializeComponent();
            SetupTableColumns();
            table1.EmptyText = "Enter text to search definitions";
            table1.EditMode = TEditMode.DoubleClick;
        }

        public void AddRow(string word, string pinyin, string definition) {
            _data.Rows.Add(word, pinyin, definition);
        }

        public void RefreshData() {
            table1.DataSource = _data;
        }

        public void ClearData() {
            _data.Rows.Clear();
        }

        private void SetupTableColumns() {
            table1.ColumnFont = Static.ColumnHeaderFont;
            table1.Font = Static.ColumnFont;
            table1.Columns.AddRange([colZh, colPy, colDefinition]);

            _data.Columns.Add("zh", typeof(string));
            _data.Columns.Add("py", typeof(string));
            _data.Columns.Add("definition", typeof(string));
        }

        private void AdjustColumnWidth() {
            int width = (FindForm()!.Width - (160 + 160) - 32);
            colDefinition.Width = $"{width}";
        }

        private void OnDictionaryTableLoad(object sender, EventArgs e) {
            // lazy solution (TableLayoutPanel > AntdUI.TabsPage)
            this.Parent!.Parent!.SizeChanged += OnParentSizeChanged;
        }

        private void OnParentSizeChanged(object? sender, EventArgs e) {
            AdjustColumnWidth();
        }
    }
}
