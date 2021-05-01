using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPDataRepository.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SPDataRepository.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService) => _productsService = productsService;

        /// <summary>
        /// /api/products
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
        {
            var products = await _productsService.GetProductDtosAsync(cancellationToken);
            return Ok(JsonSerializer.Serialize(products));
        }
    }
}
