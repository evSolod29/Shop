using AutoMapper;
using FluentValidation;
using Shop.Application.Interfaces.Base;
using Shop.Domain.Repositories.Base;

namespace Shop.Application.Services.Base
{
    public class Service : IService
    {
        protected readonly IMapper mapper;
        protected readonly IUnitOfWork repo;

        public Service(IMapper mapper, IUnitOfWork repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }
    }
}
