using System.Xml.Linq;
using VideoGameManager.Models;

namespace VideoGameManager.Services;

public class GamesRanking
{
    public const string DefPath = "./wwwroot/data/games_ranking.xml";
    private const string RootName = "VideoGameRanking";

    public static void CreateFile()
    {
        if (File.Exists(DefPath)) return;

        var directory = Path.GetDirectoryName(DefPath);
        if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);

        XDocument doc = new XDocument(new XElement(RootName));
        doc.Save(DefPath);
    }

    public static void Write(List<Game> data)
    {
        var elements = data.Select(item =>
            new XElement("Game",
                new XElement("Id", item.Id),
                new XElement("Title", item.Title),
                new XElement("Genre", item.Genre),
                new XElement("Year", item.Year),
                new XElement("Score", item.Score),
                new XElement("Description", item.Description)
            )
        );

        XDocument doc = new XDocument(new XElement(RootName, elements));
        doc.Save(DefPath);
    }

    public static List<Game> Read()
    {
        if (!File.Exists(DefPath)) return new List<Game>();

        XDocument doc = XDocument.Load(DefPath);

        return doc.Descendants("Game")
            .Select(e => new Game
            {
                Id = int.Parse(e.Element("Id")?.Value ?? "0"),
                Title = e.Element("Title")?.Value ?? "",
                Genre = e.Element("Genre")?.Value ?? "",
                Year = int.Parse(e.Element("Year")?.Value ?? "0"),
                Score = double.Parse(e.Element("Score")?.Value ?? "0.0", System.Globalization.CultureInfo.InvariantCulture),
                Description = e.Element("Description")?.Value ?? ""
            })
            .OrderByDescending(g => g.Score)
            .ToList();
    }

    public static void Append(Game? obj)
    {
        if (obj == null) return;
        
        CreateFile();

        var list = Read();
        list.Add(obj);
        
        Write(list);
    }

    public static void Append(List<Game>? objs)
    {
        if (objs == null || objs.Count == 0) return;

        CreateFile();

        var list = Read();
        list.AddRange(objs);

        Write(list);
    }
}