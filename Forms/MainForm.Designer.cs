namespace HuwWenCapture.Forms {
    partial class MainForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            AntdUI.Tabs.StyleLine styleLine1 = new AntdUI.Tabs.StyleLine();
            notifyIcon1 = new NotifyIcon(components);
            tabs1 = new AntdUI.Tabs();
            tabPage1 = new AntdUI.TabPage();
            translationDisplay1 = new HuwWenCapture.Controls.TranslationDisplay();
            tabPage2 = new AntdUI.TabPage();
            tabPage3 = new AntdUI.TabPage();
            panel1 = new AntdUI.Panel();
            tabs1.SuspendLayout();
            tabPage1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += OnNotifyIconMouseDoubleClick;
            // 
            // tabs1
            // 
            tabs1.Controls.Add(tabPage1);
            tabs1.Controls.Add(tabPage2);
            tabs1.Controls.Add(tabPage3);
            tabs1.Dock = DockStyle.Fill;
            tabs1.Location = new Point(0, 0);
            tabs1.Name = "tabs1";
            tabs1.Pages.Add(tabPage1);
            tabs1.Pages.Add(tabPage2);
            tabs1.Pages.Add(tabPage3);
            tabs1.Size = new Size(884, 561);
            tabs1.Style = styleLine1;
            tabs1.TabIndex = 0;
            tabs1.Text = "tabs1";
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(128, 255, 128);
            tabPage1.Controls.Add(translationDisplay1);
            tabPage1.Location = new Point(0, 30);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(884, 531);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Translation";
            // 
            // translationDisplay1
            // 
            translationDisplay1.BackColor = Color.White;
            translationDisplay1.Location = new Point(0, 0);
            translationDisplay1.Name = "translationDisplay1";
            translationDisplay1.Size = new Size(884, 531);
            translationDisplay1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(0, 0);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(0, 0);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Dictionary";
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(0, 0);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(0, 0);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Settings";
            // 
            // panel1
            // 
            panel1.Controls.Add(tabs1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(884, 561);
            panel1.TabIndex = 1;
            panel1.Text = "panel1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(panel1);
            Dark = true;
            Mode = AntdUI.TAMode.Dark;
            Name = "MainForm";
            Text = "华文 Capture";
            tabs1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private NotifyIcon notifyIcon1;
        private AntdUI.Tabs tabs1;
        private AntdUI.TabPage tabPage1;
        private AntdUI.TabPage tabPage2;
        private AntdUI.TabPage tabPage3;
        private Controls.TranslationDisplay translationDisplay1;
        private AntdUI.Panel panel1;
    }
}
