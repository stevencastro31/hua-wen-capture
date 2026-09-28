namespace HuwWenCapture.Forms {
    partial class DictionaryDialog {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            dictionaryDisplay1 = new HuwWenCapture.Controls.DictionaryDisplay();
            SuspendLayout();
            // 
            // dictionaryDisplay1
            // 
            dictionaryDisplay1.Dock = DockStyle.Fill;
            dictionaryDisplay1.Location = new Point(0, 0);
            dictionaryDisplay1.Name = "dictionaryDisplay1";
            dictionaryDisplay1.Size = new Size(984, 601);
            dictionaryDisplay1.TabIndex = 0;
            // 
            // DictionaryDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 601);
            Controls.Add(dictionaryDisplay1);
            MinimumSize = new Size(500, 0);
            Name = "DictionaryDialog";
            Text = "Dictionary";
            ResumeLayout(false);
        }

        #endregion

        private Controls.DictionaryDisplay dictionaryDisplay1;
    }
}