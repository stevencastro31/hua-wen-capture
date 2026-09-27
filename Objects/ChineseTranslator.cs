using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TesseractOCR;
using TesseractOCR.Enums;

namespace HuwWenCapture.Objects {
    static class ChineseTranslator {
        private static dynamic? _translator;
        private static bool _isTranslatorObjectReady = false;

        public static string TranslateToEnglish(string chinese) {
            if (!PythonEngine.IsInitialized) throw new Exception("python environment not initialized, call the Intialize() method.");
            if (!_isTranslatorObjectReady) throw new Exception("translator script is not ready.");

            using (Py.GIL()) {                                          // run python code within .NET
                dynamic res = _translator!.translate(chinese);          // translate chinese text
                return (string)res;
            }
        }

        public static bool Initialize() {
            try {
                string RUNTIME_DIR = Path.Combine(AppContext.BaseDirectory, "PythonRuntime");
                Runtime.PythonDLL = Directory.GetFiles(RUNTIME_DIR, "python31*.dll").First();
                PythonEngine.Initialize();

                using (Py.GIL()) {                          // run python code within .NET
                    dynamic sys = Py.Import("sys");         // tell the Python Runtime where the base directory of the python code is.
                    string path = Path.Combine(AppContext.BaseDirectory, "PythonScripts");
                    sys.path.append(path);
                    _translator = Py.Import("translater");       // import translater module
                    string test = _translator.translate("开始");
                }
                _isTranslatorObjectReady = true;
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