using Core.Entites;
using Repository.Dtata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext DbContext)
        {

            if (!DbContext.Brands.Any())
            {
                var BrandsData = File.ReadAllText("../Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (brands?.Count > 0)
                {
                    foreach (var Brand in brands)
                    {
                        await DbContext.Set<ProductBrand>().AddAsync(Brand);
                    }
                        await DbContext.SaveChangesAsync();
                }
            }

             if(!DbContext.Types.Any())
            {
                var TypesData = File.ReadAllText("../Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (types?.Count > 0)
                {
                    foreach (var Type in types)
                    {
                        await DbContext.Set<ProductType>().AddAsync(Type);
                    }
                    await DbContext.SaveChangesAsync();
                }
            }

           if (!DbContext.Products.Any())
            {
                var ProductsData = File.ReadAllText("../Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await DbContext.Set<Product>().AddAsync(product);
                    }
                        await DbContext.SaveChangesAsync();
                }
            }
        }
    }
}
