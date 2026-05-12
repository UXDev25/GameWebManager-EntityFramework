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
    
    [BindProperty]
    public string SelectedGenre { get; set; }

    public async Task OnGet(string selectedGenre)
    {
        Genres = await _context.Games.Include(g => g.Developer).Select(g => g.Genre).Distinct().ToListAsync();
        
        var query = _context.Games.Include(g => g.Developer).AsQueryable();
        
        if (!string.IsNullOrEmpty(selectedGenre))
        {
            SelectedGenre = selectedGenre;
            query = query.Where(g => g.Genre == selectedGenre);
        }
        Games = await query.OrderByDescending(g => g.Score).ToListAsync();
        TopGames = await _context.Games
            .Include(g => g.Developer)
            .OrderByDescending(g => g.Score)
            .Take(5)
            .ToListAsync();
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

    public IActionResult OnPost()
    {
        return Page();
    }
}
public class DecadeStat
{
    public int Decade { get; set; }
    public int Count { get; set; }
}