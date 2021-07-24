using FluentValidation;
using Homuai.Exception;

namespace Homuai.Application.SharedValidators
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage(ResourceTextException.PASSWORD_EMPTY);
            RuleFor(x => x.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceTextException.INVALID_PASSWORD);
        }
    }
}
