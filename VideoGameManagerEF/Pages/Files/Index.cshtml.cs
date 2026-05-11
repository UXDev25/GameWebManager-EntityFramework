using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameManager.Models;
using VideoGameManager.Services;
using VideoGameManagerEF.Services;

namespace VideoGameManager.Pages.Files;

public class IndexSave : PageModel
{
    private readonly GameService _service;
    private readonly IWebHostEnvironment _env;

    public IndexSave(IWebHostEnvironment env, GameService service)
    {
        _env = env;
        _service = service;
    }
    public List<string>? GamesLog { get; set; }
    
    [BindProperty]
    public IFormFile? GamesFile { get; set; }
    
    public void OnGet()
    {
        GamesLog = FileManager.Read(linia => linia);
    }

    //JSON
    public IActionResult OnPostImport()
    {
        if (GamesFile == null || GamesFile.Length == 0)
        {
            ModelState.AddModelError(string.Empty, "Invalid file");
            return Page();
        }
        GameRepository.LoadAll(GamesFile);
        _service.GetAll();
        return Page();
    }
    
    public IActionResult OnPostExport()
    {
        string filePath = Path.Combine(_env.WebRootPath, "data", "games.json");
        if (!System.IO.File.Exists(filePath))
        {
            ModelState.AddModelError(string.Empty, "There are no games registered");
            return Page();
        }
        return PhysicalFile(filePath, "application/json", "games.json");
    }

    //CSV
    public IActionResult OnPostCsvExport()
    {
        string filePath = Path.Combine(_env.WebRootPath, "data", "games.csv");
        if (!System.IO.File.Exists(filePath))
        {
            ModelState.AddModelError(string.Empty, "There are no games registered");
            return Page();
        }
        return PhysicalFile(filePath, "application/csv", "games.csv");
    }
    
    //XML
    public IActionResult OnPostXmlExport()
    {
        string filePath = Path.Combine(_env.WebRootPath, "data", "games_ranking.xml");
        if (!System.IO.File.Exists(filePath))
        {
            ModelState.AddModelError(string.Empty, "There are no games registered");
            return Page();
        }
        return PhysicalFile(filePath, "application/xml", "games_ranking.xml");
    }
}