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
            input1 = new AntdUI.Input();
            input2 = new AntdUI.Input();
            button1 = new AntdUI.Button();
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
            // input1
            // 
            input1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            input1.Font = new Font("Microsoft YaHei", 14.25F);
            input1.Location = new Point(240, 90);
            input1.MaximumSize = new Size(500, 40);
            input1.MinimumSize = new Size(200, 40);
            input1.Name = "input1";
            input1.Size = new Size(200, 40);
            input1.TabIndex = 4;
            input1.Text = "input1";
            // 
            // input2
            // 
            input2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            input2.Font = new Font("Microsoft YaHei", 14.25F);
            input2.Location = new Point(240, 155);
            input2.MaximumSize = new Size(500, 40);
            input2.MinimumSize = new Size(200, 40);
            input2.Name = "input2";
            input2.Size = new Size(200, 40);
            input2.TabIndex = 5;
            input2.Text = "input2";
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
            // SettingDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button1);
            Controls.Add(input1);
            Controls.Add(switch1);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(input2);
            Name = "SettingDisplay";
            Size = new Size(465, 285);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Switch switch1;
        private AntdUI.Label label1;
        private AntdUI.Label label2;
        private AntdUI.Label label3;
        private AntdUI.Input input1;
        private AntdUI.Input input2;
        private AntdUI.Button button1;
    }
}
