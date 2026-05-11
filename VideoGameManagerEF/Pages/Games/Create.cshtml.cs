using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;
using VideoGameManagerEF.Services;

namespace VideoGameManager.Pages.Games;

public class CreateModel : PageModel
{
    private readonly GameService _service;
    public CreateModel(GameService service) => _service = service;
    [BindProperty]
    public Game Game { get; set; }

    public void OnGet()
    {
        Game = new Game();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        _service.Add(Game);
        return RedirectToPage("/Index");
    }
}