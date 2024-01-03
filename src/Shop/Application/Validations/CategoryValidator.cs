using FluentValidation;
using Shared.DTO.DTO.Categories;
using Shared.Resources;

namespace Shop.Application.Validations
{
    public class CategoryValidator : AbstractValidator<CreateCategory>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 255)
                    .WithName(Strings.Name)
                        .WithMessage(Strings.PropertyMustBeCertainLength);
        }
    }
}
