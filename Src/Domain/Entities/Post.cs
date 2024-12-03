using Domain.Common;

namespace Domain.Entities;

public class Post : AuditableEntity<Guid> , ISoftDelete
{
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsDeleted { get; set; }
}