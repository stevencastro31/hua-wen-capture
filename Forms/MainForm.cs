using AntdUI;
using HuaWenCapture.Forms;
using HuaWenCapture.Objects;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Transactions;
using HuaWenCapture.Types;

namespace HuaWenCapture {
    public partial class MainForm : AntdUI.BaseForm {
        public MainForm() {
            InitializeComponent();
            tabs1.Font = Static.TabHeaderFont;
            button1.Click += OnButtonClick;


            HotkeyManager.Register("capture", new Gesture(Keys.Q, Mod.Alt | Mod.Shift), () => {
                BeginScreenCapture();
            });

            this.Load += OnLoad;
        }

        private void OnLoad(object? sender, EventArgs e) {
            _ = Task.Run(() => OCRService.StartAsync());
        }

        private void OnButtonClick(object sender, EventArgs e) {
            BeginScreenCapture();

            //HotkeyManager.BeginCapture(onCaptured: gesture => {
            //    button1.Text = gesture.ToString();
            //}, onProgress: partial => {
            //    button1.Text = partial.ToString();
            //}, onCancelled: () => {
            //    button1.Text = "cancel";
            //});
        }

        private void BeginScreenCapture() {
            using ScreenshotForm ssf = new();
            if (ssf.ShowDialog() == DialogResult.OK) {
                byte[]? data = ssf.GetScreenshotMemoryStream();
                if (data == null) return;

                OCRService.Enqueue(data, onResult: text => {
                    Debug.WriteLine(text);
                });


                //ProcessScreenshot(ssf.screenCapture!);


                //if (!this.Visible) {
                //    this.WindowState = FormWindowState.Normal;
                //    this.Show();
                //}
            }
        }
    }
}
