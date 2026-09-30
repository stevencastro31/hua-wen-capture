using EasyImageSharp;
using EasyImageSharp.PixelFormats;
using EasyImageSharp.Processing;
using EasyOcrSharp.Models;
using EasyOcrSharp.Services;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using Image = EasyImageSharp.Image;

namespace HuaWenCapture.Objects {
    internal static class OCRService {
        private static readonly string MODEL_PATH = Path.Combine(AppContext.BaseDirectory, "Resources", "Models", "OCR");
        private static readonly Channel<OCRJob> _queue = Channel.CreateUnbounded<OCRJob>(new UnboundedChannelOptions() {
            SingleReader = true,
            SingleWriter = false,
            AllowSynchronousContinuations = false
        });

        public static async Task StartAsync(CancellationToken ct = default) {
            try {
                await foreach (OCRJob job in _queue.Reader.ReadAllAsync(ct).ConfigureAwait(false)) {
                    try {
                        OcrResult ocrResult = await Task.Run(() => ExtractText(job.Image)).ConfigureAwait(false);
                        string text = SanitizeChineseText(ocrResult.FullText);
                        float score = (float)(ocrResult.Lines.Sum(x => x.Confidence * x.Text.Length) / Math.Max(1, ocrResult.Lines.Sum(x => x.Text.Length)));   // weighted average

                        OCRJobResult result = new OCRJobResult(text, score);
                        job.OnResult(result);
                        //Debug.WriteLine($"Managed: {GC.GetTotalMemory(true) / 1024 / 1024} MB, " + $"Process: {Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024} MB");
                    } catch (Exception ex) {
                        try {
                            job.OnError?.Invoke(ex);
                        } catch (Exception ex2) {
                            Debug.WriteLine(ex2);
                        }
                    }
                }
            } catch (OperationCanceledException ex3) {
                Debug.WriteLine(ex3);
            }
        }

        public static async Task<OcrResult> ExtractText(byte[] data) {
            using EasyOcrService service = new(new EasyOcrServiceOptions() { ModelCachePath = MODEL_PATH, });
            using Image<Rgb24> image = Image.Load<Rgb24>(data);
            image.Mutate(x => x
                .Resize(image.Width * 3, image.Height * 3)
                .Grayscale()
                .Contrast(1.2f)
            );
            OcrResult result = await service.ExtractTextFromImage(image, ["ch_sim", "ch_tra"]);
            return result;
        }

        public static void Enqueue(byte[] image, Action<OCRJobResult> onResult, Action<Exception>? onError = null) {
            _queue.Writer.TryWrite(new OCRJob(image, onResult, onError));
        }

        public static void Stop() => _queue.Writer.Complete();

        public static string SanitizeChineseText(string text) {
            // remove spaces between Chinese characters
            text = Regex.Replace(text, @"(?<=[\u4E00-\u9FFF])\s+(?=[\u4E00-\u9FFF])", "");

            // remove spaces before Chinese punctuation
            text = Regex.Replace(text, @"\s+([，。！？；：、）】》』」])", "$1");

            // remove spaces after opening punctuation/brackets
            text = Regex.Replace(text, @"([（【《『「])\s+", "$1");
            return text.Trim();
        }
    }
}
