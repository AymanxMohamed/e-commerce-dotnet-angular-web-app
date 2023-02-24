using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucutre.Data;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;
    public ProductRepository(StoreContext context) => _context = context;
    public async Task<Product> GetProductByIdAsync(int id) =>
        await _context.Products.FindAsync(id);

    public async Task<IReadOnlyList<Product>> GetProductsAsync() =>
        await _context.Products.ToListAsync();
}