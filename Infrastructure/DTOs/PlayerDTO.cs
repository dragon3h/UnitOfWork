namespace Infrastructure.DTOs;

public class PlayerDTO
{
    public int Id { get; set; } = 1;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}