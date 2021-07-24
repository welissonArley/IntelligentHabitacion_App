using FluentAssertions;
using Homuai.Application.UseCases.Home.UpdateHomeInformations;
using Homuai.Exception;
using System.Linq;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace Validators.Test.Home.UpdateHomeInformations
{
    public class UpdateHomeInformationValidationTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var request = RequestUpdateHome.Instance().Build();

            var validator = new UpdateHomeInformationValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_ZipCode_Empty()
        {
            var request = RequestUpdateHome.Instance().Build();
            request.ZipCode = "";

            var validator = new UpdateHomeInformationValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.ZIPCODE_EMPTY));
        }

        [Fact]
        public void Validade_Address_Empty()
        {
            var request = RequestUpdateHome.Instance().Build();
            request.Address = "";

            var validator = new UpdateHomeInformationValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.ADDRESS_EMPTY));
        }

        [Fact]
        public void Validade_Number_Empty()
        {
            var request = RequestUpdateHome.Instance().Build();
            request.Number = "";

            var validator = new UpdateHomeInformationValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.NUMBER_EMPTY));
        }

        [Fact]
        public void Validade_City_Empty()
        {
            var request = RequestUpdateHome.Instance().Build();
            request.City = "";

            var validator = new UpdateHomeInformationValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CITY_EMPTY));
        }

        [Fact]
        public void Validade_Rooms_Invalid()
        {
            var request = RequestUpdateHome.Instance().Build();
            request.Rooms.Add(request.Rooms.First());

            var validator = new UpdateHomeInformationValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.THERE_ARE_DUPLICATED_ROOMS));
        }
    }
}
