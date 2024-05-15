namespace Domain.Models;

public class BaseEntity : IEntity
{
    public int Id { get; set; } = default;
    public DateTime CreatedAt { get; set; } = default;
    public DateTime? UpdatedAt { get; set; } = default;
    public string CreatedBy { get; set; } = default!;
    public string? UpdatedBy { get; set; } = default;
}
