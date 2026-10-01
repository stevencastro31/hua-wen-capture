namespace HuaWenCapture {
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
            tabs1 = new AntdUI.Tabs();
            tabPage3 = new AntdUI.TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            label3 = new AntdUI.Label();
            label1 = new AntdUI.Label();
            label2 = new AntdUI.Label();
            switch1 = new AntdUI.Switch();
            buttonShadow1 = new AntdUI.ButtonShadow();
            buttonShadow2 = new AntdUI.ButtonShadow();
            tabPage1 = new AntdUI.TabPage();
            translationTable1 = new HuaWenCapture.Controls.TranslationTable();
            tabPage2 = new AntdUI.TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            dictionaryTable1 = new HuaWenCapture.Controls.DictionaryTable();
            input1 = new AntdUI.Input();
            notifyIcon1 = new NotifyIcon(components);
            tabs1.SuspendLayout();
            tabPage3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabs1
            // 
            tabs1.BackColor = Color.White;
            tabs1.Controls.Add(tabPage3);
            tabs1.Controls.Add(tabPage1);
            tabs1.Controls.Add(tabPage2);
            tabs1.Dock = DockStyle.Fill;
            tabs1.Font = new Font("Microsoft YaHei UI", 14F);
            tabs1.Location = new Point(0, 0);
            tabs1.Name = "tabs1";
            tabs1.Pages.Add(tabPage1);
            tabs1.Pages.Add(tabPage2);
            tabs1.Pages.Add(tabPage3);
            tabs1.SelectedIndex = 2;
            tabs1.Size = new Size(984, 461);
            tabs1.Style = styleLine1;
            tabs1.TabIndex = 0;
            tabs1.Text = "tabs1";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(tableLayoutPanel2);
            tabPage3.Font = new Font("Microsoft YaHei UI", 14F);
            tabPage3.Location = new Point(0, 38);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(984, 423);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "设置 (Settings)";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(label3, 0, 2);
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(label2, 0, 1);
            tableLayoutPanel2.Controls.Add(switch1, 1, 0);
            tableLayoutPanel2.Controls.Add(buttonShadow1, 1, 1);
            tableLayoutPanel2.Controls.Add(buttonShadow2, 1, 2);
            tableLayoutPanel2.Location = new Point(12, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 4;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(960, 214);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // label3
            // 
            label3.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold);
            label3.Location = new Point(3, 83);
            label3.Name = "label3";
            label3.Size = new Size(194, 34);
            label3.TabIndex = 3;
            label3.Text = "Capture Hotkey";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold);
            label1.Location = new Point(3, 3);
            label1.Name = "label1";
            label1.Size = new Size(194, 34);
            label1.TabIndex = 0;
            label1.Text = "Start Minimized";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Font = new Font("Microsoft YaHei UI", 14.25F, FontStyle.Bold);
            label2.Location = new Point(3, 43);
            label2.Name = "label2";
            label2.Size = new Size(194, 34);
            label2.TabIndex = 2;
            label2.Text = "Capture Directory";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // switch1
            // 
            switch1.Dock = DockStyle.Left;
            switch1.Location = new Point(203, 3);
            switch1.Name = "switch1";
            switch1.Size = new Size(75, 34);
            switch1.TabIndex = 4;
            switch1.Text = "switch1";
            // 
            // buttonShadow1
            // 
            buttonShadow1.Dock = DockStyle.Fill;
            buttonShadow1.Location = new Point(203, 43);
            buttonShadow1.MinimumSize = new Size(400, 0);
            buttonShadow1.Name = "buttonShadow1";
            buttonShadow1.Size = new Size(754, 34);
            buttonShadow1.TabIndex = 5;
            buttonShadow1.Text = "buttonShadow1";
            buttonShadow1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // buttonShadow2
            // 
            buttonShadow2.Dock = DockStyle.Fill;
            buttonShadow2.Location = new Point(203, 83);
            buttonShadow2.MinimumSize = new Size(400, 0);
            buttonShadow2.Name = "buttonShadow2";
            buttonShadow2.Size = new Size(754, 34);
            buttonShadow2.TabIndex = 6;
            buttonShadow2.Text = "buttonShadow2";
            buttonShadow2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(translationTable1);
            tabPage1.Font = new Font("Microsoft YaHei UI", 14F);
            tabPage1.Location = new Point(-1968, -846);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(984, 423);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "翻译 (Translations)";
            // 
            // translationTable1
            // 
            translationTable1.Dock = DockStyle.Fill;
            translationTable1.Location = new Point(0, 0);
            translationTable1.Margin = new Padding(6, 5, 6, 5);
            translationTable1.Name = "translationTable1";
            translationTable1.Size = new Size(984, 423);
            translationTable1.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(tableLayoutPanel1);
            tabPage2.Font = new Font("Microsoft YaHei UI", 14F);
            tabPage2.Location = new Point(-1968, -846);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(984, 423);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "字典 （Dictionary)";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(dictionaryTable1, 0, 1);
            tableLayoutPanel1.Controls.Add(input1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(984, 423);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // dictionaryTable1
            // 
            dictionaryTable1.Dock = DockStyle.Fill;
            dictionaryTable1.Location = new Point(5, 69);
            dictionaryTable1.Margin = new Padding(5);
            dictionaryTable1.Name = "dictionaryTable1";
            dictionaryTable1.Size = new Size(974, 349);
            dictionaryTable1.TabIndex = 0;
            // 
            // input1
            // 
            input1.Dock = DockStyle.Fill;
            input1.Location = new Point(3, 3);
            input1.Name = "input1";
            input1.Size = new Size(978, 58);
            input1.TabIndex = 1;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 461);
            Controls.Add(tabs1);
            Dark = true;
            Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Mode = AntdUI.TAMode.Dark;
            Name = "MainForm";
            Text = "华文 Capture";
            tabs1.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private AntdUI.Tabs tabs1;
        private AntdUI.TabPage tabPage1;
        private AntdUI.TabPage tabPage2;
        private AntdUI.TabPage tabPage3;
        private Controls.TranslationTable translationTable1;
        private TableLayoutPanel tableLayoutPanel1;
        private Controls.DictionaryTable dictionaryTable1;
        private AntdUI.Input input1;
        private NotifyIcon notifyIcon1;
        private AntdUI.Label label1;
        private TableLayoutPanel tableLayoutPanel2;
        private AntdUI.Label label3;
        private AntdUI.Label label2;
        private AntdUI.Switch switch1;
        private AntdUI.ButtonShadow buttonShadow1;
        private AntdUI.ButtonShadow buttonShadow2;
    }
}
