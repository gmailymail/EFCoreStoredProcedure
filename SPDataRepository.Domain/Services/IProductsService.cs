using SPDataRepository.Domain.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SPDataRepository.Domain.Services
{
    public interface IProductsService
    {
        /// <summary>
        /// Get the list of products
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductDto>> GetProductDtosAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get the list of products for a Brand
        /// </summary>
        /// <param name="BrandId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<ProductDto>> GetProductDtosAsync(int BrandId,CancellationToken cancellationToken = default);
    }
}
