using System;
using System.Collections.Generic;
using System.Text;

namespace HuaWenCapture.Objects {
    internal class OCRJob {
        public byte[] Image { get; init; }
        public Action<string> OnResult { get; init; }
        public Action<Exception>? OnError { get; init; }

        public OCRJob(byte[] image, Action<string> onResult, Action<Exception>? onError = null) {
            Image = image;
            OnResult = onResult;
            OnError = onError;
        }
    }
}
