using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HuwWenCapture.Controls {
    public partial class TranslationList : UserControl {
        public TranslationList() {
            InitializeComponent();
            this.Resize += OnResize;
        }

        public void AddTranslationEntry(string chinese, string english) {
            int width = flowLayoutPanel1.Width;
            Label zh = new Label() { Text = chinese, Width = width, };
            Label en = new Label() { Text = english, Width = width, };

            int finalHeight = CalculateLabelHeight(zh, en, width);
            zh.Height = finalHeight;
            en.Height = finalHeight;

            flowLayoutPanel1.Controls.Add(zh);
            flowLayoutPanel2.Controls.Add(en);
        }

        private int CalculateLabelHeight(Label label1, Label label2, int width) {
            int enHeight = TextRenderer.MeasureText(label1.Text, label1.Font, new Size(width, 0), TextFormatFlags.WordBreak).Height;
            int zhHeight = TextRenderer.MeasureText(label2.Text, label2.Font, new Size(width, 0), TextFormatFlags.WordBreak).Height;
            return Math.Max(enHeight, zhHeight);
        }

        private void OnResize(object? sender, EventArgs e) {
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++) {
                Label label1 = (Label)flowLayoutPanel1.Controls[i];
                Label label2 = (Label)flowLayoutPanel2.Controls[i];

                Debug.WriteLine(flowLayoutPanel1.Width);

                int finalHeight = CalculateLabelHeight(label1, label2, flowLayoutPanel1.Width);
                label1.Size = new Size(flowLayoutPanel1.Width, finalHeight);
                label2.Size = new Size(flowLayoutPanel1.Width, finalHeight);
            }
        }
    }
}
