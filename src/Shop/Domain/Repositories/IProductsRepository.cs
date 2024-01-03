using System.Collections.Generic;
using Shop.Domain.Models;
using Shop.Domain.Repositories.Base;

namespace Shop.Domain.Repositories
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<bool> HasName(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetByFilter(string? name = null,
                                               string? description = null,
                                               decimal? priceFrom = null,
                                               decimal? priceTo = null,
                                               string? commonNote = null,
                                               long? categoryId = null,
                                               string? additionalNote = null,
                                               CancellationToken cancellationToken = default);
    }
}
