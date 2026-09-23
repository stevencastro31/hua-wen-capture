using HuwWenCapture.Helpers;
using Microsoft.VisualBasic.ApplicationServices;
using Python.Runtime;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace HuwWenCapture {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        string TranslateChineseToEnglish(string chinese) {
            using (Py.GIL()) {
                // tell the Python Runtime where the base directory of the python code is.
                dynamic sys = Py.Import("sys");
                string path = Path.Combine(AppContext.BaseDirectory, "PythonScripts");
                sys.path.append(path);

                Debug.WriteLine(Path.Combine(AppContext.BaseDirectory, "PythonScripts"));

                // import custom module
                dynamic translator = Py.Import("translater");

                // translate
                dynamic res = translator.translate(chinese);
                return (string)res;
            }
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            Debug.WriteLine("Shutting down Python Engine...");
            PythonEngine.Shutdown();
            Debug.WriteLine("Python Engine Shutdown.");
        }

        private async void Form1_Load(object sender, EventArgs e) {
            try {
                await PythonEnvironment.EnsureReadyAsync(new Progress<string>(msg => Debug.WriteLine(msg)));
                Runtime.PythonDLL = Directory.GetFiles(PythonEnvironment.RuntimeDir, "python31*.dll").First();
                Debug.WriteLine(Runtime.PythonDLL);
                PythonEngine.Initialize();

            } catch (Exception ex) {
                MessageBox.Show($"Setup failed:\n{ex.Message}");

            } finally {
                string result = TranslateChineseToEnglish("我迷路了！");
                Debug.WriteLine(result);
            }
        }
    }
}
