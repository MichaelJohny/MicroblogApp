using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Posts.Commands;

public class CreatePostCommand : IRequest<Guid>
{
    public string Content { get; set; }
    //public IFormFile Image { get;  }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly IBlogAppDbContext _dbContext;

    public CreatePostCommandHandler(IBlogAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            CreatedAt = DateTime.UtcNow,
            Latitude = new Random().NextDouble() * 180 - 90, // Random latitude
            Longitude = new Random().NextDouble() * 360 - 180, // Random longitude
        };

        await _dbContext.Posts.AddAsync(post, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return post.Id;
    }
}
