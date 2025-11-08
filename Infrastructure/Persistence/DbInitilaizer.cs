using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitilaizer : IDbInitializer
    {
        private readonly StoreDbContext context;

        public DbInitilaizer(StoreDbContext _context)
        {
            context = _context;
        }
        public async Task InitializeAsync()
        {
            if(context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
            }
            if (!context.ProductTypes.Any())
            {
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if(types is not null && types.Any())
                {
                    await context.ProductTypes.AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.ProductBrands.Any())
            {
                var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if(brands is not null && brands.Any())
                {
                    await context.ProductBrands.AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\products.json");
                var product = JsonSerializer.Deserialize<List<Product>>(productsData);
                if(product is not null && product.Any())
                {
                    await context.Products.AddRangeAsync(product);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
