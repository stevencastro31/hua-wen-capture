using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using HuwWenCapture.Objects;
using AntdUI;

namespace HuwWenCapture.Controls {
    public partial class SettingDisplay : UserControl {
        public SettingDisplay() {
            InitializeComponent();

            switch1.Checked = Properties.Settings.Default.StartMinimized;
            input1.Text = SettingsManager.CaptureHotkey;
            input2.Text = SettingsManager.CaptureDirectory;
        }

        private void OnSaveButtonClick(object sender, EventArgs e) {
            try {
                SettingsManager.StartMinimized = switch1.Checked;
                SettingsManager.CaptureHotkey = input1.Text;
                HotkeyManager.RegisterHotkey();
                SettingsManager.CaptureDirectory = input2.Text;
            } catch (Exception ex) {
                Notification.error(FindForm()!, "Error", ex.Message, TAlignFrom.BR, new Font("Microsoft YaHei", 10));
            } finally {
                SettingsManager.Save();
                Notification.success(FindForm()!, "Saved", "Settings Saved", TAlignFrom.BR, new Font("Microsoft YaHei", 10));
            }
        }
    }
}
