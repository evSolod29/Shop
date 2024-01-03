using FluentValidation;
using Shared.DTO.DTO.Users;
using Shared.Resources;

namespace Auth.Application.Validators;

public class UserValidator : AbstractValidator<CreateUser>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .Length(3, 30)
            .WithName(Strings.UserName)
            .WithMessage(Strings.PropertyMustBeCertainLength);
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage(Strings.EmailNotCorrect);
        RuleSet("Password", () =>
        {
            RuleFor(x => x.Password)
                .Length(6, 30)
                .WithMessage(Strings.PropertyMustBeCertainLength);
        });
    }
}
