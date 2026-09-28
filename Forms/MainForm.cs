using HuaWenCapture.Objects;
using AntdUI;

namespace HuaWenCapture {
    public partial class MainForm : AntdUI.BaseForm {
        public MainForm() {
            InitializeComponent();
            tabs1.Font = Static.TabHeaderFont;
        }

        private void OnButtonClick(object sender, EventArgs e) {
            HotkeyManager.BeginCapture(onCaptured: gesture => {
                button1.Text = gesture.ToString();
            }, onProgress: partial => {
                button1.Text = partial.ToString();
            }, onCancelled: () => {
                button1.Text = "cancel";
            });
        }
    }
}
