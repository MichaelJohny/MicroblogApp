using MicrblogApp.Persistence;

namespace Application.UnitTests.Common;

public  class CommandTestBase : IDisposable
{
    protected readonly BlogAppDbContext _context;

    public CommandTestBase()
    {
        _context = BlogAppContextFactory.Create();
    }

    public void Dispose()
    {
        BlogAppContextFactory.Destroy(_context);
    }
}