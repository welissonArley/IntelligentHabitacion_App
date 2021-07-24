using FluentValidation;
using Homuai.Application.SharedValidators;
using Homuai.Application.UseCases.User.SharedValidations;
using Homuai.Communication.Request;
using Homuai.Domain.Repository.User;
using Homuai.Exception;

namespace Homuai.Application.UseCases.User.RegisterUser
{
    public class RegisterUserValidation : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidation(IUserReadOnlyRepository repository)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceTextException.NAME_EMPTY);
            RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceTextException.EMAIL_EMPTY);
            RuleFor(x => x.PushNotificationId).NotEmpty().WithMessage(ResourceTextException.PUSHNOTIFICATION_INVALID);
            RuleFor(x => x.Password).SetValidator(new PasswordValidator());
            RuleFor(x => x.EmergencyContacts).SetValidator(new EmergencyContactsValidation());
            RuleFor(x => x.Phonenumbers).SetValidator(new PhonenumbersValidation());
            When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage(ResourceTextException.EMAIL_INVALID);
                RuleFor(x => x.Email).MustAsync(async (email, cancelation) =>
                {
                    var exists = await repository.ExistActiveUserWithEmail(email);

                    return !exists;

                }).WithMessage(ResourceTextException.EMAIL_ALREADYBEENREGISTERED);
            });
        }
    }
}
