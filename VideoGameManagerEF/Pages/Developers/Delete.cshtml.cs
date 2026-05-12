using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Models.Enums;
using VideoGameManager.Services;
using VideoGameManagerEF.Data;
using VideoGameManagerEF.Services;

namespace VideoGameManagerEF.Pages.Games;

public class DeleteModelDev : PageModel
{
    private readonly GameStoreContext _context;
    public DeleteModelDev(GameStoreContext context) => _context = context;
    
    [BindProperty]
    public Developer? Developer { get; set; }
    public List<Game>? Games { get; set; }

    public void OnGet(int id)
    {
        Developer = _context.FindAsync<Developer>(id).Result;
        Games = _context.Games.ToList();
    }

    public IActionResult OnPost()
    {
        if (Developer == null) return Page();
        if (_context.Games.Any(g => g.DeveloperId == Developer.Id))
        {
            ModelState.AddModelError(string.Empty, "Operation cancelled: another Games Depend on this developer");
            return Page();
        }
        _context.Remove(Developer);
        _context.SaveChanges();
        return RedirectToPage("/Developers/Index");
    }
}