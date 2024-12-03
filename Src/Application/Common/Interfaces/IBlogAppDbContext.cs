using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IBlogAppDbContext
{
    public DbSet<Post> Posts { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}