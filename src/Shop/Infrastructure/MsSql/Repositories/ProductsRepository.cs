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
                (string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name.ToLower())) &&
                (string.IsNullOrEmpty(description) || x.Description.ToLower().Contains(description.ToLower())) &&
                (string.IsNullOrEmpty(commonNote) || x.CommonNote.ToLower().Contains(commonNote.ToLower())) &&
                (string.IsNullOrEmpty(additionalNote) || x.AdditionalNote.ToLower().Contains(additionalNote.ToLower())) &&
                (priceFrom == null || x.Price >= priceFrom) &&
                (priceTo == null || x.Price <= priceTo) &&
                (categoryId == null || x.Category.Id == categoryId))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasName(string name, CancellationToken cancellationToken = default)
        {
            return await entities.AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
        }

        protected override IQueryable<Product> BaseInclude(IQueryable<Product> query)
        {
            return query.Include(x => x.Category);
        }

    }
}
