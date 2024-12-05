using Domain.Common;

namespace Domain.Entities;

public class Post : AuditableEntity<Guid> , ISoftDelete 
{
    public Post()
    {
        Images = new HashSet<PostProcessingImage>();
    }
    public string Content { get; set; }
    // rename to original image url
    public string OriginalImageUrl { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string UserId { get; set; }
    public bool IsDeleted { get; set; }

    public ApplicationUser User { get; set; }
    public ICollection<PostProcessingImage> Images { get; set; }
}