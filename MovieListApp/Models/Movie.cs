using System.ComponentModel.DataAnnotations;

namespace MovieListApp.Models;

public class Movie
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MinLength(1)]
    [MaxLength(200)]
    public string Name { get; set; }
    [Range(1900, 2100, ErrorMessage = "Production year must be between 1900 and 2100.")]
    public int? ProductionYear { get; set; }
}