using AntdUI;
using HuaWenCapture.Forms;
using HuaWenCapture.Objects;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Transactions;
using HuaWenCapture.Types;
using EasyOcrSharp.Models;

namespace HuaWenCapture {
    public partial class MainForm : AntdUI.BaseForm {
        public MainForm() {
            InitializeComponent();
            tabs1.Font = Static.TabHeaderFont;

            AntdUI.Config.ShowInWindowByNotification = true;

            HotkeyManager.Register("capture", new Gesture(Keys.Q, Mod.Alt | Mod.Shift), () => {
                BeginScreenCapture();
            });


            translationTable1.AddEntry(0.2F, "我迷路了", "I'm Lost", "asd");
            translationTable1.AddEntry(0.2F, "", "I'm Lost", "asd");

            this.Load += OnLoad;
        }

        private void OnLoad(object? sender, EventArgs e) {
            _ = Task.Run(() => OCRService.StartAsync());
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
    }
}
