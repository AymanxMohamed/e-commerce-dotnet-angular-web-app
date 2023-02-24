using Core.Entities;

namespace Core.Interfaces;
public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity> GetByIdAsync(int id);
    Task<IReadOnlyList<TEntity>> ListAllAsync();
}