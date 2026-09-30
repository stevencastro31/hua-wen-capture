using JiebaNet.Segmenter;
using Microsoft.Data.Sqlite;
using DictionaryEntry = HuaWenCapture.Objects.DefinitionEntry;

namespace HuaWenCapture.Objects {
    internal static class DictionaryService {
        static readonly string DATABASE_PATH = Path.Combine(AppContext.BaseDirectory, "Resources", "Dictionary", "cedict.db");
        static readonly string SEGMENT_CONFIG_PATH = Path.Combine(AppContext.BaseDirectory, "Resources", "Segmenter");
        public static readonly JiebaSegmenter Segmenter = new();

        static DictionaryService() {
            JiebaNet.Segmenter.ConfigManager.ConfigFileBaseDir = SEGMENT_CONFIG_PATH;
        }

        public static List<DictionaryEntry> Lookup(string word) {
            List<DictionaryEntry> results = [];

            using SqliteConnection connection = new($"Data Source={DATABASE_PATH}");
            connection.Open();
            using SqliteCommand command = connection.CreateCommand();

            command.CommandText = """
                SELECT simplified, traditional, pinyin, definition
                FROM dictionary
                WHERE simplified = $word OR traditional = $word
            """;

            command.Parameters.AddWithValue("$word", word);
            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                DefinitionEntry entry = new(
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3)
                );
                results.Add(entry);
            }
            return results;
        }
    }
}
