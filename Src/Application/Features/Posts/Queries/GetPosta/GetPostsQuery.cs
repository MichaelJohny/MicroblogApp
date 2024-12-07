using Application.Common.Interfaces;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Posts.Queries.GetPosta;

public record GetPostsQuery : IRequest<IEnumerable<GetPostsOutputDto>>;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, IEnumerable<GetPostsOutputDto>>
{
    private readonly IBlogAppDbContext _dbContext;
    private readonly IImageProcessingService _imageProcessingService;

    public GetPostsQueryHandler(IBlogAppDbContext dbContext, IImageProcessingService imageProcessingService)
    {
        _dbContext = dbContext;
        _imageProcessingService = imageProcessingService;
    }

    public async Task<IEnumerable<GetPostsOutputDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _dbContext.Posts.Include(e => e.Images)
            .Include(e => e.User)
            .AsNoTracking()
            .Select(e => new PostDto()
            {
                Id = e.Id,
                Content = e.Content,
                OriginalImage = e.OriginalImageUrl,
                Images = e.Images.Select(s => s.Url).ToList(),
                UserName = e.User.UserName,
                CreatedAt = e.CreatedAt
            }).OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);

        var imagesSize = Constants.GetRandomImageSize();
        var result = posts.Select(p =>
            new GetPostsOutputDto(p.Id, p.Content,
                _imageProcessingService.SelectBestImageUrl(p.Images, imagesSize[0], imagesSize[1]),
                p.UserName, p.CreatedAt));
        return result;
    }
}

public record GetPostsOutputDto(Guid Id, string Content, string ImageUrl, string UserName, DateTime createdAt);

public class PostDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string OriginalImage { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<string> Images { get; set; }
}