using FluentAssertions;
using Homuai.Application.UseCases.Login.ForgotPassword;
using Homuai.Communication.Request;
using Homuai.Exception;
using System;
using Useful.ToTests.Builders.Entity;
using Useful.ToTests.Builders.Repositories;
using Xunit;

namespace Validators.Test.Login.ForgotPassword
{
    public class ForgotPasswordValidationTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            var code = CodeBuilder.Instance().Build(user.Id);

            var validator = CreateValidator(user, code);

            var validationResult = validator.Validate(new RequestResetYourPasswordJson
            {
                Code = code.Value,
                Email = user.Email,
                Password = "@NewPassword123"
            });

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_Invalid_User()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            var code = CodeBuilder.Instance().Build(user.Id);

            var validator = CreateValidator(user, code);

            var validationResult = validator.Validate(new RequestResetYourPasswordJson
            {
                Code = code.Value,
                Email = "bad@email.com",
                Password = "@NewPassword123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.INVALID_USER));
        }

        [Fact]
        public void Validade_Invalid_Code_User()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            var code = CodeBuilder.Instance().Build(user.Id+1);

            var validator = CreateValidator(user, code);

            var validationResult = validator.Validate(new RequestResetYourPasswordJson
            {
                Code = code.Value,
                Email = user.Email,
                Password = "@NewPassword123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CODE_RESET_PASSWORD_REQUIRED));
        }

        [Fact]
        public void Validade_Invalid_Code()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            var code = CodeBuilder.Instance().Build(user.Id);

            var validator = CreateValidator(user, code);

            var validationResult = validator.Validate(new RequestResetYourPasswordJson
            {
                Code = "CODEINVALID",
                Email = user.Email,
                Password = "@NewPassword123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CODE_INVALID));
        }

        [Fact]
        public void Validade_Invalid_Code_Expired()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            var code = CodeBuilder.Instance().Build(user.Id);
            code.CreateDate = DateTime.UtcNow.AddDays(-5);

            var validator = CreateValidator(user, code);

            var validationResult = validator.Validate(new RequestResetYourPasswordJson
            {
                Code = "CODEINVALID",
                Email = user.Email,
                Password = "@NewPassword123"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EXPIRED_CODE));
        }

        [Fact]
        public void Validade_Invalid_Password_Empty()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            var code = CodeBuilder.Instance().Build(user.Id);

            var validator = CreateValidator(user, code);

            var validationResult = validator.Validate(new RequestResetYourPasswordJson
            {
                Code = code.Value,
                Email = user.Email,
                Password = ""
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PASSWORD_EMPTY));
        }

        [Fact]
        public void Validade_Invalid_Password_Length()
        {
            var user = UserBuilder.Instance().WithoutHomeAssociation();
            var code = CodeBuilder.Instance().Build(user.Id);

            var validator = CreateValidator(user, code);

            var validationResult = validator.Validate(new RequestResetYourPasswordJson
            {
                Code = code.Value,
                Email = user.Email,
                Password = "@"
            });

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.INVALID_PASSWORD));
        }

        private ForgotPasswordValidation CreateValidator(Homuai.Domain.Entity.User user, Homuai.Domain.Entity.Code code)
        {
            var userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().GetByEmail(user).Build();
            var codeReadOnlyRepository = CodeReadOnlyRepositoryBuilder.Instance().GetByUserId(code).Build();

            return new ForgotPasswordValidation(codeReadOnlyRepository, userReadOnlyRepository);
        }
    }
}
