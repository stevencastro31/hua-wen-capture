using AntdUI;
using HuaWenCapture.Forms;
using HuaWenCapture.Objects;
using HuaWenCapture.Types;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using DictionaryEntry = HuaWenCapture.Objects.DefinitionEntry;
using FolderBrowserDialog = AntdUI.FolderBrowserDialog;

namespace HuaWenCapture {
    public partial class MainForm : AntdUI.BaseForm {
        public MainForm() {
            InitializeComponent();

            tabs1.Font = Static.TabHeaderFont;
            AntdUI.Config.ShowInWindowByNotification = true;
            HotkeyManager.Register("capture", ConfigManager.GetCaptureHotkey(), this.BeginScreenCapture);

            SetupNotifyIcon();
            SubscribeEvents();
            SetupSettingsUI();
        }

        // form methods
        private void BeginScreenCapture() {
            using ScreenshotForm ssf = new();
            if (ssf.ShowDialog() == DialogResult.OK) {
                byte[]? data = ssf.GetScreenshotMemoryStream();
                if (data == null) return;

                string filepath = SaveScreenshot(ssf.GetScreenshotBitmap()!);
                OCRService.Enqueue(data, onResult: (result) => {
                    string zh = result.Text;
                    string en = TranslationService.Translate(zh);

                    BeginInvoke(() => {
                        translationTable1.AddEntry(result.Confidence, zh, en, filepath);
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

        private string SaveScreenshot(Bitmap bmp) {
            string filename = string.Format(@"{0}.png", Guid.NewGuid());
            string filepath = Path.Combine(ConfigManager.GetCapturePath(), filename);
            bmp.Save(filepath, ImageFormat.Png);
            return filepath;
        }

        private void SubscribeEvents() {
            input1.TextChanged += OnSearchTextChanged;
            translationTable1.OnDictionaryLookUp += OnDictionaryLookUp;
            this.Load += OnLoad;
            this.FormClosing += OnFormClosing;
            this.Shown += OnFormShown;
        }

        private void SetupNotifyIcon() {
            notifyIcon1.Icon = SystemIcons.WinLogo;
            notifyIcon1.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            
            string baseIconDirectory = Path.Combine(AppContext.BaseDirectory, "Resources", "Icons");    // from: https://icon-icons.com
            notifyIcon1.ContextMenuStrip.Items.Add("Capture", Image.FromFile(Path.Combine(baseIconDirectory, "icon-204560.png")));
            notifyIcon1.ContextMenuStrip.Items.Add("Exit", Image.FromFile(Path.Combine(baseIconDirectory, "icon-234165.png")));

            notifyIcon1.DoubleClick += OnNotifyIconMouseDoubleClick;
            notifyIcon1.ContextMenuStrip.Items[0].Click += OnContextMenuCaptureClick;
            notifyIcon1.ContextMenuStrip.Items[1].Click += OnContextMenuExitClick;
        }

        private void SetupSettingsUI() {
            switch1.Checked = ConfigManager.GetIsStartMinimized();
            buttonShadow1.Text= ConfigManager.GetCapturePath();
            buttonShadow2.Text= ConfigManager.GetCaptureHotkey().ToString();

            switch1.CheckedChanged += OnStartMinimizedSettingButtonClick;
            buttonShadow1.Click += OnCapturePathSettingButtonClick;
            buttonShadow2.Click += OnCaptureHotkeySettingButtonClick;
        }

        // form events
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
        }

        private void OnDictionaryLookUp(string text) {
            input1.Text = text;
            tabs1.SelectedIndex = 1;
        }

        private void OnFormShown(object? sender, EventArgs e) {
            if (ConfigManager.GetIsStartMinimized()) {
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void OnFormClosing(object? sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                this.Hide();
            } else {
                ConfigManager.SaveConfig();
            }
        }

        // notify icon events
        private void OnNotifyIconMouseDoubleClick(object? sender, EventArgs e) {
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void OnContextMenuCaptureClick(object? sender, EventArgs e) {
            BeginScreenCapture();
        }

        private void OnContextMenuExitClick(object? sender, EventArgs e) {
            Application.Exit();
        }

        // setting events
        private void OnStartMinimizedSettingButtonClick(object sender, AntdUI.BoolEventArgs e) {
            ConfigManager.SetIsStartMinimized(e.Value);
        }

        private void OnCapturePathSettingButtonClick(object? sender, EventArgs e) {
            FolderBrowserDialog folderDialog = new();
            folderDialog.DirectoryPath = ConfigManager.GetCapturePath();
            DialogResult result = folderDialog.ShowDialog(this);
            if (result == DialogResult.OK) {
                ConfigManager.SetCapturePath(folderDialog.DirectoryPath);
                buttonShadow1.Text = ConfigManager.GetCapturePath();
            }
        }

        private void OnCaptureHotkeySettingButtonClick(object? sender, EventArgs e) {
            buttonShadow2.Text = "Enter a new hotkey...";

            HotkeyManager.BeginCapture(onCaptured: gesture => {
                buttonShadow2.Text = gesture.ToString();

                RegisterResult result = HotkeyManager.Register("capture", gesture, this.BeginScreenCapture);

                if (result == RegisterResult.AlreadyInUse) {
                    buttonShadow2.Text = ConfigManager.GetCaptureHotkey().ToString();
                    Notification.error(this, gesture.ToString() + " (This hotkey is already in use).", "", TAlignFrom.BR, autoClose: 3);
                }
                else if (result == RegisterResult.Success) {
                    ConfigManager.SetCaptureHotkey(gesture);
                    translationTable1.UpdateEmptyText();
                }
            }, onProgress: partial => {
                if (partial.ToString().Length < 1)
                    buttonShadow2.Text = "Enter a new hotkey...";
                else
                    buttonShadow2.Text = partial.ToString();
            }, onCancelled: () => {
                buttonShadow2.Text = ConfigManager.GetCaptureHotkey().ToString();
            });
        }
    }
}
