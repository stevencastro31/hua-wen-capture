using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using TesseractOCR;
using TesseractOCR.Enums;
using TessImage = TesseractOCR.Pix.Image;
using TessPage = TesseractOCR.Page;

namespace HuwWenCapture.Objects {
    static class ChineseOCR {
        static readonly Engine simChiEngine = new(@"./Data", Language.ChineseSimplified, EngineMode.LstmOnly);
        static readonly Engine traChiEngine = new(@"./Data", Language.ChineseTraditional, EngineMode.LstmOnly);

        public static (string, float) PerformOCR(byte[] image) {
            TessImage img = TessImage.LoadFromMemory(image);

            TessPage page1 = simChiEngine.Process(img);
            TessPage page2 = traChiEngine.Process(img);
            string raw = page1.MeanConfidence > page2.MeanConfidence ? page1.Text : page2.Text;
            float score = page1.MeanConfidence > page2.MeanConfidence ? page1.MeanConfidence : page2.MeanConfidence;

            img.Dispose();
            page1.Dispose();
            page2.Dispose();

            return (CleanOCRText(raw), score);
        }

        static string CleanOCRText(string text) {
            // remove spaces between Chinese characters
            text = Regex.Replace(text, @"(?<=[\u4E00-\u9FFF])\s+(?=[\u4E00-\u9FFF])", "");

            // remove spaces before Chinese punctuation
            text = Regex.Replace(text, @"\s+([，。！？；：、）】》』」])", "$1");

            // remove spaces after opening punctuation/brackets
            text = Regex.Replace(text,@"([（【《『「])\s+","$1");
            return text.Trim();
        }
    }
}
