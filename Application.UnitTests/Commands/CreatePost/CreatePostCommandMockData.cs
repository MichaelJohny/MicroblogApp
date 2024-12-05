using Application.Common.Interfaces;
using Application.Features.Posts.Commands.CreatePost;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Application.UnitTests.Commands.CreatePost;

public static class CreatePostCommandMockData
{
    public static Mock<IBlogAppDbContext> GetMockDbContext()
    {
        var mockDbContext = new Mock<IBlogAppDbContext>();
        var mockPostSet = new Mock<DbSet<Post>>();

        mockDbContext.Setup(x => x.Posts).Returns(mockPostSet.Object);
        mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        return mockDbContext;
    }
    public static Mock<ICacheService> GetMockCacheService(List<string> cachedImages)
    {
        var mockCacheService = new Mock<ICacheService>();
        mockCacheService
            .Setup(x => x.GetFromCache(It.IsAny<string>()))
            .Returns(cachedImages);

        return mockCacheService;
    }
    public static Mock<ICacheService> GetMockEmptyCacheService()
    {
        var mockCacheService = new Mock<ICacheService>();
        mockCacheService
            .Setup(x => x.GetFromCache(It.IsAny<string>()))
            .Returns(Enumerable.Empty<string>());

        return mockCacheService;
    }
    public static Mock<ICurrentUserService> GetMockCurrentUserService(Guid userId)
    {
        var mockCurrentUserService = new Mock<ICurrentUserService>();
        mockCurrentUserService
            .Setup(x => x.UserId)
            .Returns(userId.ToString);

        return mockCurrentUserService;
    }
    public static CreatePostCommand GetValidCreatePostCommand()
    {
        return new CreatePostCommand
        {
            Id = Guid.NewGuid().ToString(),
            Content = "Test Post Content",
            Image = "https://example.com/test-image.jpg"
        };
    }
}