using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HuwWenCapture.Helpers {
    public static class PythonEnvironment {
        private static readonly string[] RequiredPackages = { "argostranslate" };

        public static string RuntimeDir => Path.Combine(AppContext.BaseDirectory, "PythonRuntime");
        private static string PythonExe => Path.Combine(RuntimeDir, "python.exe");
        private static string SitePackagesDir => Path.Combine(RuntimeDir, "Lib", "site-packages");

        public static async Task EnsureReadyAsync(IProgress<string>? status = null) {
            if (!File.Exists(PythonExe))
                throw new FileNotFoundException("Base Python runtime missing — check csproj copy settings.", PythonExe);

            List<string> missing = RequiredPackages
                .Where(p => !Directory.Exists(Path.Combine(SitePackagesDir, p)))
                .ToList();

            if (missing.Count == 0) return;

            foreach (string package in missing) {
                status?.Report($"Installing {package}...");
                await RunPipInstall(package);
            }
        }

        private static async Task RunPipInstall(string package) {
            ProcessStartInfo psi = new ProcessStartInfo {
                FileName = PythonExe,
                Arguments = $"-m pip install {package}",
                WorkingDirectory = RuntimeDir,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using Process proc = Process.Start(psi)!;
            Task<string> stdoutTask = proc.StandardOutput.ReadToEndAsync();
            Task<string> stderrTask = proc.StandardError.ReadToEndAsync();

            await Task.WhenAll(stdoutTask, proc.WaitForExitAsync());
            string stdout = await stdoutTask;
            string stderr = await stderrTask;

            Debug.WriteLine($"[pip stdout]\n {stdout}");
            Debug.WriteLine($"[pip stderr]\n {stderr}");

            if (proc.ExitCode != 0)
                throw new InvalidOperationException($"pip install {package} failed:\n{stderr}");
            Debug.WriteLine($"Complete {package}...");
        }
    }
}
