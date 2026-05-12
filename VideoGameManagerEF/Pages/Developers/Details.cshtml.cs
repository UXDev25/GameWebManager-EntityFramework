using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Models;
using VideoGameManagerEF.Data;

namespace VideoGameManager.Pages.Developers;

public class DetailsDev : PageModel
{
    private readonly GameStoreContext _context;
    public DetailsDev(GameStoreContext context) => _context = context;
    
    public Developer Developer { get; set; }
    
    public async Task OnGet(int id)
    {
        Developer = await _context.Developers
            .Include(d => d.Games)
            .FirstOrDefaultAsync(d => d.Id == id);

    }
}