using Shared.DTO.DTO.Categories;

namespace Shop.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<long> Create(CreateCategory createCategory, CancellationToken cancellationToken = default);
        Task<long> Update(long id, CreateCategory createCategory, CancellationToken cancellationToken = default);
        Task Delete(long id, CancellationToken cancellationToken = default);
        Task<ViewCategory> GetCategory(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ViewCategory>> GetCategories(string? name = null, CancellationToken cancellationToken = default);
    }
}
