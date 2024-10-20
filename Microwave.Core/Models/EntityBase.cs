namespace Microwave.Core.Models;

public abstract class EntityBase
{
    public EntityBase()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; private set; }
}
