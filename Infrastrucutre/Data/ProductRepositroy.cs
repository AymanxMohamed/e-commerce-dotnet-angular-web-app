using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucutre.Data;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;
    public ProductRepository(StoreContext context) => _context = context;
    public async Task<Product> GetProductByIdAsync(int id) =>
        await _context.Products
        .Include(p => p.ProducType)
        .Include(p => p.ProductBrand)
        .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IReadOnlyList<Product>> GetProductsAsync() =>
        await _context.Products
        .Include(p => p.ProducType)
        .Include(p => p.ProductBrand)
        .ToListAsync();

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync() =>
        await _context.ProductTypes.ToListAsync();

    public async Task<IReadOnlyList<ProductBrand>> GetPruductBrandsAsync() =>
        await _context.ProductBrands.ToListAsync();
}