using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;
using VideoGameManagerEF.Services;

namespace VideoGameManager.Games;

public class IndexModel : PageModel
{
    private readonly GameService _service;
    public IndexModel(GameService service) => _service = service;
 
    public List<Game> Games { get; set; } = new();
 
    public void OnGet() => Games = _service.GetAll();
}
