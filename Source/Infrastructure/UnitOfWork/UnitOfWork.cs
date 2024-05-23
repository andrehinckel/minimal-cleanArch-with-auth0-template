using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork(DbContext databaseContext) : IUnitOfWork
{
    private IDbContextTransaction _transaction = null!;

    public async Task BeginTransactionAsync()
    {
        _transaction = await databaseContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await databaseContext.SaveChangesAsync();
        await _transaction.CommitAsync();
    }

    public async Task Rollback()
    {
        await databaseContext.DisposeAsync();
        await _transaction.RollbackAsync();
    }
}