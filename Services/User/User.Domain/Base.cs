namespace User.Domain;

public class Base
{
    public string Id { get; set; } = Guid.CreateVersion7().ToString();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}