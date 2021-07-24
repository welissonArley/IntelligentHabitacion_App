using FluentAssertions;
using Homuai.Application.UseCases.MyFoods.Validator;
using Homuai.Communication.Enums;
using Homuai.Exception;
using Useful.ToTests.Builders.Request;
using Xunit;

namespace Validators.Test.MyFoods.Validator
{
    public class MyFoodValidationTest
    {
        [Fact]
        public void Validade_Sucess()
        {
            var request = RequestProduct.Instance().Build();

            var validator = new MyFoodValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_Sucess_DueDate_Null()
        {
            var request = RequestProduct.Instance().Build();
            request.DueDate = null;

            var validator = new MyFoodValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_Name_Empty()
        {
            var request = RequestProduct.Instance().Build();
            request.Name = "";

            var validator = new MyFoodValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.PRODUCT_NAME_EMPTY));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Validade_Quantity_Invalid(int quantity)
        {
            var request = RequestProduct.Instance().Build();
            request.Quantity = quantity;

            var validator = new MyFoodValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.QUANTITY_PRODUCTS_INVALID));
        }

        [Fact]
        public void Validade_Type_Invalid()
        {
            var request = RequestProduct.Instance().Build();
            request.Type = (ProductType)1000;

            var validator = new MyFoodValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.TYPE_PRODUCTS_INVALID));
        }

        [Fact]
        public void Validade_DueDate_Invalid()
        {
            var request = RequestProduct.Instance().Build();
            request.DueDate = System.DateTime.UtcNow.AddDays(-2);

            var validator = new MyFoodValidation();

            var validationResult = validator.Validate(request);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(e => e.ErrorMessage.Equals(ResourceTextException.DUEDATE_MUST_BE_GRATER_THAN_TODAY));
        }
    }
}
