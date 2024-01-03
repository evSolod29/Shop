using Shared.DTO.DTO.Products;
using Shop.Application.Interfaces.Base;

namespace Shop.Application.Interfaces
{
    public interface IProductService : IService
    {
        Task<long> Create(CreateProduct createProduct);
        Task<long> Update(long id, CreateProduct createProduct);
        Task Delete(long id);
        Task<ViewProduct> GetProduct(long id);
        Task<ViewProductFull> GetFullProduct(long id);
        Task<IEnumerable<ViewProduct>> GetProducts(string? name = null,
                                                                    string? description = null,
                                                                    decimal? priceFrom = null,
                                                                    decimal? priceTo = null,
                                                                    string? commonNote = null,
                                                                    long? categoryId = null);
        Task<IEnumerable<ViewProductFull>> GetFullProducts(string? name = null,
                                                                            string? description = null,
                                                                            decimal? priceFrom = null,
                                                                            decimal? priceTo = null,
                                                                            string? commonNote = null,
                                                                            long? categoryId = null,
                                                                            string? additionalNote = null);
    }
}
