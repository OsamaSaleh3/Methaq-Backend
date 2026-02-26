using Methaq.Application.Common.Interfaces;
using Methaq.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Methaq.Infrastructure.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _transaction!.CommitAsync();
        await _transaction.DisposeAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _transaction!.RollbackAsync();
        await _transaction.DisposeAsync();
    }
}
