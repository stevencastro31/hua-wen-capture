using HuwWenCapture.Forms;
using HuwWenCapture.Objects;
using System.Runtime.CompilerServices;

namespace HuwWenCapture {
    internal static class Program {
        [STAThread]
        static async Task Main() {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}