using System;
using Shop.Domain.Models.Base;

namespace Shop.Domain.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<T?> GetById(long id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);
        Task<T> Create(T entity, CancellationToken cancellationToken = default);
        Task<T> Update(T entity, CancellationToken cancellationToken = default);
        Task Delete(T entity, CancellationToken cancellationToken = default);
        Task<bool> HasId(long id, CancellationToken cancellationToken = default);
    }
}
