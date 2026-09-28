using AntdUI;
using JiebaNet.Segmenter;
using Microsoft.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace HuwWenCapture.Objects {
    internal static partial class ChineseDictionary {
        static string path = Path.Combine(AppContext.BaseDirectory, "Data", "cedict.db");
        public static readonly JiebaSegmenter Segmenter = new();

        //CREATE TABLE dictionary(
        //  id INTEGER PRIMARY KEY,
        //  simplified TEXT NOT NULL,
        //  traditional TEXT NOT NULL,
        //  pinyin TEXT NOT NULL,        -- tone-marked (e.g. "zhōng guó")
        //  pinyin_search TEXT NOT NULL, -- normalized for search(e.g. "zhongguo")
        //  definition TEXT NOT NULL
        //);

        public static List<DictionaryEntry> Lookup(string word) {
            List<DictionaryEntry> results = [];

            using SqliteConnection connection = new($"Data Source={path}");
            connection.Open();
            using SqliteCommand command = connection.CreateCommand();

            command.CommandText = """
                SELECT simplified, traditional, pinyin, definition
                FROM dictionary
                WHERE simplified = $word OR traditional = $word;
            """;

            command.Parameters.AddWithValue("$word", word);

            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read()) {
                results.Add(new DictionaryEntry {
                    Simplified = reader.GetString(0),
                    Traditional = reader.GetString(1),
                    Pinyin = reader.GetString(2),
                    Definition = reader.GetString(3)
                });
            }
            return results;
        }
    }
}
