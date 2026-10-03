using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.Tokenizers;
using System.Text;
using System.Text.Json;

namespace HuaWenCapture.Objects {
    internal static class TranslationService {
        private static InferenceSession? _encoder;
        private static InferenceSession? _decoder;

        private static readonly SentencePieceTokenizer _tokenizer;
        private static readonly Dictionary<string, int> _vocab;
        private static readonly Dictionary<int, string> _idToToken;

        private static readonly int _padId, _eosId, _unkId, _decoderStartId, _maxLength;

        private static readonly string MODEL_ROOT_PATH = Path.Combine(AppContext.BaseDirectory, "Resources", "Models", "Translation");
        private static readonly string ENCODER_MODEL_PATH = Path.Combine(MODEL_ROOT_PATH, "encoder_model.onnx");
        private static readonly string DECODER_MODEL_PATH = Path.Combine(MODEL_ROOT_PATH, "decoder_model.onnx");
        private static readonly string CONFIG_PATH = Path.Combine(MODEL_ROOT_PATH, "config.json");
        private static readonly string VOCAB_PATH = Path.Combine(MODEL_ROOT_PATH, "vocab.json");
        private static readonly string SOURCE_PATH = Path.Combine(MODEL_ROOT_PATH, "source.spm");

        static TranslationService() {
            using FileStream fsSPM = File.OpenRead(SOURCE_PATH);
            _tokenizer = SentencePieceTokenizer.Create(fsSPM, addBeginningOfSentence: false, addEndOfSentence: false);

            _vocab = JsonSerializer.Deserialize<Dictionary<string, int>>(File.ReadAllText(VOCAB_PATH))!;
            _idToToken = _vocab.ToDictionary(kv => kv.Value, kv => kv.Key);

            using JsonDocument fsConfig = JsonDocument.Parse(File.OpenRead(CONFIG_PATH));
            JsonElement root = fsConfig.RootElement;
            _padId = root.GetProperty("pad_token_id").GetInt32();
            _eosId = root.GetProperty("eos_token_id").GetInt32();
            _decoderStartId = root.GetProperty("decoder_start_token_id").GetInt32();
            _maxLength = Math.Min(root.TryGetProperty("max_length", out var ml) ? ml.GetInt32() : 512, 512);
            _unkId = _vocab.TryGetValue("<unk>", out var u) ? u : 1;
        }

        public static string Translate(string text) {
            LoadModels();

            // tokenize text and swap them out to their respective ids
            IReadOnlyList<EncodedToken> pieces = _tokenizer.EncodeToTokens(text, out _);
            long[] inputIds = pieces
                .Select(p => _vocab.TryGetValue(p.Value, out var id) ? id : _unkId)
                .Append(_eosId)
                .Select(i => (long)i)
                .ToArray();

            // present text ids as tensors
            int srcLen = inputIds.Length;
            DenseTensor<long> idsTensor = new(inputIds, new[] { 1, srcLen });                                   // 1xn dimension tensor
            DenseTensor<long> maskTensor = new(Enumerable.Repeat(1L, srcLen).ToArray(), new[] { 1, srcLen });   // 1xn dimension tensor

            // encode
            using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> encoderResults = _encoder!.Run(new[] {
                NamedOnnxValue.CreateFromTensor("input_ids", idsTensor),
                NamedOnnxValue.CreateFromTensor("attention_mask", maskTensor),
            });
            Tensor<float> encoderHidden = encoderResults.First(r => r.Name == "last_hidden_state").AsTensor<float>();

            // decode: greedy loop (no KV cache: re-feeds the full prefix each step)
            List<long> generated = new List<long> { _decoderStartId };
            for (int step = 0; step < _maxLength; step++) {
                DenseTensor<long> decoderIds = new(generated.ToArray(), new[] { 1, generated.Count });

                using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> decoderResults = _decoder!.Run(new[] {
                    NamedOnnxValue.CreateFromTensor("input_ids", decoderIds),
                    NamedOnnxValue.CreateFromTensor("encoder_attention_mask", maskTensor),
                    NamedOnnxValue.CreateFromTensor("encoder_hidden_states", encoderHidden),
                });

                Tensor<float> logits = decoderResults.First(r => r.Name == "logits").AsTensor<float>();
                int vocabSize = logits.Dimensions[2];
                int last = generated.Count - 1;

                int best = 0;
                float bestScore = float.NegativeInfinity;
                for (int v = 0; v < vocabSize; v++) {
                    if (v == _padId) continue;          // never generate pad
                    float s = logits[0, last, v];
                    if (s > bestScore) { bestScore = s; best = v; }
                }

                if (best == _eosId) break;
                generated.Add(best);
            }

            // detokenize: convert ids to words
            StringBuilder builder = new();
            foreach (long id in generated.Skip(1))
                if (_idToToken.TryGetValue((int)id, out var token) && token != "<unk>")
                    builder.Append(token);
            return builder.ToString().Replace('\u2581', ' ').Trim();
        }
    
        private static void LoadModels() {
            if (_encoder != null && _decoder != null) return;
            SessionOptions opts = new() { GraphOptimizationLevel = GraphOptimizationLevel.ORT_ENABLE_ALL };
            _encoder = new InferenceSession(ENCODER_MODEL_PATH, opts);
            _decoder = new InferenceSession(DECODER_MODEL_PATH, opts);
        }

        public static void DisposeModels() {
            _encoder?.Dispose();
            _decoder?.Dispose();
            _encoder = null;
            _decoder = null;
        }
    }
}
