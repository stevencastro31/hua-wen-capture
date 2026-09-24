using HuwWenCapture.Forms;
using HuwWenCapture.Objects;
using System.Runtime.CompilerServices;

namespace HuwWenCapture {
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main() {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            await ChineseTranslator.Initialize();
            RuntimeHelpers.RunClassConstructor(typeof(ChineseOCR).TypeHandle);

            Application.Run(new Form1());
        }
    }
}