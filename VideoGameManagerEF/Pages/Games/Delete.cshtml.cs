using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;
using VideoGameManagerEF.Services;

namespace VideoGameManager.Pages.Games;

public class DeleteModel : PageModel
{
    private readonly GameService _service;
    public DeleteModel(GameService service) => _service = service;
    
    [BindProperty]
    public Game? Game { get; set; }

    public void OnGet(int id)
    {
        Game = _service.GetById(id);
    }

    public IActionResult OnPost()
    {
        if (Game != null) _service.Delete(Game.Id);
        return RedirectToPage("/Index");
    }
}