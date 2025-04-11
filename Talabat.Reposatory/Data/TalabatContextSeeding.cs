  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Reposatory.Data
{
    public class TalabatContextSeeding
    {
        public static async Task SeedAsync(TalbatDbContext talbatDbContext)
        {
            if (!talbatDbContext.ProductBrands.Any())
            {
                var ProductBrand = File.ReadAllText("../Talabat.Reposatory/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrand);
                foreach (var brand in brands)
                {
                    await talbatDbContext.Set<ProductBrand>().AddAsync(brand);
                }
                await talbatDbContext.SaveChangesAsync();
            }

            if (!talbatDbContext.ProductTypes.Any())
            {
                var ProductType = File.ReadAllText("../Talabat.Reposatory/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(ProductType);
                foreach (var type in Types)
                {
                    await talbatDbContext.Set<ProductType>().AddAsync(type);
                }
                await talbatDbContext.SaveChangesAsync();
            }
            if (!talbatDbContext.Products.Any())
            {
                var ProductsData = File.ReadAllText("../Talabat.Reposatory/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                foreach (var product in products)
                {
                    await talbatDbContext.Set<Product>().AddAsync(product);
                }
                await talbatDbContext.SaveChangesAsync();
            }
        }
    }
}
