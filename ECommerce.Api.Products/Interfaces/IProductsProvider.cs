using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<ProductModel> products, string ErrorMessage)> GetProductAsync();
        Task<(bool IsSuccess, ProductModel product, string ErrorMessage)> GetProductByIdAsync(int id);
    }
}
