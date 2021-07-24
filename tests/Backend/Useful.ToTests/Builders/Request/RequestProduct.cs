using Bogus;
using Homuai.Communication.Enums;
using Homuai.Communication.Request;

namespace Useful.ToTests.Builders.Request
{
    public class RequestProduct
    {
        private static RequestProduct _instance;

        public static RequestProduct Instance()
        {
            _instance = new RequestProduct();
            return _instance;
        }

        public RequestProductJson Build()
        {
            return new Faker<RequestProductJson>()
                .RuleFor(u => u.Name, (f) => f.Commerce.Product())
                .RuleFor(u => u.Quantity, (f) => f.Random.Number(1, 30))
                .RuleFor(u => u.Type, (f) => f.PickRandom<ProductType>())
                .RuleFor(u => u.DueDate, (f) => System.DateTime.UtcNow.AddDays(f.Random.Number(1, 30)));
        }
    }
}
