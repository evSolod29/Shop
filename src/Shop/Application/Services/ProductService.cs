using AutoMapper;
using FluentValidation;
using Shared.Resources;
using Shared.DTO.DTO.Products;
using Shop.Application.Interfaces;
using Shop.Application.Services.Base;
using Shop.Domain.Models;
using Shop.Domain.Repositories.Base;
using Shop.Application.Exceptions;

namespace Shop.Application.Services
{
    public class ProductService : Service, IProductService
    {
        private readonly IValidator<CreateProduct> validator;
        public ProductService(IMapper mapper, IUnitOfWork repo, IValidator<CreateProduct> validator) : base(mapper, repo)
        {
            this.validator = validator;

        }

        public async Task<long> Create(CreateProduct createProduct)
        {
            try
            {
                await validator.ValidateAndThrowAsync(createProduct);
                Product product = mapper.Map<Product>(createProduct);
                Category? category = await repo.Category.GetById(createProduct.CategoryId)
                    ?? throw new IncorrectParametersException(Strings.CategoryNotFound);
                product.Category = category;
                product = await repo.Products.Create(product);
                return product.Id;
            }
            catch (ValidationException ex)
            {
                throw new IncorrectParametersException(ex.Errors.FirstOrDefault()!.ErrorMessage);
            }
        }

        public async Task Delete(long id)
        {
            Product? product = await repo.Products.GetById(id)
                ?? throw new NotFoundException(Strings.ProductNotFound);
            await repo.Products.Delete(product);
        }


        public async Task<ViewProductFull> GetFullProduct(long id)
        {
            Product? product = await repo.Products.GetById(id)
                ?? throw new NotFoundException(Strings.ProductNotFound);
            return mapper.Map<ViewProductFull>(product);
        }

        public async Task<IEnumerable<ViewProductFull>> GetFullProducts(string? name = null,
                                                                        string? description = null,
                                                                        decimal? priceFrom = null,
                                                                        decimal? priceTo = null,
                                                                        string? commonNote = null,
                                                                        long? categoryId = null,
                                                                        string? additionalNote = null)
        {

            return
                mapper.Map<IEnumerable<ViewProductFull>>(
                    await repo.Products.GetByFilter(name, description, priceFrom, priceTo, commonNote, categoryId, additionalNote));
        }

        public async Task<ViewProduct> GetProduct(long id)
        {
            Product? product = await repo.Products.GetById(id)
                ?? throw new NotFoundException(Strings.ProductNotFound);
            return mapper.Map<ViewProduct>(product);
        }

        public async Task<IEnumerable<ViewProduct>> GetProducts(string? name = null,
                                                                string? description = null,
                                                                decimal? priceFrom = null,
                                                                decimal? priceTo = null,
                                                                string? commonNote = null,
                                                                long? categoryId = null)
        {
            return mapper.Map<IEnumerable<ViewProduct>>(
                    await repo.Products.GetByFilter(name, description, priceFrom, priceTo, commonNote, categoryId));
        }

        public async Task<long> Update(long id, CreateProduct createProduct)
        {
            try
            {
                await validator.ValidateAndThrowAsync(createProduct);
                Product? product = await repo.Products.GetById(id)
                    ?? throw new NotFoundException(Strings.ProductNotFound);
                product = mapper.Map(createProduct, product);
                Category? category = await repo.Category.GetById(createProduct.CategoryId)
                    ?? throw new IncorrectParametersException(Strings.CategoryNotFound);
                product.Category = category;
                product = await repo.Products.Update(product);
                return product.Id;
            }
            catch (ValidationException ex)
            {
                throw new IncorrectParametersException(ex.Errors.FirstOrDefault()!.ErrorMessage);
            }
        }
    }
}
