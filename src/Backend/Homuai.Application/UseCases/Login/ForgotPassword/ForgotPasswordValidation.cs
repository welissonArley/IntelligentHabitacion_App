using FluentValidation;
using Homuai.Application.SharedValidators;
using Homuai.Communication.Request;
using Homuai.Domain.Repository.Code;
using Homuai.Domain.Repository.User;
using Homuai.Exception;
using System;

namespace Homuai.Application.UseCases.Login.ForgotPassword
{
    public class ForgotPasswordValidation : AbstractValidator<RequestResetYourPasswordJson>
    {
        public ForgotPasswordValidation(ICodeReadOnlyRepository codeRepository, IUserReadOnlyRepository userReadOnlyRepository)
        {
            RuleFor(x => x).CustomAsync(async (request, context, canelation) =>
            {
                var user = await userReadOnlyRepository.GetByEmail(request.Email);

                if (user == null)
                    context.AddFailure(ResourceTextException.INVALID_USER);
                else
                {
                    var code = await codeRepository.GetByUserId(user.Id);
                    if (code == null)
                        context.AddFailure(ResourceTextException.CODE_RESET_PASSWORD_REQUIRED);
                    else
                        ValidateCode(code, request.Code, context);
                }
            });
            RuleFor(x => x.Password).SetValidator(new PasswordValidator());
        }

        private void ValidateCode(Domain.Entity.Code code, string codeReceived, ValidationContext<RequestResetYourPasswordJson> context)
        {
            if (!code.Value.Equals(codeReceived.ToUpper()))
                context.AddFailure(ResourceTextException.CODE_INVALID);

            if (DateTime.Compare(code.CreateDate.AddHours(1), DateTime.UtcNow) <= 0)
                context.AddFailure(ResourceTextException.EXPIRED_CODE);
        }
    }
}
