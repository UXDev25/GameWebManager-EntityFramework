using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Models.Enums;
using VideoGameManager.Services;
using VideoGameManagerEF.Data;
using VideoGameManagerEF.Services;

namespace VideoGameManagerEF.Pages.Games;

public class EditModel: PageModel
{
    private readonly GameStoreContext _context;
    public EditModel(GameStoreContext context) => _context = context;
    
    [BindProperty]
    public Game? Game { get; set; }
    public List<Developer> Developers { get; set; }

    public void OnGet(int id)
    {
        Game = _context.FindAsync<Game>(id).Result;
        Developers = _context.Developers.ToList();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        Console.WriteLine("Game id at post: " + Game?.Id);
        if (Game != null) _context.SaveChanges();
        FileManager.Append(Game, ECrud.Update);
        GameRepository.SaveAll(_context.Games.ToList());
        GameExporter.Append(_context.Games.ToList());
        GamesRanking.Append(_context.Games.ToList());
        return RedirectToPage("/Index");
    }
}

