using HuaWenCapture.Forms;
using HuaWenCapture.Objects;
using HuaWenCapture.Types;
using System.Diagnostics;
using DictionaryEntry = HuaWenCapture.Objects.DefinitionEntry;

namespace HuaWenCapture {
    public partial class MainForm : AntdUI.BaseForm {
        public MainForm() {
            InitializeComponent();
            tabs1.Font = Static.TabHeaderFont;
            AntdUI.Config.ShowInWindowByNotification = true;

            HotkeyManager.Register("capture", new Gesture(Keys.Q, Mod.Alt | Mod.Shift), () => {
                BeginScreenCapture();
            });

            SubscribeEvents();
        }

        private void BeginScreenCapture() {
            using ScreenshotForm ssf = new();
            if (ssf.ShowDialog() == DialogResult.OK) {
                byte[]? data = ssf.GetScreenshotMemoryStream();
                if (data == null) return;

                OCRService.Enqueue(data, onResult: (result) => {
                    string zh = result.Text;
                    string en = TranslationService.Translate(zh);

                    BeginInvoke(() => {
                        translationTable1.AddEntry(result.Confidence, zh, en, "test");
                        tabs1.SelectedIndex = 0;    // go tabs
                    });

                }, onError: (ex) => {
                    Debug.WriteLine(ex.Message);
                });

                if (!this.Visible) {
                    this.WindowState = FormWindowState.Normal;
                    this.Show();
                }
            }
        }

        private void SubscribeEvents() {
            input1.TextChanged += OnSearchTextChanged;
            translationTable1.OnDictionaryLookUp += OnDictionaryLookUp;
            this.Load += OnLoad;
        }

        private void OnSearchTextChanged(object? sender, EventArgs e) {
            string text = input1.Text;
            if (string.IsNullOrWhiteSpace(text)) return;

            List<DictionaryEntry> entries = [
                .. DictionaryService.Segmenter
                .Cut(text, cutAll: true)
                .Distinct()
                .SelectMany(DictionaryService.Lookup)
            ];
            dictionaryTable1.ClearData();

            foreach (DictionaryEntry entry in entries)
                dictionaryTable1.AddRow($"{entry.Simplified} / {entry.Traditional}", entry.PinYin, entry.Definition);
            dictionaryTable1.RefreshData();
        }

        private void OnLoad(object? sender, EventArgs e) {
            _ = Task.Run(() => OCRService.StartAsync());

            translationTable1.AddEntry(0.2F, "我迷路了", "I'm Lost", "asd");
            translationTable1.AddEntry(0.2F, "", "I'm Lost", "asd");
        }

        private void OnDictionaryLookUp(string text) {
            input1.Text = text;
            tabs1.SelectedIndex = 1;
        }

        private void OnButtonClick(object sender, EventArgs e) {
            //BeginScreenCapture();

            //HotkeyManager.BeginCapture(onCaptured: gesture => {
            //    button1.Text = gesture.ToString();
            //}, onProgress: partial => {
            //    button1.Text = partial.ToString();
            //}, onCancelled: () => {
            //    button1.Text = "cancel";
            //});
        }
    }
}
