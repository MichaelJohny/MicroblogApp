using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Common;
using Domain.Common;
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

    public BlogAppDbContext(DbContextOptions options , ICurrentUserService currentUserService,IDateTime dateTime ) : base(options)
    {
        _dateTime = dateTime;
        _currentUserService = currentUserService;
    }


    public DbSet<Post> Posts { get; set; }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.DetectChanges();
        BeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }
    
    private void BeforeSaving()
    {
        ChangeTracker.Entries<IAuditableEntity>().ToList().ForEach(entry =>
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy =
                    _currentUserService?.UserId != null ? _currentUserService.UserId : Guid.NewGuid().ToString();
                entry.Entity.CreatedAt = _dateTime.Now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedBy =
                    _currentUserService?.UserId != null ? _currentUserService.UserId : string.Empty;
                entry.Entity.LastModifiedOn = _dateTime?.Now ?? DateTime.Now;
            }
        });
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogAppDbContext).Assembly);
    }
}

