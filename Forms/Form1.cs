using HuwWenCapture.Objects;
using Python.Runtime;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Runtime.InteropServices;

namespace HuwWenCapture.Forms {
    public partial class Form1 : Form {
        private const int HOTKEY_ID = 1;
        private const int WM_HOTKEY = 0x0312;
        private const uint MOD_CONTROL = 0x0002;
        private const uint MOD_ALT = 0x0001;
        private const uint MOD_SHIFT = 0x0004;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        public Form1() {
            InitializeComponent();

            RegisterHotKey(Handle, HOTKEY_ID, MOD_ALT | MOD_SHIFT, (uint)Keys.H);
            notifyIcon1.Visible = true;
            notifyIcon1.Icon = SystemIcons.WinLogo;
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            ChineseTranslator.Shutdown();
        }

        private void ScreenCapture() {
            using ScreenshotForm ssf = new();
            if (ssf.ShowDialog() == DialogResult.OK) {
                Bitmap img = ssf.screenCapture!;
                img.Save("output.png", ImageFormat.Png);

                MemoryStream stream = new();
                img.Save(stream, ImageFormat.Png);
                byte[] data = stream.ToArray();

                string input = ChineseOCR.PerformOCR(data);
                string res = ChineseTranslator.TranslateToEnglish(input);

                StringBuilder builder = new();
                builder.AppendLine(input);
                builder.AppendLine(res);
                //MessageBox.Show(builder.ToString(), "Alert");

                translationList1.AddTranslationEntry(input, res);
            } else {
                Debug.WriteLine("woops");
            }
            ssf.Dispose();
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
                ScreenCapture();
            base.WndProc(ref m);
        }

        private void OnNotifyIconMouseDoubleClick(object sender, MouseEventArgs e) {
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                Hide();
            }
            base.OnFormClosing(e);
        }
    }
}
