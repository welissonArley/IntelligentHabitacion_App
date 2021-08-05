using FluentAssertions;
using Homuai.Application.UseCases.User.RegisterUser;
using Homuai.Domain.Repository.User;
using Homuai.Exception;
using System.Linq;
using System.Threading.Tasks;
using Useful.ToTests.Builders.Repositories;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace Validators.Test.User.RegisterUser
{
    public class RegisterUserValidationTest
    {
        [Fact]
        public async Task Validade_Sucess()
        {
            var user = RequestRegisterUser.Instance().Build();

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validade_NameEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.Name = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.NAME_EMPTY));
        }

        [Fact]
        public async Task Validade_EmailEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.Email = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMAIL_EMPTY));
        }

        [Fact]
        public async Task Validade_PushNotificationIdEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.PushNotificationId = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PUSHNOTIFICATION_INVALID));
        }

        [Fact]
        public async Task Validade_PasswordEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.Password = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PASSWORD_EMPTY));
        }

        [Fact]
        public async Task Validade_PasswordInvalid()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.Password = "@";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.INVALID_PASSWORD));
        }

        [Fact]
        public async Task Validade_EmergencyContactEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.EmergencyContacts.Clear();

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMERGENCYCONTACT_EMPTY));
        }

        [Fact]
        public async Task Validade_MoreThan2EmergencyContact()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.EmergencyContacts.Add(RequestEmergencyContact.Instance().Build());
            user.EmergencyContacts.Add(RequestEmergencyContact.Instance().Build());
            user.EmergencyContacts.Add(RequestEmergencyContact.Instance().Build());

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMERGENCYCONTACT_MAX_TWO));
        }

        [Fact]
        public async Task Validade_EmergencyContact1NameEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.EmergencyContacts.First().Name = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID));
        }

        [Fact]
        public async Task Validade_EmergencyContact2NameEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.EmergencyContacts.Last().Name = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID));
        }

        [Fact]
        public async Task Validade_EmergencyContact1And2NameEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.EmergencyContacts.First().Name = "";
            user.EmergencyContacts.Last().Name = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID));
        }

        [Fact]
        public async Task Validade_EmergencyContact1RelationshipEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.EmergencyContacts.First().Relationship = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID));
        }

        [Fact]
        public async Task Validade_EmergencyContact2RelationshipEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.EmergencyContacts.Last().Relationship = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID));
        }

        [Fact]
        public async Task Validade_EmergencyContact1And2RelationshipEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.EmergencyContacts.First().Relationship = "";
            user.EmergencyContacts.Last().Relationship = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID));
        }

        [Fact]
        public async Task Validade_EmergencyContact1PhonenumberEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.EmergencyContacts.First().Phonenumber = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY));
        }

        [Fact]
        public async Task Validade_EmergencyContact2PhonenumberEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.EmergencyContacts.Last().Phonenumber = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY));
        }

        [Fact]
        public async Task Validade_EmergencyContact1And2PhonenumberEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.EmergencyContacts.First().Phonenumber = "";
            user.EmergencyContacts.Last().Phonenumber = "";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(3);
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY));
        }

        [Fact]
        public async Task Validade_EmergencyContact1And2SamePhonenumber()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.EmergencyContacts.First().Phonenumber = user.EmergencyContacts.Last().Phonenumber;

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMERGENCY_CONTACT_SAME_PHONENUMBER));
        }

        [Fact]
        public async Task Validade_PhonenumbersEmpty()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.Phonenumbers.Clear();

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBER_EMPTY));
        }

        [Fact]
        public async Task Validade_MoreThan2PhoneNumbers()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.Phonenumbers.Add("+55 37 9 2000-0000");
            user.Phonenumbers.Add("+55 37 9 3000-0000");
            user.Phonenumbers.Add("+55 37 9 4000-0000");

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBER_MAX_TWO));
        }

        [Fact]
        public async Task Validade_SamePhoneNumbers()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.Phonenumbers.Clear();
            user.Phonenumbers.Add("+55 37 9 0000-0000");
            user.Phonenumbers.Add("+55 37 9 0000-0000");

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBERS_ARE_SAME));
        }

        [Fact]
        public async Task Validade_EmailInvalidFormat()
        {
            var user = RequestRegisterUser.Instance().Build();
            user.Email = "usertest.com";

            var validator = CreateValidator();
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMAIL_INVALID));
        }

        [Fact]
        public async Task Validade_ExistActiveUserWithEmail()
        {
            var user = RequestRegisterUser.Instance().Build();

            var validator = CreateValidator(user.Email);
            var validationResult = await validator.ValidateAsync(user);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMAIL_ALREADYBEENREGISTERED));
        }

        private RegisterUserValidation CreateValidator(string email = null)
        {
            IUserReadOnlyRepository userReadOnlyRepository;
            if (string.IsNullOrWhiteSpace(email))
                userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().Build();
            else
                userReadOnlyRepository = UserReadOnlyRepositoryBuilder.Instance().ExistActiveUserWithEmail(email).Build();

            return new RegisterUserValidation(userReadOnlyRepository);
        }
    }
}
