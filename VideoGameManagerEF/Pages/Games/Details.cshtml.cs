using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;
using VideoGameManagerEF.Services;

namespace VideoGameManager.Pages.Games;

public class Details : PageModel
{
    private readonly GameService _service;
    public Game? Game { get; set; }
    public Details(GameService service) => _service = service;
    
    public void OnGet(int id)
    {
        Game = _service.GetById(id);
    }

    
}