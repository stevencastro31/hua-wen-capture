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
            panel3 = new AntdUI.Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            input1 = new AntdUI.Input();
            dictionaryDisplay1 = new HuwWenCapture.Controls.DictionaryDisplay();
            tabPage3 = new AntdUI.TabPage();
            panel2 = new Panel();
            settingDisplay1 = new HuwWenCapture.Controls.SettingDisplay();
            panel1 = new AntdUI.Panel();
            tabs1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            panel3.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tabPage3.SuspendLayout();
            panel2.SuspendLayout();
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
            tabs1.Controls.Add(tabPage2);
            tabs1.Controls.Add(tabPage1);
            tabs1.Controls.Add(tabPage3);
            tabs1.Dock = DockStyle.Fill;
            tabs1.Location = new Point(0, 0);
            tabs1.Name = "tabs1";
            tabs1.Pages.Add(tabPage1);
            tabs1.Pages.Add(tabPage2);
            tabs1.Pages.Add(tabPage3);
            tabs1.SelectedIndex = 1;
            tabs1.Size = new Size(1184, 461);
            tabs1.Style = styleLine1;
            tabs1.TabIndex = 0;
            tabs1.Text = "tabs1";
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(128, 255, 128);
            tabPage1.Controls.Add(translationDisplay1);
            tabPage1.Location = new Point(-2368, -862);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(1184, 431);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "翻译 (Translation )";
            // 
            // translationDisplay1
            // 
            translationDisplay1.BackColor = Color.White;
            translationDisplay1.Dock = DockStyle.Fill;
            translationDisplay1.Location = new Point(0, 0);
            translationDisplay1.Name = "translationDisplay1";
            translationDisplay1.Size = new Size(1184, 431);
            translationDisplay1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(panel3);
            tabPage2.Location = new Point(0, 30);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(1184, 431);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "字典 (Dictionary)";
            // 
            // panel3
            // 
            panel3.Controls.Add(tableLayoutPanel1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1184, 431);
            panel3.TabIndex = 0;
            panel3.Text = "panel3";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(input1, 0, 0);
            tableLayoutPanel1.Controls.Add(dictionaryDisplay1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.1073818F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 83.89262F));
            tableLayoutPanel1.Size = new Size(1184, 431);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // input1
            // 
            input1.Dock = DockStyle.Fill;
            input1.Location = new Point(3, 3);
            input1.Multiline = true;
            input1.Name = "input1";
            input1.Size = new Size(1178, 63);
            input1.TabIndex = 0;
            input1.TextChanged += OnDictionaryInputTextChanged;
            // 
            // dictionaryDisplay1
            // 
            dictionaryDisplay1.Dock = DockStyle.Fill;
            dictionaryDisplay1.Location = new Point(3, 72);
            dictionaryDisplay1.Name = "dictionaryDisplay1";
            dictionaryDisplay1.Size = new Size(1178, 356);
            dictionaryDisplay1.TabIndex = 1;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(panel2);
            tabPage3.Location = new Point(-2368, -862);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1184, 431);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "设置 (Settings)";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(settingDisplay1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1184, 431);
            panel2.TabIndex = 1;
            // 
            // settingDisplay1
            // 
            settingDisplay1.Location = new Point(0, 0);
            settingDisplay1.Name = "settingDisplay1";
            settingDisplay1.Size = new Size(973, 289);
            settingDisplay1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(tabs1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1184, 461);
            panel1.TabIndex = 1;
            panel1.Text = "panel1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 461);
            Controls.Add(panel1);
            Dark = true;
            Mode = AntdUI.TAMode.Dark;
            Name = "MainForm";
            Text = "华文 Capture";
            tabs1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            panel2.ResumeLayout(false);
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
        private Panel panel2;
        private Controls.SettingDisplay settingDisplay1;
        private AntdUI.Panel panel3;
        private TableLayoutPanel tableLayoutPanel1;
        private AntdUI.Input input1;
        private Controls.DictionaryDisplay dictionaryDisplay1;
    }
}
