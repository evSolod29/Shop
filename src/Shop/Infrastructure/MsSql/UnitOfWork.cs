using System;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Repositories;
using Shop.Domain.Repositories.Base;
using Shop.Infrastructure.MsSql.Repositories;

namespace Shop.Infrastructure.MsSql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        private IProductsRepository? products;
        private ICategoriesRepository? categories;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }


        public IProductsRepository Products => products ??= new ProductsRepository(context);

        public ICategoriesRepository Category => categories ??= new CategoriesRepository(context);

    }
}
