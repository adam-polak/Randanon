using Newtonsoft.Json;

namespace WebAPI.DataAccess.Lib;

public class FileWithKey : IEquatable<FileWithKey>
{
    public required string FileName { get; set; }
    public required string Key { get; set; }

    public bool Equals(FileWithKey? other)
    {
        if(other == null) return false;
        Console.WriteLine(Key + " " + other.Key);
        return FileName.Equals(other.FileName) && Key.Equals(other.Key);
    }
}

public static class JsonInfoRetriever
{
    private static Dictionary<FileWithKey, string> Pairs = [];

    public static string GetValue(string fileName, string key)
    {
        // FileWithKey fk = new FileWithKey() { FileName = fileName, Key = key };
        // Check cache for value, dict gets corrupt with async calls
        // if(Pairs.ContainsKey(fk)) return Pairs.GetValueOrDefault(fk) ?? "";
        string value = "";
        using(JsonTextReader reader = new JsonTextReader(new StreamReader(fileName)))
        {
            while(reader.Read())
            {
                if(reader.TokenType.ToString().Equals("PropertyName"))
                {
                    string k = reader.Value != null ? reader.Value.ToString() ?? "" : "";
                    if(!k.Equals(key)) continue;
                    reader.Read();
                    value = reader.Value != null ? reader.Value.ToString() ?? "" : "";
                    break;
                }
            }
        }

        // Add query to cache
        // Pairs.Add(fk, value);
        return value;
    }
}