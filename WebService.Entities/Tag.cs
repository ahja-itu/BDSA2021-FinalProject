using System.ComponentModel.DataAnnotations;

namespace WebService.Entities;


public record Tag
{
    int Id;

    [Required]
    string Name;
}
