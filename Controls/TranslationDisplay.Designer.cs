namespace HuwWenCapture.Controls {
    partial class TranslationDisplay {
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
            table1 = new AntdUI.Table();
            SuspendLayout();
            // 
            // table1
            // 
            table1.ClipboardCopyFocusedCell = true;
            table1.Dock = DockStyle.Fill;
            table1.EditMode = AntdUI.TEditMode.DoubleClick;
            table1.Gap = 12;
            table1.Location = new Point(0, 0);
            table1.Name = "table1";
            table1.Size = new Size(500, 500);
            table1.TabIndex = 0;
            table1.Text = "table1";
            // 
            // TranslationDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(table1);
            Name = "TranslationDisplay";
            Size = new Size(500, 500);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Table table1;
    }
}
