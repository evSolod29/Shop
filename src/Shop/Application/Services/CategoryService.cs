using AutoMapper;
using FluentValidation;
using Shared.DTO.DTO.Categories;
using Shared.Resources;
using Shop.Application.Exceptions;
using Shop.Application.Interfaces;
using Shop.Application.Services.Base;
using Shop.Domain.Models;
using Shop.Domain.Repositories.Base;

namespace Shop.Application.Services
{
    public class CategoryService : Service, ICategoryService
    {
        protected readonly IValidator<CreateCategory> validator;
        public CategoryService(IMapper mapper, IUnitOfWork repo, IValidator<CreateCategory> validator) : base(mapper, repo)
        {
            this.validator = validator;
        }

        public async Task<long> Create(CreateCategory createCategory, CancellationToken cancellationToken)
        {
            try
            {
                await validator.ValidateAndThrowAsync(createCategory, cancellationToken);
                if (await repo.Category.HasName(createCategory.Name, cancellationToken))
                    throw new IncorrectParametersException(Strings.CategoryAlreadyExist);
                Category category = mapper.Map<Category>(createCategory);
                category = await repo.Category.Create(category, cancellationToken);
                return category.Id;
            }
            catch (ValidationException ex)
            {
                throw new IncorrectParametersException(ex.Errors.FirstOrDefault()!.ErrorMessage);
            }
        }

        public async Task Delete(long id, CancellationToken cancellationToken)
        {
            Category? category = await repo.Category.GetById(id, cancellationToken)
                ?? throw new NotFoundException(Strings.CategoryNotFound);
            await repo.Category.Delete(category, cancellationToken);
        }

        public async Task<IEnumerable<ViewCategory>> GetCategories(string? name = null, CancellationToken cancellationToken = default)
        {
            return mapper.Map<IEnumerable<ViewCategory>>(await repo.Category.GetByFilter(name, cancellationToken));
        }

        public async Task<ViewCategory> GetCategory(long id, CancellationToken cancellationToken)
        {
            Category? category = await repo.Category.GetById(id, cancellationToken)
                ?? throw new NotFoundException(Strings.CategoryNotFound);
            return mapper.Map<ViewCategory>(category);
        }

        public async Task<long> Update(long id, CreateCategory createCategory, CancellationToken cancellationToken)
        {
            try
            {
                Category? category = await repo.Category.GetById(id, cancellationToken)
                ?? throw new NotFoundException(Strings.CategoryNotFound);
                await validator.ValidateAndThrowAsync(createCategory, cancellationToken);
                if (!category.Name.Equals(createCategory.Name, StringComparison.OrdinalIgnoreCase))
                    if (await repo.Category.HasName(createCategory.Name, cancellationToken))
                        throw new IncorrectParametersException(Strings.CategoryAlreadyExist);
                category = mapper.Map(createCategory, category);
                category = await repo.Category.Update(category, cancellationToken);
                return category.Id;
            }
            catch (ValidationException ex)
            {
                throw new IncorrectParametersException(ex.Errors.FirstOrDefault()!.ErrorMessage);
            }
        }

    }
}
