using VideoGameManager.Models;
using VideoGameManager.Models.Enums;

namespace VideoGameManager.Services;

public static class FileManager
{
    private const string DefPath = "./wwwroot/data/gameLog.Txt";
    //FILE READING
    public static List<T>? Read<T>(Func<string, T> parser)
    {
        List<T> list = new List<T>();
        
        if (!File.Exists(DefPath))
        {
            return null;
        }
        string[] lines = File.ReadAllLines(DefPath);

        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                string cleanLine = line.Trim();

                try
                {
                    T obj = parser(cleanLine);
                    list.Add(obj);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error parsing line: {cleanLine}");
                }
            }
        }

        return list;
    }
    
    //FILE SAVING
    public static void Append(Game? obj, ECrud action)
    {
        var content = obj?.ToStringAlt(action);

        if (File.Exists(DefPath) && new FileInfo(DefPath).Length > 0)
        {
            File.AppendAllText(DefPath, Environment.NewLine + content);
        }
        else
        {
            File.AppendAllText(DefPath, content);
        }
    }
    
    public static void Append(string content, ECrud action)
    {

        if (File.Exists(DefPath) && new FileInfo(DefPath).Length > 0)
        {
            File.AppendAllText(DefPath, Environment.NewLine + content);
        }
        else
        {
            File.AppendAllText(DefPath, content);
        }
    }
}