using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManagerEF.Data;

namespace VideoGameManager.Pages.Developers;

public class DetailsDev : PageModel
{
    private readonly GameStoreContext _context;
    public DetailsDev(GameStoreContext context) => _context = context;
    
    public void OnGet(int id)
    {
        
    }
}