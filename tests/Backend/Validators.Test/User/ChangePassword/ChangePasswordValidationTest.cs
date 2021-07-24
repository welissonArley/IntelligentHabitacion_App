using FluentAssertions;
using Homuai.Application.UseCases.User.ChangePassword;
using Homuai.Exception;
using Useful.ToTests.Builders.Encripter;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace Validators.Test.User.ChangePassword
{
    public class ChangePasswordValidationTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var request = RequestChangePassword.Instance().Build();

            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();

            var validator = new ChangePasswordValidation(passwordEncripter, new Homuai.Domain.Entity.User
            {
                Password = passwordEncripter.Encrypt(request.CurrentPassword)
            });
            
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_Current_Password_Invalid()
        {
            var request = RequestChangePassword.Instance().Build();

            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();

            var validator = new ChangePasswordValidation(passwordEncripter, new Homuai.Domain.Entity.User
            {
                Password = passwordEncripter.Encrypt("differentPassword")
            });

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CURRENT_PASSWORD_INVALID));
        }

        [Fact]
        public void Validade_NewCurrent_Password_Empty()
        {
            var request = RequestChangePassword.Instance().Build();
            request.NewPassword = "";

            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();

            var validator = new ChangePasswordValidation(passwordEncripter, new Homuai.Domain.Entity.User
            {
                Password = passwordEncripter.Encrypt(request.CurrentPassword)
            });

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PASSWORD_EMPTY));
        }

        [Fact]
        public void Validade_NewCurrent_Password_Invalid_Length()
        {
            var request = RequestChangePassword.Instance().Build();
            request.NewPassword = "@1";

            var passwordEncripter = PasswordEncripterBuilder.Instance().Build();

            var validator = new ChangePasswordValidation(passwordEncripter, new Homuai.Domain.Entity.User
            {
                Password = passwordEncripter.Encrypt(request.CurrentPassword)
            });

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.INVALID_PASSWORD));
        }
    }
}
