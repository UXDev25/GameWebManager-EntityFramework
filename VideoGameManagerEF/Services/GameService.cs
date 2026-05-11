using System.Diagnostics;
using VideoGameManager.Models;
using VideoGameManager.Models.Enums;
using VideoGameManager.Services;

namespace VideoGameManagerEF.Services;

public class GameService
{
    
    private List<Game> _games = new()
    {
        new() { Id=1, Title="The Legend of Zelda: TotK", Genre="Adventure",
            Year=2023, Score=9.8, Description="Open-world action RPG" },
        new() { Id=2, Title="Elden Ring", Genre="RPG",
            Year=2022, Score=9.5, Description="Open-world soulslike" },
        new() { Id=3, Title="Celeste", Genre="Platformer",
            Year=2018, Score=9.0, Description="Precision platformer" },
    };
    private int _nextId = 4;

    public List<Game> GetAll()
    {
        if (GameRepository.Read<Game>().Count <= 0)
        {
            GameRepository.Write(_games);
            Console.WriteLine("alo");
        }
        _games = GameRepository.Read<Game>();
        GameExporter.Write(_games);
        GamesRanking.Write(_games);
        return _games;
    }
    
    public Game? GetById(int id) => _games.FirstOrDefault(g => g.Id == id);

    public void Add(Game game)
    {
        game.Id = _nextId++; _games.Add(game); 
        FileManager.Append(game, ECrud.Create);
        GameRepository.SaveAll(_games);
        GameExporter.Append(game);
        GamesRanking.Append(game);
    }
    
    public void Update(Game game) {
        Console.WriteLine("Game id: " + game.Id);
        var index = _games.FindIndex(g => g.Id == game.Id);
        if (index >= 0) _games[index] = game;
        FileManager.Append(game, ECrud.Update);
        GameRepository.Write(_games);
        GameExporter.Write(_games);
        GamesRanking.Write(_games);
    }

    public void Delete(int id)
    {
        FileManager.Append(GetById(id), ECrud.Delete);
        _games.RemoveAll(g => g.Id == id);
        GameRepository.Write(_games);
        GameExporter.Write(_games);
        GamesRanking.Write(_games);
    }

    public void DeleteAll()
    {
        FileManager.Append("All", ECrud.Delete);
        _games.Clear();
        GameRepository.Write(_games);
        GameExporter.Write(_games);
        GamesRanking.Write(_games);
    }


}
