using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;
using VideoGameManagerEF.Data;
using VideoGameManagerEF.Services;

namespace VideoGameManagerEF.Pages.Games;

public class Details : PageModel
{
    private readonly GameStoreContext _context;
    public Details(GameStoreContext context) => _context = context;
    public Game? Game { get; set; }
    
    public void OnGet(int id)
    {
        Game = _context.FindAsync<Game>(id).Result;
    }
}