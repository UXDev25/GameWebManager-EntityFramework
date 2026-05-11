using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Models;
using VideoGameManagerEF.Data;

namespace VideoGameManager.Pages.Developers;

public class IndexDev : PageModel
{
    private readonly GameStoreContext _context;
    public IndexDev(GameStoreContext context) => _context = context;
    public List<Developer> Developers { get; set; } = default;
    public async Task OnGet()
    {
        Developers = await _context.Developers
            .Include(d => d.Games) 
            .ToListAsync();
    }
}