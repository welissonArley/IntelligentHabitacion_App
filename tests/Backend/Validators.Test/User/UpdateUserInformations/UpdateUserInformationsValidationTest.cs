using FluentAssertions;
using Homuai.Application.UseCases.User.UpdateUserInformations;
using Homuai.Exception;
using System.Linq;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace Validators.Test.User.UpdateUserInformations
{
    public class UpdateUserInformationsValidationTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var request = RequestUpdateUser.Instance().Build();

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_NameEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.Name = "";

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.NAME_EMPTY));
        }

        [Fact]
        public void Validade_EmailEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.Email = "";

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMAIL_EMPTY));
        }

        [Fact]
        public void Validade_PhonenumbersEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.Phonenumbers.Clear();

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBER_EMPTY));
        }

        [Fact]
        public void Validade_EmergencyContactEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.EmergencyContacts.Clear();

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMERGENCYCONTACT_EMPTY));
        }

        [Fact]
        public void Validade_MoreThan2PhoneNumbers()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.Phonenumbers.Add("+55 37 9 2000-0000");
            request.Phonenumbers.Add("+55 37 9 3000-0000");
            request.Phonenumbers.Add("+55 37 9 4000-0000");

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBER_MAX_TWO));
        }

        [Fact]
        public void Validade_MoreThan2EmergencyContact()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.EmergencyContacts.Add(RequestEmergencyContact.Instance().Build());
            request.EmergencyContacts.Add(RequestEmergencyContact.Instance().Build());
            request.EmergencyContacts.Add(RequestEmergencyContact.Instance().Build());

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMERGENCYCONTACT_MAX_TWO));
        }

        [Fact]
        public void Validade_SamePhoneNumbers()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.Phonenumbers.Clear();
            request.Phonenumbers.Add("+55 37 9 0000-0000");
            request.Phonenumbers.Add("+55 37 9 0000-0000");

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PHONENUMBERS_ARE_SAME));
        }

        [Fact]
        public void Validade_EmergencyContactSamePhoneNumbers()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.EmergencyContacts.First().Phonenumber = request.EmergencyContacts.Last().Phonenumber;

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.EMERGENCY_CONTACT_SAME_PHONENUMBER));
        }

        [Fact]
        public void Validade_EmergencyContact1NameEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.EmergencyContacts.First().Name = "";

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, 1)));
        }

        [Fact]
        public void Validade_EmergencyContact2NameEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.EmergencyContacts.Last().Name = "";

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, 2)));
        }

        [Fact]
        public void Validade_EmergencyContact1And2NameEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.EmergencyContacts.First().Name = "";
            request.EmergencyContacts.Last().Name = "";

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, 1)));
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_NAME_EMERGENCY_CONTACT_INVALID, 2)));
        }

        [Fact]
        public void Validade_EmergencyContact1RelationshipEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.EmergencyContacts.First().Relationship = "";

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, 1)));
        }

        [Fact]
        public void Validade_EmergencyContact2RelationshipEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.EmergencyContacts.Last().Relationship = "";

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, 2)));
        }

        [Fact]
        public void Validade_EmergencyContact1And2RelationshipEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.EmergencyContacts.First().Relationship = "";
            request.EmergencyContacts.Last().Relationship = "";

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(2);
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, 1)));
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.THE_RELATIONSHIP_EMERGENCY_CONTACT_INVALID, 2)));
        }

        [Fact]
        public void Validade_EmergencyContact1PhonenumberEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.EmergencyContacts.First().Phonenumber = "";

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, 1)));
        }

        [Fact]
        public void Validade_EmergencyContact2PhonenumberEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.EmergencyContacts.Last().Phonenumber = "";

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, 2)));
        }

        [Fact]
        public void Validade_EmergencyContact1And2PhonenumberEmpty()
        {
            var request = RequestUpdateUser.Instance().Build();
            request.EmergencyContacts.First().Phonenumber = "";
            request.EmergencyContacts.Last().Phonenumber = "";

            var validator = new UpdateUserInformationsValidation();
            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(3);
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, 1)));
            validationResult.Errors.Should().Contain(e => e.ErrorMessage.Equals(string.Format(ResourceTextException.PHONENUMBER_EMERGENCY_CONTACT_EMPTY, 2)));
        }
    }
}