using Domain.Entities;
using MicrblogApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.UnitTests.Common;

public class BlogAppContextFactory
{
    public static BlogAppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<BlogAppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new BlogAppDbContext(options);

        context.Database.EnsureCreated();

        // context.Posts.AddRange(new[] {
        //     new Post { Id = Guid.Parse("f69c90c4-5141-4b32-9e15-974af60dc9b6"), Content = "Post 1" , UserId = "9bbc1228-6aef-45fd-ab15-19c7fd5fcfc6" },
        //     new Post { Id = Guid.Parse("3e56092c-a36b-4f85-a4e1-497185d2c89f"), Content = "Post 2" , UserId = "f3c7c9cf-e5e5-4eb0-ae86-e6f5534d8aa4" },
        //     new Post { Id = Guid.Parse("8feceb11-2fee-4e79-bcb6-6fd7bbaf7f1a"), Content = "Post 3" , UserId = "9bbc1228-6aef-45fd-ab15-19c7fd5fcfc6" },
        //     
        // });
        //
        //
        // context.SaveChanges();

        return context;
    }

    public static void Destroy(BlogAppDbContext context)
    {
        context.Database.EnsureDeleted();

        context.Dispose();
    }
}