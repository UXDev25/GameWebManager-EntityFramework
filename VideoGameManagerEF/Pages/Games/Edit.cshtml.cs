using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    
    public SelectList DeveloperList { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Game = _context.FindAsync<Game>(id).Result;
        if (Game == null)
        {
            return NotFound();
        }
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

        _context.Entry(Game).State = EntityState.Modified;
        
        if (Game != null)
        {
            _context.Attach(Game).State = EntityState.Modified;
            
            try 
            {
                await _context.SaveChangesAsync();
                FileManager.Append(Game, ECrud.Update);
                GameRepository.SaveAll(_context.Games.ToList());
                GameExporter.Append(_context.Games.ToList());
                GamesRanking.Append(_context.Games.ToList());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Games.Any(e => e.Id == Game.Id)) return NotFound();
                else throw;
            }
        }

        return RedirectToPage("./Index");
    }
    
    private async Task LoadDevelopersAsync()
    {
        var developers = await _context.Developers.ToListAsync();
        DeveloperList = new SelectList(developers, "Id", "Name", Game?.DeveloperId);
    }
}

