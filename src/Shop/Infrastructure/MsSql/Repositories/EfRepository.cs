using Microsoft.EntityFrameworkCore;
using Shop.Domain.Models.Base;
using Shop.Domain.Repositories.Base;

namespace Shop.Infrastructure.MsSql.Repositories
{
    public abstract class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext context;
        protected readonly DbSet<T> entities;


        public EfRepository(DbContext context)
        {
            this.context = context;
            entities = context.Set<T>();

        }
        public async Task<T> Create(T entity, CancellationToken cancellationToken = default)
        {
            await context.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task Delete(T entity, CancellationToken cancellationToken = default)
        {
            context.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default)
        {
            return await BaseInclude(entities).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<T?> GetById(long id, CancellationToken cancellationToken = default)
        {
            return await BaseInclude(entities).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> HasId(long id, CancellationToken cancellationToken = default)
        {
            return await entities.AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<T> Update(T entity, CancellationToken cancellationToken = default)
        {
            context.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        protected abstract IQueryable<T> BaseInclude(IQueryable<T> query);
    }
}
