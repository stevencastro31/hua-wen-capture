using AntdUI;
using HuwWenCapture.Controls;
using HuwWenCapture.Objects;
using Python.Runtime;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace HuwWenCapture.Forms {
    public partial class MainForm : AntdUI.BaseForm {
        private const int WM_HOTKEY = 0x0312;
        private string _capturesPath;

        public MainForm() {
            InitializeComponent();
            this.Font = new Font("DengXian", 14);

            SetupNotifyIcon();
            HotkeyManager.SetForm(this);

            string picturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            _capturesPath = Path.Combine(picturesPath, "HuaWenCapture");
            if (!Directory.Exists(_capturesPath))
                Directory.CreateDirectory(Path.Combine(_capturesPath));


            Load += MainForm_Load;
            tabs1.Resize += OnTabsResize;
            FormClosing += OnFormClosing;
        }

        private void MainForm_Load(object? sender, EventArgs e) {
            ChineseTranslator.Initialize();
        }

        // Form Events
        private void OnFormClosing(object? sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                Hide();
            } else {
                ChineseTranslator.Shutdown();
            }
        }

        private void OnTabsResize(object? sender, EventArgs e) {
            translationDisplay1.ResizeColumns(tabs1.Width);
        }

        private void OnNotifyIconMouseDoubleClick(object sender, MouseEventArgs e) {
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        // Methods
        private void ScreenCapture() {
            using ScreenshotForm ssf = new();
            if (ssf.ShowDialog() == DialogResult.OK) {
                ProcessScreenshot(ssf.screenCapture!);
            }
        }

        private void ProcessScreenshot(Bitmap bmp) {
            string filename = string.Format(@"{0}.png", Guid.NewGuid());
            string filepath = Path.Combine(_capturesPath, filename);
            bmp.Save(filepath, ImageFormat.Png);

            MemoryStream stream = new();
            bmp.Save(stream, ImageFormat.Png);
            byte[] data = stream.ToArray();

            (string rawChinese, float score) = ChineseOCR.PerformOCR(data);
            string translatedEnglish = ChineseTranslator.TranslateToEnglish(rawChinese);

            translationDisplay1.AddEntry(score, rawChinese, translatedEnglish, filepath);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m) {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == 1)
                ScreenCapture();
            base.WndProc(ref m);
        }
        
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
        private void OnContextMenuCaptureClick(object? sender, EventArgs e) {
            ScreenCapture();
        }

        private void OnContextMenuExitClick(object? sender, EventArgs e) {
            Application.Exit();
        }
    }
}
