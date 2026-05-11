using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;
using VideoGameManagerEF.Services;

namespace VideoGameManager.Pages.Games;

public class EditModel: PageModel
{
    private readonly GameService _service;
    public EditModel(GameService service) => _service = service;
    public List<Game> Games { get; set; } = [];
    
    [BindProperty]
    public Game? Game { get; set; }

    public void OnGet(int id)
    {
        Games = _service.GetAll();
        Game = _service.GetById(id);
    }

    public IActionResult OnPost()
    {
        Games = _service.GetAll();
        if (!ModelState.IsValid)
        {
            return Page();
        }
        if (Game?.Title == Games.Find(game => game.Title == Game?.Title)?.Title && Game?.Id != Games.Find(game => game.Title == Game?.Title)?.Id)
        {
            ModelState.AddModelError(string.Empty, "This game is already on the collection.");
            return Page();
        }
        
        Console.WriteLine("Game id at post: " + Game?.Id);
        if (Game != null) _service.Update(Game);
        return RedirectToPage("/Index");
    }
}

