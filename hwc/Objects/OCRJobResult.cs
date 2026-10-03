namespace HuaWenCapture.Objects {
    internal class OCRJobResult {
        public string Text { get; set; }
        public float Confidence { get; set; }

        public OCRJobResult(string text, float confidence) {
            this.Text = text;
            this.Confidence = confidence;
        }
    }
}
