using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }        

        [HttpGet]
        public async Task<IActionResult> GetProductAsync()
        {
            var result = await productsProvider.GetProductAsync();

            if (result.IsSuccess)
            {
                return Ok(result.products);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await productsProvider.GetProductByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.product);
            }

            return NotFound();
        }
    }
}
