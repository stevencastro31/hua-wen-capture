namespace HuwWenCapture.Controls {
    partial class SettingDisplay {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            switch1 = new AntdUI.Switch();
            label1 = new AntdUI.Label();
            label2 = new AntdUI.Label();
            label3 = new AntdUI.Label();
            button1 = new AntdUI.Button();
            buttonCaptureDirectory = new AntdUI.ButtonShadow();
            buttonCaptureHotkey = new AntdUI.ButtonShadow();
            SuspendLayout();
            // 
            // switch1
            // 
            switch1.Location = new Point(240, 27);
            switch1.Name = "switch1";
            switch1.Size = new Size(80, 36);
            switch1.TabIndex = 0;
            switch1.Text = "switch1";
            // 
            // label1
            // 
            label1.Font = new Font("Microsoft YaHei", 14.25F);
            label1.Location = new Point(25, 25);
            label1.Name = "label1";
            label1.Size = new Size(200, 40);
            label1.TabIndex = 1;
            label1.Text = "Start Minimized";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Font = new Font("Microsoft YaHei", 14.25F);
            label2.Location = new Point(25, 155);
            label2.Name = "label2";
            label2.Size = new Size(200, 40);
            label2.TabIndex = 2;
            label2.Text = "Capture Directory";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Font = new Font("Microsoft YaHei", 14.25F);
            label3.Location = new Point(25, 90);
            label3.Name = "label3";
            label3.Size = new Size(200, 40);
            label3.TabIndex = 3;
            label3.Text = "Capture Hotkey";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft YaHei", 14.25F);
            button1.Location = new Point(25, 220);
            button1.Name = "button1";
            button1.Size = new Size(415, 40);
            button1.TabIndex = 6;
            button1.Text = "Save";
            button1.Click += OnSaveButtonClick;
            // 
            // buttonShadow1
            // 
            buttonCaptureDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCaptureDirectory.Font = new Font("Microsoft YaHei", 14.25F);
            buttonCaptureDirectory.Location = new Point(240, 155);
            buttonCaptureDirectory.MaximumSize = new Size(500, 40);
            buttonCaptureDirectory.MinimumSize = new Size(200, 40);
            buttonCaptureDirectory.Name = "buttonShadow1";
            buttonCaptureDirectory.Size = new Size(200, 40);
            buttonCaptureDirectory.TabIndex = 7;
            buttonCaptureDirectory.Text = "buttonShadow1";
            buttonCaptureDirectory.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // buttonShadow2
            // 
            buttonCaptureHotkey.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCaptureHotkey.Font = new Font("Microsoft YaHei", 14.25F);
            buttonCaptureHotkey.Location = new Point(240, 90);
            buttonCaptureHotkey.MaximumSize = new Size(500, 40);
            buttonCaptureHotkey.MinimumSize = new Size(200, 40);
            buttonCaptureHotkey.Name = "buttonShadow2";
            buttonCaptureHotkey.Size = new Size(200, 40);
            buttonCaptureHotkey.TabIndex = 8;
            buttonCaptureHotkey.Text = "buttonShadow2";
            buttonCaptureHotkey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SettingDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonCaptureHotkey);
            Controls.Add(buttonCaptureDirectory);
            Controls.Add(button1);
            Controls.Add(switch1);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "SettingDisplay";
            Size = new Size(465, 285);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Switch switch1;
        private AntdUI.Label label1;
        private AntdUI.Label label2;
        private AntdUI.Label label3;
        private AntdUI.Button button1;
        private AntdUI.ButtonShadow buttonCaptureDirectory;
        private AntdUI.ButtonShadow buttonCaptureHotkey;
    }
}
