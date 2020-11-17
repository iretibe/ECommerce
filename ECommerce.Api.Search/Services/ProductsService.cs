using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrdersService> _logger;

        public ProductsService(IHttpClientFactory httpClientFactory, ILogger<OrdersService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, IEnumerable<ProductModel> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProductsService");
                var response = await client.GetAsync("api/products");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(content, options);
                    return (true, result, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
