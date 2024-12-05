using Application.Common.Interfaces;
using Application.Features.Posts.Commands.CreatePost;
using Application.UnitTests.Common;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Application.UnitTests.Commands.CreatePost;

public class CreatePostCommandTests : CommandTestBase
{
    private readonly Mock<IBlogAppDbContext> _mockDbContext;
    private readonly Mock<ICacheService> _mockCacheService;
    private readonly Mock<ICurrentUserService> _mockCurrentUserService;
    private readonly CreatePostCommand _validCommand;
    private readonly Guid _testUserId;
    
    public CreatePostCommandTests()
    {
        _testUserId = Guid.NewGuid();
        _validCommand = CreatePostCommandMockData.GetValidCreatePostCommand();
            
        _mockDbContext = CreatePostCommandMockData.GetMockDbContext();
        _mockCacheService = CreatePostCommandMockData.GetMockCacheService(new List<string> 
        { 
            "https://example.com/cached-image1.jpg", 
            "https://example.com/cached-image2.jpg" 
        });
        _mockCurrentUserService = CreatePostCommandMockData.GetMockCurrentUserService(_testUserId);
    }
    [Fact]
    public async Task Handle_ValidCommand_ShouldCreatePostSuccessfully()
    {
        // Arrange
        var handler = new CreatePostCommandHandler(
            _mockDbContext.Object, 
            _mockCacheService.Object, 
            _mockCurrentUserService.Object);

        // Act
        var result = await handler.Handle(_validCommand, CancellationToken.None);

        // Assert
        
        result.Should().NotBeEmpty();
        _mockDbContext.Verify(x => x.Posts.AddAsync(It.IsAny<Post>(), CancellationToken.None), Times.Once);
        _mockDbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        

    }
    
    [Fact]
    public async Task Handle_WithoutProvidedId_ShouldGenerateNewGuid()
    {
        // Arrange
        var commandWithoutId = new CreatePostCommand
        {
            Content = "Test Post Without ID",
            Image = "https://example.com/test-image.jpg"
        };

        var mockEmptyCacheService = CreatePostCommandMockData.GetMockEmptyCacheService();
        var handler = new CreatePostCommandHandler(
            _mockDbContext.Object, 
            mockEmptyCacheService.Object, 
            _mockCurrentUserService.Object);

        // Act
        var result = await handler.Handle(commandWithoutId, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task Handle_ShouldSetCorrectPostProperties()
    {
        // Arrange
        var handler = new CreatePostCommandHandler(
            _mockDbContext.Object, 
            _mockCacheService.Object, 
            _mockCurrentUserService.Object);

        // Act
        var result = await handler.Handle(_validCommand, CancellationToken.None);

        // Assert
        _mockDbContext.Verify(x => x.Posts.AddAsync(
                It.Is<Post>(p => 
                    p.Content == _validCommand.Content &&
                    p.OriginalImageUrl == _validCommand.Image &&
                    p.UserId == _testUserId.ToString() &&
                    p.Images.Count == 2 &&
                    p.CreatedAt != default &&
                    p.Latitude >= -90 && p.Latitude <= 90 &&
                    p.Longitude >= -180 && p.Longitude <= 180
                ), 
                CancellationToken.None), 
            Times.Once);
    }
}