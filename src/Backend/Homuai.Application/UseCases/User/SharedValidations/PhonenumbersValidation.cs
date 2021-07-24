using FluentValidation;
using Homuai.Exception;
using System.Collections.Generic;
using System.Linq;

namespace Homuai.Application.UseCases.User.SharedValidations
{
    public class PhonenumbersValidation : AbstractValidator<List<string>>
    {
        public PhonenumbersValidation()
        {
            RuleFor(x => x).Must(c => c.Count > 0).WithMessage(ResourceTextException.PHONENUMBER_EMPTY);
            RuleFor(x => x).Must(c => c.Count <= 2).WithMessage(ResourceTextException.PHONENUMBER_MAX_TWO);
            RuleFor(x => x).Must(c => c.Count == c.Distinct().Count()).WithMessage(ResourceTextException.PHONENUMBERS_ARE_SAME);
        }
    }
}
