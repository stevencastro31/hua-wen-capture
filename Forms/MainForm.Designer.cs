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
            AntdUI.Tabs.StyleLine styleLine1 = new AntdUI.Tabs.StyleLine();
            tabs1 = new AntdUI.Tabs();
            tabPage1 = new AntdUI.TabPage();
            translationTable1 = new HuaWenCapture.Controls.TranslationTable();
            tabPage2 = new AntdUI.TabPage();
            tabPage3 = new AntdUI.TabPage();
            button1 = new Button();
            tabs1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // tabs1
            // 
            tabs1.BackColor = Color.White;
            tabs1.Controls.Add(tabPage1);
            tabs1.Controls.Add(tabPage2);
            tabs1.Controls.Add(tabPage3);
            tabs1.Dock = DockStyle.Fill;
            tabs1.Font = new Font("Microsoft YaHei UI", 14F);
            tabs1.Location = new Point(0, 0);
            tabs1.Name = "tabs1";
            tabs1.Pages.Add(tabPage1);
            tabs1.Pages.Add(tabPage2);
            tabs1.Pages.Add(tabPage3);
            tabs1.Size = new Size(984, 461);
            tabs1.Style = styleLine1;
            tabs1.TabIndex = 1;
            tabs1.Text = "tabs1";
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(translationTable1);
            tabPage1.Font = new Font("Microsoft YaHei UI", 14F);
            tabPage1.Location = new Point(0, 38);
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
            tabPage2.Font = new Font("Microsoft YaHei UI", 14F);
            tabPage2.Location = new Point(0, 0);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(0, 0);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "字典 （Dictionary)";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(button1);
            tabPage3.Font = new Font("Microsoft YaHei UI", 14F);
            tabPage3.Location = new Point(-1568, -838);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(784, 419);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "设置 (Settings)";
            // 
            // button1
            // 
            button1.Location = new Point(450, 212);
            button1.Name = "button1";
            button1.Size = new Size(207, 111);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
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
            tabPage1.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private AntdUI.Tabs tabs1;
        private AntdUI.TabPage tabPage1;
        private AntdUI.TabPage tabPage2;
        private AntdUI.TabPage tabPage3;
        private Button button1;
        private Controls.TranslationTable translationTable1;
    }
}
