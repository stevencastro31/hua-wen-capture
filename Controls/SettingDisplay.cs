using AntdUI;
using HuwWenCapture.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace HuwWenCapture.Controls {
    public partial class SettingDisplay : UserControl {
        public SettingDisplay() {
            InitializeComponent();

            // initial values
            switch1.Checked = Properties.Settings.Default.StartMinimized;
            buttonCaptureHotkey.Text = SettingsManager.CaptureHotkey;
            buttonCaptureDirectory.Text = SettingsManager.CaptureDirectory;

            buttonCaptureHotkey.Click += OnButtonCaptureHotkeyClick;
            buttonCaptureDirectory.Click += OnButtonCaptureDirectoryClick;

            buttonCaptureHotkey.KeyDown += OnButtonCaptureHotkeyKeyDown;
            buttonCaptureHotkey.KeyUp += OnButtonCaptureHotkeyKeyUp;
            buttonCaptureHotkey.LostFocus += OnButtonCaptureHotkeyLostFocus;
        }

        private void OnButtonCaptureHotkeyLostFocus(object? sender, EventArgs e) {
            _recordingHotkey = false;
            if (!_newValidHotkey)
                buttonCaptureHotkey.Text = SettingsManager.CaptureHotkey;
        }

        private bool _recordingHotkey = false;
        private bool _newValidHotkey = false;
        private string _previousHotkey;
        private string _newHotkey;

        private void OnButtonCaptureHotkeyKeyUp(object? sender, KeyEventArgs e) {
            if (!_recordingHotkey) return;

            try {
                SettingsManager.CaptureHotkey = _newHotkey;
                _newValidHotkey = true;
                buttonCaptureHotkey.Text = _newHotkey;
                _recordingHotkey = false;
            } catch {
                buttonCaptureHotkey.Text = "Press Hotkey...";
            }
        }

        private void OnButtonCaptureHotkeyKeyDown(object? sender, KeyEventArgs e) {
            if (!_recordingHotkey) return;

            string hotkey = "";
            if (e.Control) hotkey += "Ctrl+";
            if (e.Alt) hotkey += "Alt+";
            if (e.Shift) hotkey += "Shift+";

            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
                hotkey += e.KeyCode.ToString().ToUpperInvariant();

            _newHotkey = hotkey;
            buttonCaptureHotkey.Text = hotkey;

            e.SuppressKeyPress = true;
            e.Handled = true;
        }

        private void OnButtonCaptureHotkeyClick(object? sender, EventArgs e) {
            _recordingHotkey = true;
            _newValidHotkey = false;
            _previousHotkey = buttonCaptureHotkey.Text;
            buttonCaptureHotkey.Text = "Press Hotkey...";
            buttonCaptureHotkey.Focus();
        }

        private void OnButtonCaptureDirectoryClick(object? sender, EventArgs e) {
            AntdUI.FolderBrowserDialog dialog = new();
            dialog.DirectoryPath = SettingsManager.CaptureDirectory;
            DialogResult res = dialog.ShowDialog(this);

            if (res == DialogResult.OK) {
                SettingsManager.CaptureDirectory = dialog.DirectoryPath;
                buttonCaptureDirectory.Text = SettingsManager.CaptureDirectory;
            }
        }

        private void OnSaveButtonClick(object sender, EventArgs e) {
            try {
                SettingsManager.StartMinimized = switch1.Checked;
                SettingsManager.CaptureHotkey = buttonCaptureHotkey.Text;
                HotkeyManager.UpdateHotkey();

                SettingsManager.Save();
                Notification.success(FindForm()!, "Saved", "Settings Saved", TAlignFrom.BR, new Font("Microsoft YaHei", 10));
            } catch (Exception ex) {
                Notification.error(FindForm()!, "Error", ex.Message, TAlignFrom.BR, new Font("Microsoft YaHei", 10));
            }
        }
    }
}
