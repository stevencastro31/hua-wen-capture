using EasyImageSharp;
using EasyImageSharp.PixelFormats;
using EasyImageSharp.Processing;
using EasyOcrSharp;
using EasyOcrSharp.Models;
using EasyOcrSharp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Channels;
using Image = EasyImageSharp.Image;

namespace HuaWenCapture.Objects {
    internal static class OCRService {
        private static readonly string MODEL_PATH = Path.Combine(AppContext.BaseDirectory, "Resources", "Models");
        private static readonly Channel<OCRJob> _queue = Channel.CreateUnbounded<OCRJob>(new UnboundedChannelOptions() {
            SingleReader = true,
            SingleWriter = false,
            AllowSynchronousContinuations = false
        });

        public static async Task StartAsync(CancellationToken ct = default) {
            try {
                await foreach (OCRJob job in _queue.Reader.ReadAllAsync(ct).ConfigureAwait(false)) {
                    try {
                        string text = await Task.Run(() => ExtractText(job.Image)).ConfigureAwait(false);
                        job.OnResult(text);
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

        public static async Task<string> ExtractText(byte[] data) {
            using EasyOcrService service = new(new EasyOcrServiceOptions() { ModelCachePath = MODEL_PATH, });
            using Image<Rgb24> image = Image.Load<Rgb24>(data);
            image.Mutate(x => x
                .Resize(image.Width * 3, image.Height * 3)
                .Grayscale()
                .Contrast(1.2f)
            );
            OcrResult result = await service.ExtractTextFromImage(image, ["ch_sim", "ch_tra"]);
            return result.FullText;
        }

        public static void Enqueue(byte[] image, Action<string> onResult, Action<Exception>? onError = null) {
            _queue.Writer.TryWrite(new OCRJob(image, onResult, onError));
        }

        public static void Stop() => _queue.Writer.Complete();
    }
}
