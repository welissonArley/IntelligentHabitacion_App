using FluentValidation;
using Homuai.Application.Services.Cryptography;
using Homuai.Application.SharedValidators;
using Homuai.Communication.Request;
using Homuai.Exception;

namespace Homuai.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordValidation : AbstractValidator<RequestChangePasswordJson>
    {
        public ChangePasswordValidation(PasswordEncripter passwordEncripter, Domain.Entity.User userDataNow)
        {
            RuleFor(x => x.CurrentPassword).Must(c => userDataNow.Password.Equals(passwordEncripter.Encrypt(c))).WithMessage(ResourceTextException.CURRENT_PASSWORD_INVALID);
            RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator());
        }
    }
}
