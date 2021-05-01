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
    public class BrandsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public BrandsController(IProductsService productsService) => _productsService = productsService;

        /// <summary>
        /// /api/brands/1/products
        /// </summary>
        /// <param name="BrandId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Route("{Id}/products")]
        public async Task<IActionResult> GetProductsByBrand(int Id, CancellationToken cancellationToken = default)
        {
            var products = await _productsService.GetProductDtosAsync(Id, cancellationToken);
            return Ok(JsonSerializer.Serialize(products));
        }
    }
}
