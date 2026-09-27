using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using HuwWenCapture.Objects;

namespace HuwWenCapture.Controls {
    public partial class SettingDisplay : UserControl {
        public SettingDisplay() {
            InitializeComponent();

            switch1.Checked = Properties.Settings.Default.StartMinimized;
            input1.Text = SettingsManager.CaptureHotkey;
            input2.Text = SettingsManager.CaptureDirectory;

            switch1.CheckedChanged += OnStartMinimizedChanged;
            input1.TextChanged += OnCaptureHotkeyChanged;
            input2.TextChanged += OnCaptureDirectoryChanged;
        }

        private void OnStartMinimizedChanged(object sender, AntdUI.BoolEventArgs e) {
            //throw new NotImplementedException();
        }

        private void OnCaptureDirectoryChanged(object? sender, EventArgs e) {
            //throw new NotImplementedException();
        }

        private void OnCaptureHotkeyChanged(object? sender, EventArgs e) {
            //throw new NotImplementedException();
        }
    }
}
