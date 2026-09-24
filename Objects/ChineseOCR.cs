using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TesseractOCR;
using TesseractOCR.Enums;
using TessImage = TesseractOCR.Pix.Image;
using TessPage = TesseractOCR.Page;
using System.Drawing;

namespace HuwWenCapture.Objects {
    static class ChineseOCR {
        static readonly Engine simChiEngine = new(@"./Data", Language.ChineseSimplified, EngineMode.LstmOnly);
        static readonly Engine traChiEngine = new(@"./Data", Language.ChineseTraditional, EngineMode.LstmOnly);

        public static string PerformOCR(byte[] image) {
            TessImage img = TessImage.LoadFromMemory(image);

            TessPage page1 = simChiEngine.Process(img);
            TessPage page2 = traChiEngine.Process(img);
            string raw = page1.MeanConfidence > page2.MeanConfidence ? page1.Text : page2.Text;
            float score = page1.MeanConfidence > page2.MeanConfidence ? page1.MeanConfidence : page2.MeanConfidence;

            img.Dispose();
            page1.Dispose();
            page2.Dispose();

            return score + ", " + raw.Replace(" ", "").Replace("\n", "");
        }
    }
}
