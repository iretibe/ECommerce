using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductDbContext dbContext, 
            ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
#pragma warning disable EF1001 // Internal EF Core API usage.
            if (!dbContext.Products.Any())
#pragma warning restore EF1001 // Internal EF Core API usage.
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 5, Inventory = 200 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 150, Inventory = 1000 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 200, Inventory = 2000 });
                dbContext.SaveChanges();
            }
        }

        public ILogger<ProductsProvider> Logger { get; }
        public IMapper Mapper { get; }

        public async Task<(bool IsSuccess, IEnumerable<ProductModel> products, string ErrorMessage)> GetProductAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();

#pragma warning disable EF1001 // Internal EF Core API usage.
                if (products != null && products.Any())
#pragma warning restore EF1001 // Internal EF Core API usage.
                {
                    var result = mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);

                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, ProductModel product, string ErrorMessage)> GetProductByIdAsync(int id)
        {
            try
            {
                var prod = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (prod != null)
                {
                    var result = mapper.Map<Product, ProductModel>(prod);

                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());

                return (false, null, ex.Message);
            }
        }
    }
}
