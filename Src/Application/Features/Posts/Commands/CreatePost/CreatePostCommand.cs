using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<Guid>
{
    public string Id { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly IBlogAppDbContext _dbContext;
    private readonly ICacheService _cache;
    private readonly ICurrentUserService _currentUserService;
    public CreatePostCommandHandler(IBlogAppDbContext dbContext, ICacheService cache, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _cache = cache;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var images = _cache.GetFromCache(request.Id);
        var post = new Post
        {
            Id = string.IsNullOrEmpty(request.Id) ? Guid.NewGuid() :Guid.Parse(request.Id),
            Content = request.Content,
            OriginalImageUrl = request.Image,
            CreatedAt = DateTime.UtcNow,
            Latitude = new Random().NextDouble() * 180 - 90, // Random latitude
            Longitude = new Random().NextDouble() * 360 - 180, // Random longitude,
            UserId = _currentUserService.UserId,
            Images =
                images.Select(i => new PostProcessingImage()
                {
                    PostId =Guid.Parse(request.Id),
                    Url = i
                }).ToList()
        };

        await _dbContext.Posts.AddAsync(post, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return post.Id;
    }
}
