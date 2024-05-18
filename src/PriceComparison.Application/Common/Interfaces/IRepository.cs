using PriceComparison.Domain.Common;

namespace PriceComparison.Application.Common.Interfaces;

public interface IRepository<TEntity, in TId>
where TEntity : Entity<TId>
{
    Task AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);

    Task<TEntity?> GetByIdAsync(TId id);
}