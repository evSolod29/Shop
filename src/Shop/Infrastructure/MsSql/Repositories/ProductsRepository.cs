using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;
using Shop.Domain.Repositories;

namespace Shop.Infrastructure.MsSql.Repositories
{
    public class ProductsRepository : EfRepository<Product>, IProductsRepository
    {
        public ProductsRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetByFilter(string? name = null, string? description = null, decimal? priceFrom = null, decimal? priceTo = null, string? commonNote = null, long? categoryId = null, string? additionalNote = null, CancellationToken cancellationToken = default)
        {
            return await BaseInclude(entities).AsNoTracking().Where(x =>
                (string.IsNullOrEmpty(name) || x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(description) || x.Description.Contains(description, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(commonNote) || x.CommonNote.Contains(commonNote, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(additionalNote) || x.AdditionalNote.Contains(additionalNote, StringComparison.OrdinalIgnoreCase)) &&
                (priceFrom == null || x.Price >= priceFrom) &&
                (priceTo == null || x.Price <= priceTo) &&
                (categoryId == null || x.Category.Id == categoryId))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasName(string name, CancellationToken cancellationToken = default)
        {
            return await entities.AnyAsync(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase), cancellationToken);
        }

        protected override IQueryable<Product> BaseInclude(IQueryable<Product> query)
        {
            return query.Include(x => x.Category);
        }

    }
}
