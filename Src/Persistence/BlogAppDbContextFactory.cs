using Application.Common.Interfaces;
using Common;
using Microsoft.EntityFrameworkCore;

namespace MicrblogApp.Persistence;

public class BlogAppDbContextFactory : DesignTimeDbContextFactoryBase<BlogAppDbContext>
{
    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _currentUserService;

    public BlogAppDbContextFactory(IDateTime dateTime, ICurrentUserService currentUserService)
    {
        _dateTime = dateTime;
        _currentUserService = currentUserService;
    }

    public BlogAppDbContextFactory()
    {
        
    }
    
    protected override BlogAppDbContext CreateNewInstance(DbContextOptions<BlogAppDbContext> options)
    {
        return new BlogAppDbContext(options , _currentUserService, _dateTime);
    }
}