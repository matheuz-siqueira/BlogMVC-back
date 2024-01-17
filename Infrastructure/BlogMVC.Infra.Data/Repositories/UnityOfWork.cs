using BlogMVC.Domain.Interfaces;
using BlogMVC.Infra.Data.Context;

namespace BlogMVC.Infra.Data.Repositories;

public sealed class UnityOfWork : IDisposable, IUnityOfWork
{
    private readonly AppDbContext _context;
    private bool _disposed;
    public UnityOfWork(AppDbContext context)
    {
        _context = context; 
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync(); 
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if(!_disposed && disposing)
        {
            _context.Dispose(); 
        }
        _disposed = true; 
    }
}
