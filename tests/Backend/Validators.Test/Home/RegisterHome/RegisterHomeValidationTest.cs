using FluentAssertions;
using Homuai.Application.UseCases.Home.RegisterHome;
using Homuai.Communication.Enums;
using Homuai.Communication.Request;
using Homuai.Exception;
using System.Collections.Generic;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace Validators.Test.Home.RegisterHome
{
    public class RegisterHomeValidationTest
    {
        public static IEnumerable<object[]> GetRequestsDifferentCountries()
        {
            yield return new object[] { RequestRegisterHome.Instance().OthersCountries() };
            yield return new object[] { RequestRegisterHome.Instance().Brazil() };
        }

        [Theory]
        [MemberData(nameof(GetRequestsDifferentCountries))]
        public void Validade_Sucess(RequestRegisterHomeJson request)
        {
            var validator = new RegisterHomeValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(GetRequestsDifferentCountries))]
        public void Validade_ZipCode_Empty(RequestRegisterHomeJson request)
        {
            request.ZipCode = "";

            var validator = new RegisterHomeValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.ZIPCODE_EMPTY));
        }

        [Theory]
        [MemberData(nameof(GetRequestsDifferentCountries))]
        public void Validade_Address_Empty(RequestRegisterHomeJson request)
        {
            request.Address = "";

            var validator = new RegisterHomeValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.ADDRESS_EMPTY));
        }

        [Theory]
        [MemberData(nameof(GetRequestsDifferentCountries))]
        public void Validade_Number_Empty(RequestRegisterHomeJson request)
        {
            request.Number = "";

            var validator = new RegisterHomeValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.NUMBER_EMPTY));
        }

        [Theory]
        [MemberData(nameof(GetRequestsDifferentCountries))]
        public void Validade_City_Empty(RequestRegisterHomeJson request)
        {
            request.City = "";

            var validator = new RegisterHomeValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.CITY_EMPTY));
        }

        [Fact]
        public void Validade_Country_Empty()
        {
            var request = RequestRegisterHome.Instance().Brazil();
            request.Country = (Country)1000;

            var validator = new RegisterHomeValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.COUNTRY_EMPTY));
        }

        [Fact]
        public void Validade_Brazil_StateProvince_Empty()
        {
            var request = RequestRegisterHome.Instance().Brazil();
            request.StateProvince = "";

            var validator = new RegisterHomeValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.STATEPROVINCE_EMPTY));
        }

        [Fact]
        public void Validade_Brazil_Neighborhood_Empty()
        {
            var request = RequestRegisterHome.Instance().Brazil();
            request.Neighborhood = "";

            var validator = new RegisterHomeValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.NEIGHBORHOOD_EMPTY));
        }

        [Fact]
        public void Validade_Brazil_ZipCode_Invalid()
        {
            var request = RequestRegisterHome.Instance().Brazil();
            request.ZipCode = "123";

            var validator = new RegisterHomeValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.ZIPCODE_INVALID_BRAZIL));
        }
    }
}
