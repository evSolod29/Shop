using Shop.Domain.Models;
using Shop.Domain.Repositories.Base;

namespace Shop.Domain.Repositories
{
    public interface ICategoriesRepository : IRepository<Category>
    {
        Task<bool> HasName(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetByFilter(string? name = null, CancellationToken cancellationToken = default);

    }
}
