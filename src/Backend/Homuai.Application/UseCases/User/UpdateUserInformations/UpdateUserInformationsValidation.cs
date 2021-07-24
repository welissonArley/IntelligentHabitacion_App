using FluentValidation;
using Homuai.Application.UseCases.User.SharedValidations;
using Homuai.Communication.Request;
using Homuai.Exception;

namespace Homuai.Application.UseCases.User.UpdateUserInformations
{
    public class UpdateUserInformationsValidation : AbstractValidator<RequestUpdateUserJson>
    {
        public UpdateUserInformationsValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceTextException.NAME_EMPTY);
            RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceTextException.EMAIL_EMPTY);
            RuleFor(x => x.EmergencyContacts).SetValidator(new EmergencyContactsValidation());
            RuleFor(x => x.Phonenumbers).SetValidator(new PhonenumbersValidation());
        }
    }
}
