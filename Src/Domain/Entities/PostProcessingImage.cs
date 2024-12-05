using Domain.Common;

namespace Domain.Entities;

public class PostProcessingImage : Entity<long>
{
    public Guid PostId { get; set; }
    
    public string Url { get; set; }
    public Post Post { get; set; }
}