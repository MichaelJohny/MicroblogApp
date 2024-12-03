using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Common;
using Domain.Entities;

namespace  MicrblogApp.Persistence;

public class BlogAppDbContext : DbContext , IBlogAppDbContext
{
    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _currentUserService;

    public BlogAppDbContext()
    {
     
    }

    public BlogAppDbContext(DbContextOptions options) : base(options)
    {
     
    }

    public BlogAppDbContext(DbContextOptions options , ICurrentUserService currentUserService,IDateTime dateTime )
    {
        _dateTime = dateTime;
        _currentUserService = currentUserService;
    }


    public DbSet<Post> Posts { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.DetectChanges();
        return base.SaveChangesAsync(cancellationToken);
    }
}

