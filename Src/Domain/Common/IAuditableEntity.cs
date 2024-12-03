namespace Domain.Common;

public interface IAuditableEntity
{
    string CreatedBy { get; set; }

    DateTime CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOn { get; set; }
}