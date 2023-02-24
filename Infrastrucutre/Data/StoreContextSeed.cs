using System.Text.Json;
using Core.Entities;

namespace Infrastrucutre.Data;
public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.ProductBrands.Any())
        {
            // . -> the current directory is the API folder which is the startup project
            // 
            var brandsData = File.ReadAllText("../Infrastrucutre/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            context.ProductBrands.AddRange(brands);
        }
        if (!context.ProductTypes.Any())
        {
            var typesData = File.ReadAllText("../Infrastrucutre/SeedData/types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            context.ProductTypes.AddRange(types);
        }
        if (!context.Products.Any())
        {
            var productData = File.ReadAllText("../Infrastrucutre/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productData);
            context.Products.AddRange(products);
        }
        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();

    }
}