using AntdUI;
using HuwWenCapture.Controls;
using HuwWenCapture.Objects;
using HuwWenCapture.Properties;
using Python.Runtime;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using TesseractOCR.Font;

namespace HuwWenCapture.Forms {
    public partial class MainForm : AntdUI.BaseForm {
        private const int WM_HOTKEY = 0x0312;
        private string _capturesPath;

        public MainForm() {
            InitializeComponent();
            this.Font = new Font("Microsoft YaHei", 15F);

            HotkeyManager.SetForm(this);
            SetupNotifyIcon();

            Load += OnFormLoad;
            tabs1.Resize += OnTabsResize;
            FormClosing += OnFormClosing;
            Shown += OnFormShown;

            this.MinimumSize = new Size(800, 500);
        }


        // Methods
        private void ScreenCapture() {
            using ScreenshotForm ssf = new();
            if (ssf.ShowDialog() == DialogResult.OK) {
                ProcessScreenshot(ssf.screenCapture!);
            }
        }

        private void ProcessScreenshot(Bitmap bmp) {
            string filepath = SaveScreenshot(bmp);

            MemoryStream stream = new();
            bmp.Save(stream, ImageFormat.Png);
            byte[] data = stream.ToArray();

            (string rawChinese, float score) = ChineseOCR.PerformOCR(data);
            string translatedEnglish = ChineseTranslator.TranslateToEnglish(rawChinese);

            translationDisplay1.AddEntry(score, rawChinese, translatedEnglish, filepath);
            translationDisplay1.AddEntry(0.9F, "我迷路了！", "I am lost!", "test");
        }

        private string SaveScreenshot(Bitmap bmp) {
            string filename = string.Format(@"{0}.png", Guid.NewGuid());
            if (_capturesPath == null)
                _capturesPath = SettingsManager.CaptureDirectory;
            string filepath = Path.Combine(_capturesPath, filename);
            bmp.Save(filepath, ImageFormat.Png);
            return filepath;
        }


        // Form Events
        private void OnFormLoad(object? sender, EventArgs e) {
            ChineseTranslator.Initialize();
        }

        private void OnFormClosing(object? sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                this.Hide();
            } else {
                ChineseTranslator.Shutdown();
            }
        }

        private void OnFormShown(object? sender, EventArgs e) {
            if (Properties.Settings.Default.StartMinimized) {
                this.Hide();
                this.WindowState = FormWindowState.Minimized;
            }
        }


        // Tab Events
        private void OnTabsResize(object? sender, EventArgs e) {
            translationDisplay1.ResizeColumns(tabs1.Width);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m) {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == 1)
                ScreenCapture();
            base.WndProc(ref m);
        }
        

        // Notify Icons
        private void SetupNotifyIcon() {
            notifyIcon1.Icon = SystemIcons.WinLogo;
            notifyIcon1.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            // from: https://icon-icons.com
            notifyIcon1.ContextMenuStrip.Items.Add("Capture", Image.FromFile("./Data/icon-204560.png"));
            notifyIcon1.ContextMenuStrip.Items.Add("Exit", Image.FromFile("./Data/icon-234165.png"));
            
            notifyIcon1.ContextMenuStrip.Items[0].Click += OnContextMenuCaptureClick;
            notifyIcon1.ContextMenuStrip.Items[1].Click += OnContextMenuExitClick;
        }

        // Context Menu Strip Events
        private void OnNotifyIconMouseDoubleClick(object sender, MouseEventArgs e) {
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void OnContextMenuCaptureClick(object? sender, EventArgs e) {
            ScreenCapture();
        }

        private void OnContextMenuExitClick(object? sender, EventArgs e) {
            Application.Exit();
        }
    }
}
