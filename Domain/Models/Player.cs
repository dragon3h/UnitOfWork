using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Player : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(30)]
    public string Password { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}