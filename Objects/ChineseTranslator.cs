using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TesseractOCR;
using TesseractOCR.Enums;

namespace HuwWenCapture.Objects {
    static class ChineseTranslator {

        public static string TranslateToEnglish(string chinese) {
            if (!PythonEngine.IsInitialized) throw new Exception("Python Environment not initialized, call the Intialize() method.");

            using (Py.GIL()) {                          // run python code within .NET
                dynamic sys = Py.Import("sys");         // tell the Python Runtime where the base directory of the python code is.
                string path = Path.Combine(AppContext.BaseDirectory, "PythonScripts");
                sys.path.append(path);

                dynamic translator = Py.Import("translater");       // import translater module
                dynamic res = translator.translate(chinese);        // translate chinese text
                return (string)res;
            }
        }

        public static async Task<bool> Initialize() {
            try {
                //await PythonEnvironment.EnsureReadyAsync(new Progress<string>(msg => Debug.WriteLine(msg)));
                Runtime.PythonDLL = Directory.GetFiles(PythonEnvironment.RuntimeDir, "python31*.dll").First();
                PythonEngine.Initialize();
                Debug.WriteLine("translator ready");
            } catch (Exception ex) {
                MessageBox.Show($"setup failed:\n{ex.Message}");
                return false;
            } 
            return true;
        }

        public static void Shutdown() {
            if (PythonEngine.IsInitialized) {
                PythonEngine.Shutdown();
                Debug.WriteLine("python engine shutdown complete");
            }
        }
    }
}