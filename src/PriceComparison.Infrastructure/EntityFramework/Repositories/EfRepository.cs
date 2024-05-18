using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using PriceComparison.Application.Common.Interfaces;
using PriceComparison.Domain.Common;

namespace PriceComparison.Infrastructure.EntityFramework.Repositories;

public class EfRepository<TEntity, TId>(ApplicationDbContext context) : IRepository<TEntity,TId> where TEntity : Entity<TId>
{
    [UsedImplicitly]
    protected DbSet<TEntity> DbSet { get; } = context.Set<TEntity>();

    public Task AddAsync(TEntity entity)
    {
        DbSet.Add(entity);
        return context.SaveChangesAsync();
    }

    public Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        return context.SaveChangesAsync();
    }

    public Task DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        return context.SaveChangesAsync();
    }

    public Task<TEntity?> GetByIdAsync(TId id)
    {
        return DbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}