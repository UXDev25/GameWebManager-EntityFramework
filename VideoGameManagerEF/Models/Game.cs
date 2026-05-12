using System.ComponentModel.DataAnnotations;
using VideoGameManager.Models.Enums;

namespace VideoGameManager.Models;

public class Game
{
    [Key]
    public int Id { get; set; }
 
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
 
    [Required(ErrorMessage = "Genre is required")]
    public string Genre { get; set; } = string.Empty;
 
    [Range(1970, 2030, ErrorMessage = "Invalid year")]
    public int Year { get; set; }
 
    [Range(0, 10, ErrorMessage = "Score must be between 0 and 10")]
    public double Score { get; set; }
 
    public string Description { get; set; } = string.Empty;
    
    //Developer values
    public int DeveloperId { get; set; }
    
    public Developer? Developer { get; set; }
    public string? Platform { get; set; }
    
    
    
    //Methods
    public string ToStringAlt(ECrud action) => $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {action} - {Title}";
    
    public override bool Equals(object obj)
    {
        return obj is Game game &&
               Id == game.Id &&
               Title == game.Title;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Title);
    }
}
