using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Models;
using VideoGameManagerEF.Data;

namespace VideoGameManagerEF.Pages.Stats;

public class IndexStats : PageModel
{
    private readonly GameStoreContext _context;
    public IndexStats(GameStoreContext context) => _context = context;

    public List<Game> Games { get; set; } = [];
    public List<Game> TopGames { get; set; } = [];
    public List<DecadeStat> DecadeGames { get; set; } = [];
    public List<string> Genres { get; set; } = [];
    public List<DeveloperAvgStat> AvgDevelopers { get; set; } = [];
    public List<Game> GameResults { get; set; } = [];
    public List<Developer> SortedDevelopers { get; set; } = [];
    [BindProperty]
    public string SelectedGenre { get; set; }

    [BindProperty(SupportsGet = true)] 
    public int? DevGamesSort { get; set; }

    [BindProperty(SupportsGet = true)]
    public string SelectedTitle { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SelectedGenreFilter { get; set; }
    [BindProperty(SupportsGet = true)]
    public int? SelectedMinYear { get; set; }

    public async Task OnGet(string selectedGenre)
    {
        Genres = await _context.Games.Include(g => g.Developer).Select(g => g.Genre).Distinct().ToListAsync();
        await SortByGenre(selectedGenre);
        await SortTopGames();
        await GamesForDecade();
        await AverageDevelopers();
        await CombinedResearch();
        await SortDevGames();
    }

    public IActionResult OnPost()
    {
        return Page();
    }

    private async Task SortDevGames()
    {
        var query = _context.Developers.Include(d => d.Games).AsQueryable();
        if (DevGamesSort.HasValue)
        {
            DevGamesSort = DevGamesSort.Value;
            query = query.Where(d => d.Games.Count > DevGamesSort.Value);
        }
        SortedDevelopers = await query.OrderByDescending(d => d.Games.Count).ToListAsync();
    }

    private async Task SortByGenre(string selectedGenre)
    {
        var query = _context.Games.Include(g => g.Developer).AsQueryable();
        
        if (!string.IsNullOrEmpty(selectedGenre))
        {
            SelectedGenre = selectedGenre;
            query = query.Where(g => g.Genre == selectedGenre);
        }
        Games = await query.OrderByDescending(g => g.Score).ToListAsync();
    }

    private async Task SortTopGames()
    {
        TopGames = await _context.Games
            .Include(g => g.Developer)
            .OrderByDescending(g => g.Score)
            .Take(5)
            .ToListAsync();
    }

    private async Task GamesForDecade()
    {
        DecadeGames = await _context.Games
            .GroupBy(g => (g.Year / 10) * 10)
            .Select(grp => new DecadeStat 
            { 
                Decade = grp.Key, 
                Count = grp.Count() 
            })
            .OrderBy(x => x.Decade)
            .ToListAsync();
    }

    private async Task AverageDevelopers()
    {
        AvgDevelopers = await _context.Developers
            .Include(d => d.Games)
            .Where(d => d.Games.Any())
            .Select(d => new DeveloperAvgStat{
                Name = d.Name,
                GameCount = d.Games.Count,
                AvgScore = d.Games.Average(g => g.Score)
            })
            .OrderByDescending(x => x.AvgScore)
            .ToListAsync();
    }

    private async Task CombinedResearch()
    {
        var query = _context.Games.Include(g => g.Developer).AsQueryable();

        if (!string.IsNullOrEmpty(SelectedTitle))
            query = query.Where(g => g.Title.Contains(SelectedTitle));

        if (!string.IsNullOrEmpty(SelectedGenreFilter))
            query = query.Where(g => g.Genre == SelectedGenreFilter);

        if (SelectedMinYear.HasValue)
            query = query.Where(g => g.Year >= SelectedMinYear.Value);

        GameResults = await query.OrderBy(g => g.Title).ToListAsync();

    }
}
public class DecadeStat
{
    public int Decade { get; set; }
    public int Count { get; set; }
}

public class DeveloperAvgStat
{
    public string Name { get; set; }
    public int GameCount { get; set; }
    public double AvgScore { get; set; }
}