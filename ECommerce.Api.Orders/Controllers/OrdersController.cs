﻿using System.Threading.Tasks;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await ordersProvider.GetOrdersAsync(customerId);

            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }

            return NotFound();
        }
    }
}
