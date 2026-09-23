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
        //static Engine traChiEngine = new(@"./Data", Language.ChineseTraditional, EngineMode.LstmOnly);

        public static string PerformOCR() {
            TessImage img = TessImage.LoadFromFile("./Data/sublime_text_UamyBQT0If.png");
            TessPage page = simChiEngine.Process(img);
            string raw = page.Text;
            Debug.WriteLine($"Confidence: {page.MeanConfidence}");
            return raw.Replace(" ", "").Replace("\n", "");
        }
    }
}
