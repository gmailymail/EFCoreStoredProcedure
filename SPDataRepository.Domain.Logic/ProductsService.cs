using Microsoft.Data.SqlClient;
using SPDataRepository.Data;
using SPDataRepository.Domain.Dtos;
using SPDataRepository.Domain.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SPDataRepository.Domain.Logic
{
    public class ProductsService : IProductsService
    {
        private readonly ProductDbContext _productDbContext;

        public ProductsService(ProductDbContext productDbContext) => _productDbContext = productDbContext;

        public async Task<IEnumerable<ProductDto>> GetProductDtosAsync(CancellationToken cancellationToken = default)
        {
            string spName = "GetProducts";
            var products = await _productDbContext.GetTsFromSpAsync<ProductDto>(spName,token:cancellationToken);
            return products;
        }

        public async Task<IEnumerable<ProductDto>> GetProductDtosAsync(int BrandId, CancellationToken cancellationToken = default)
        {
            string spName = "GetProductsByBrandId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new("BrandId",BrandId)
            };

            var products = await _productDbContext.GetTsFromSpAsync<ProductDto>(
                StoredProcedure: spName, 
                token: cancellationToken,
                parameters: parameters);

            return products;
        }
    }
}
