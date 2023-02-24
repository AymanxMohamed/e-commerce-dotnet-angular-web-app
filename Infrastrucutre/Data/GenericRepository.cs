using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucutre.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly StoreContext _context;
        public GenericRepository(StoreContext context) =>
            _context = context;

        public async Task<TEntity> GetByIdAsync(int id) =>
            await _context.Set<TEntity>().FindAsync(id);


        public async Task<IReadOnlyList<TEntity>> ListAllAsync() =>
            await _context.Set<TEntity>().ToListAsync();
    }
}