using System;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models;
using Shop.Domain.Repositories;

namespace Shop.Infrastructure.MsSql.Repositories
{
    public class CategoriesRepository : EfRepository<Category>, ICategoriesRepository
    {
        public CategoriesRepository(DbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Category>> GetByFilter(string? name = null, CancellationToken cancellationToken = default)
        {
            return await BaseInclude(entities)
                .Where(x => string.IsNullOrEmpty(name) || x.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasName(string name, CancellationToken cancellationToken = default)
        {
            return await entities.AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken: cancellationToken);
        }

        protected override IQueryable<Category> BaseInclude(IQueryable<Category> query)
        {
            return query;
        }

    }
}
