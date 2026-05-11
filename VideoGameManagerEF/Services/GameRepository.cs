using System.Text.Json;
using VideoGameManager.Models;

namespace VideoGameManager.Services;

public static class GameRepository
{
    public const string DefPath = "./wwwroot/data/games.json";
    private static List<Game>? _records;

    public static void LoadAll(IFormFile file)
    {
        var games = JsonSerializer.Deserialize<List<Game>>(file.OpenReadStream());
        if (games != null) Write(games);
    }

    public static void SaveAll(IEnumerable<Game> games)
    {
        foreach (var game in games)
        {
            Append(game);
            _records?.Add(game);
        }
    }
    
    public static List<Game> Read<Game>()
    {
        if (!File.Exists(DefPath))
        {
            return new List<Game>();
        }
        string json = File.ReadAllText(DefPath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<Game>();
        }
        return JsonSerializer.Deserialize<List<Game>>(json) ?? new List<Game>();
    }
    
    public static void Write<Game>(List<Game> records)
    {
        if (string.IsNullOrWhiteSpace(DefPath)) throw new ArgumentException("invalid path", nameof(DefPath));
        if (records == null) throw new ArgumentNullException(nameof(records));
        
        var directory = Path.GetDirectoryName(DefPath);
        if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
        
        string json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(DefPath, json);
    }
    
    public static void Append(Game obj)
    {
        if (string.IsNullOrWhiteSpace(DefPath)) throw new ArgumentException("invalid path", nameof(DefPath));
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        
        var records = Read<Game>();

        if (!records.Contains(obj))
        {
            records.Add(obj);
        }
        Write(records);
    }
    
    public static void Append<Game>(List<Game> objs)
    {

        if (string.IsNullOrWhiteSpace(DefPath))
        {
            throw new ArgumentException("invalid path", nameof(DefPath));
        }

        if (objs == null || objs.Count == 0)
        {
            throw new ArgumentException("The list of objects to append cannot be null or empty.", nameof(objs));
        }

        var records = Read<Game>();
        records.AddRange(objs);
        Write(records);
    }
}