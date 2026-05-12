using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Models.Enums;
using VideoGameManager.Services;
using VideoGameManagerEF.Data;
using VideoGameManagerEF.Services;

namespace VideoGameManagerEF.Pages.Games;

public class DeleteModel : PageModel
{
    private readonly GameStoreContext _context;
    public DeleteModel(GameStoreContext context) => _context = context;
    
    [BindProperty]
    public Game? Game { get; set; }

    public void OnGet(int id)
    {
        Game = _context.FindAsync<Game>(id).Result;
    }

    public IActionResult OnPost()
    {
        if (Game == null) return Page();
        FileManager.Append(Game, ECrud.Delete);
        _context.Remove(Game);
        GameRepository.SaveAll(_context.Games.ToList());
        GameExporter.Append(_context.Games.ToList());
        GamesRanking.Append(_context.Games.ToList());
        return RedirectToPage("/Index");
    }
}