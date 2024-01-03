using System;

namespace Shop.Domain.Repositories.Base
{
    public interface IUnitOfWork
    {
        IProductsRepository Products { get; }
        ICategoriesRepository Category { get; }
    }
}
