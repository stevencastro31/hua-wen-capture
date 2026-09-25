using AntdUI;
using AntdUI.Chat;
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

        string chiTestText = "我喜欢很多不同的爱好 , 比如看书、 听音乐和运动。 我最喜欢的运动是跑步 , 因力人可以让我放松心情 , 同时保持健康。 周未的时候 , 我也喜欢和朋友一起去爬山张者打篮球 , 这让我感到很开心。";
        string engTestText = "I like a lot of different hobbies, like reading books, listening to music and sports. My favorite sport is running, because people can relax me and keep me healthy. I also like to play basketball with my friends in the mountains before Monday, which makes me happy.";

        public MainForm() {
            InitializeComponent();

            this.Font = new Font("DengXian", 14);
            notifyIcon1.Icon = SystemIcons.WinLogo;
            HotkeyManager.SetForm(this);

            string directory = "./captures";
            Directory.CreateDirectory(directory);

            tabs1.Resize += Tabs1_Resize;
        }

        private void Tabs1_Resize(object? sender, EventArgs e) {
            translationDisplay1.ResizeColumns(tabs1.Width);
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            ChineseTranslator.Shutdown();
        }

        private void ScreenCapture() {

            using ScreenshotForm ssf = new();
            if (ssf.ShowDialog() == DialogResult.OK) {
                Bitmap img = ssf.screenCapture!;

                Debug.WriteLine(img.HorizontalResolution);
                Debug.WriteLine(img.VerticalResolution);

                string filename = string.Format(@"{0}.png", Guid.NewGuid());
                string filepath = Path.Combine(AppContext.BaseDirectory, "captures", filename);
                img.Save(filepath, ImageFormat.Png);

                MemoryStream stream = new();
                img.Save(stream, ImageFormat.Png);
                byte[] data = stream.ToArray();

                string rawChinese = ChineseOCR.PerformOCR(data);
                string translatedEnglish = ChineseTranslator.TranslateToEnglish(rawChinese);

                translationDisplay1.AddEntry(rawChinese, translatedEnglish, filepath);
            }
            ssf.Dispose();
        }

        protected override void WndProc(ref System.Windows.Forms.Message m) {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == 1)
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
