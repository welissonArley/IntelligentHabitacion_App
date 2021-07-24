using FluentValidation;
using Homuai.Communication.Request;
using Homuai.Exception;
using System.Collections.Generic;
using System.Linq;

namespace Homuai.Application.UseCases.User.SharedValidations
{
    public class EmergencyContactsValidation : AbstractValidator<List<RequestEmergencyContactJson>>
    {
        public EmergencyContactsValidation()
        {
            RuleFor(x => x).Must(c => c.Count > 0).WithMessage(ResourceTextException.EMERGENCYCONTACT_EMPTY);
            RuleFor(x => x).Must(c => c.Count <= 2).WithMessage(ResourceTextException.EMERGENCYCONTACT_MAX_TWO);
            RuleFor(x => x).Must(c => c.Select(w => w.Phonenumber).Distinct().Count() == c.Count).WithMessage(ResourceTextException.EMERGENCY_CONTACT_SAME_PHONENUMBER);
            RuleForEach(x => x).ChildRules(emergencyContact =>
            {
                emergencyContact.RuleFor(c => c.Name).NotEmpty().WithMessage(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID);
                emergencyContact.RuleFor(c => c.Relationship).NotEmpty().WithMessage(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID);
                emergencyContact.RuleFor(c => c.Phonenumber).NotEmpty().WithMessage(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY);
            });
        }
    }
}
