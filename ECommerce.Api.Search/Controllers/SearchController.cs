using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTermModel term)
        {
            var result = await _searchService.SearchAsync(term.CustomerId);

            if (result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }

            return NotFound();
        }
    }
}
