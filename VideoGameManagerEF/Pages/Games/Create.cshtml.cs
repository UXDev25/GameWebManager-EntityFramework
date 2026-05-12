using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Models.Enums;
using VideoGameManager.Services;
using VideoGameManagerEF.Data;
using VideoGameManagerEF.Services;

namespace VideoGameManagerEF.Pages.Games;

public class CreateModel : PageModel
{
    private readonly GameStoreContext _context;
    public CreateModel(GameStoreContext context) => _context = context;
    [BindProperty]
    public Game Game { get; set; }
    public List<Developer> Developers { get; set; }

    public void OnGet()
    {
        Game = new Game();
        Developers = _context.Developers.ToList();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        _context.Add(Game);
        FileManager.Append(Game, ECrud.Create);
        GameRepository.SaveAll(_context.Games);
        GameExporter.Append(Game);
        GamesRanking.Append(Game);
        _context.SaveChanges();
        return RedirectToPage("/Index");
    }
}