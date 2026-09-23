using HuwWenCapture.Objects;
using Python.Runtime;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace HuwWenCapture {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            ChineseTranslator.Shutdown();
        }

        private async void Form1_Load(object sender, EventArgs e) {
            await ChineseTranslator.Initialize();

            string input = ChineseOCR.PerformOCR();
            Debug.WriteLine($"OCR: {input}");

            string res = ChineseTranslator.TranslateToEnglish(input);
            Debug.WriteLine($"Result: {res}");
        }
    }
}
