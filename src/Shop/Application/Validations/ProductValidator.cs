using FluentValidation;
using Shared.DTO.DTO.Products;
using Shared.Resources;
using Shop.Domain.Models;

namespace Shop.Application.Validations
{
    public class ProductValidator : AbstractValidator<CreateProduct>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0)
                    .WithName(Strings.Price)
                        .WithMessage(Strings.PropertyMustBeGreaterThanZero);
            RuleFor(x => x.Description)
                .Length(3, 255)
                    .WithName(Strings.Description)
                        .WithMessage(Strings.PropertyMustBeCertainLength);
            RuleFor(x => x.Name)
                .Length(3, 255)
                    .WithName(Strings.Name)
                        .WithMessage(Strings.PropertyMustBeCertainLength);
            RuleFor(x => x.CommonNote)
                .Length(3, 255)
                    .WithName(Strings.CommonNote)
                        .WithMessage(Strings.PropertyMustBeCertainLength);
            RuleFor(x => x.AdditionalNote)
                .Length(3, 255)
                    .WithName(Strings.AdditionalNote)
                        .WithMessage(Strings.PropertyMustBeCertainLength);
            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                    .WithName(Strings.Category)
                        .WithMessage(Strings.PropertyMustBeNotNull);
        }
    }
}
