using HuwWenCapture.Objects;
using Python.Runtime;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace HuwWenCapture {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            ChineseTranslator.Shutdown();
        }

        private async void Form1_Load(object sender, EventArgs e) {

        }

        private void ButtonClick(object sender, EventArgs e) {
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
                MessageBox.Show(builder.ToString(), "Alert");

            } else {
                Debug.WriteLine("woops");
            }
            ssf.Dispose();
        }
    }
}
