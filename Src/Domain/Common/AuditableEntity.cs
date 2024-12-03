namespace Domain.Common;

public class AuditableEntity : AuditableEntity<int> { }
public class AuditableEntity<T> : Entity<T>
{
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}