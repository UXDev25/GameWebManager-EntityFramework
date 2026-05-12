using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // Necessari per SelectList
using Microsoft.EntityFrameworkCore;
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
    public Game Game { get; set; } = default!;
    public SelectList Developers { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Game = new Game();
        await LoadDevelopersAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadDevelopersAsync();
            return Page();
        }

        _context.Games.Add(Game);
        await _context.SaveChangesAsync();
        FileManager.Append(Game, ECrud.Create);
        GameRepository.SaveAll(_context.Games.ToList());
        GameExporter.Append(Game);
        GamesRanking.Append(Game);

        return RedirectToPage("./Index");
    }

    private async Task LoadDevelopersAsync()
    {
        var developers = await _context.Developers.ToListAsync();
        Developers = new SelectList(developers, "Id", "Name");
    }
}